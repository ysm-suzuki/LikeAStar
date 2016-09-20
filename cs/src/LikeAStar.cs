using System;
using System.Collections.Generic;
using System.Diagnostics;

using Atagoal.Core;
using LWCollide;

namespace LikeAStar
{
    // This is the main interface
    public class LikeAStar
    {
        public static bool isTest = false;

        private List<Cell> _cells = new List<Cell>();

        private float _cellWidth = -1;
        private float _cellHeight = -1;

        private float _fieldWidth = -1;
        private float _fieldHeight = -1;

        // cells
        private int _horizonSize = 0;
        private int _verticalSize = 0;

        private List<LWShape> _obstacles = new List<LWShape>();

        public void SetCellSize(float width, float height)
        {
            _cellWidth = width;
            _cellHeight = height;
        }

        public void SetFieldSize(float width, float height)
        {
            _fieldWidth = width;
            _fieldHeight = height;
        }

        public void SetObsacles(List<LWShape> obstacles)
        {
            _obstacles = obstacles;
        }


        public List<Point> Run(LWShape subject, Point start, Point destination)
        {
            System.Diagnostics.Debug.Assert(_cellWidth > 0);
            System.Diagnostics.Debug.Assert(_cellHeight > 0);
            System.Diagnostics.Debug.Assert(_fieldWidth > 0);
            System.Diagnostics.Debug.Assert(_fieldHeight > 0);
            

            MakeCells();

            // Mark the start and destination cells
            Cell startCell = GetCell(start);
            Cell destinationCell = GetCell(destination);
            System.Diagnostics.Debug.Assert(startCell != null && destinationCell != null);

            
            List<Cell> pathCells = SimpleAStar(startCell, destinationCell, _cells);
            List<Point> rawPaths = new List<Point>();

            if (pathCells.Count == 0)
                return rawPaths; // The path not found.

            foreach (Cell pathCell in pathCells)
            {
                if (pathCell == destinationCell)
                {
                    // Add the destination point instead of the destinationCell's position.
                    rawPaths.Add(destination);
                    continue;
                }

                rawPaths.Add(pathCell.GetCenter());
            }

            if (isTest)
                foreach(var test in rawPaths)
                    Console.WriteLine("raw (" + test.x + "," + test.y + ")");
            
            List<Point> optimizedPaths = Optimize(subject, rawPaths);

            if (isTest)
                foreach(var test in optimizedPaths)
                    Console.WriteLine("optimized (" + test.x + "," + test.y + ")");
                
            // Revome the start point.
            Point resultStartPoint = optimizedPaths[0];
            optimizedPaths.Remove(resultStartPoint);

            return optimizedPaths;
        }

        private Cell GetCell(Point position)
        {
            foreach (Cell cell in _cells)
                if (cell.IsWithIn(position))
                    return cell;

            return null;
        }

        private void MakeCells()
        {
            int horizonSize = (int)System.Math.Floor(_fieldWidth / _cellWidth);
            int verticalSize = (int)System.Math.Floor(_fieldHeight / _cellHeight);

            _horizonSize = horizonSize;
            _verticalSize = verticalSize;
             
            
            for (int i = 0; i < horizonSize; i++)
            {
                for (int j = 0; j < verticalSize; j++)
                {
                    Cell cell = new Cell
                    {
                        x = i * _cellWidth,
                        y = j * _cellHeight,
                        width = _cellWidth,
                        height = _cellHeight,
                        gridX = i,
                        gridY = j,
                    };

                    if (IsWithInOBstacles(cell))
                        cell.status = Cell.Status.Disabled;

                    _cells.Add(cell);
                }
            }

            
            foreach(Cell cell in _cells)
            {
                foreach (Cell nextCell in _cells)
                {
                    if (nextCell.gridX == cell.gridX - 1
                        && nextCell.gridY == cell.gridY - 1)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX - 1
                        && nextCell.gridY == cell.gridY + 0)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX - 1
                        && nextCell.gridY == cell.gridY + 1)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX + 0
                        && nextCell.gridY == cell.gridY - 1)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX + 0
                        && nextCell.gridY == cell.gridY + 1)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX + 1
                        && nextCell.gridY == cell.gridY - 1)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX + 1
                        && nextCell.gridY == cell.gridY + 0)
                        cell.nexts.Add(nextCell);

                    if (nextCell.gridX == cell.gridX + 1
                        && nextCell.gridY == cell.gridY + 1)
                        cell.nexts.Add(nextCell);
                }
            }
        }

        private bool IsWithInOBstacles(Cell cell)
        {
            List<Point> vertexes = new List<Point>
            {
                new Point
                {
                    x = cell.x,
                    y = cell.y,
                },
                new Point
                {
                    x = cell.x + cell.width,
                    y = cell.y,
                },
                new Point
                {
                    x = cell.x,
                    y = cell.y + cell.height,
                },
                new Point
                {
                    x = cell.x + cell.width,
                    y = cell.y + cell.height,
                }
            };

            foreach (Point vertex in vertexes)
            {
                foreach (var obstacle in _obstacles)
                {
                    Point point = new Point
                    {
                        x = vertex.x,
                        y = vertex.y
                    };

                    if (obstacle.IsWithIn(point))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        ////////////////////////////////////////// a-star 
        // TODO : refactor
        private List<Cell> SimpleAStar(Cell start, Cell destination, List<Cell> cells)
        {
            List<Cell> path = new List<Cell>();

            if (start.IsDisabled())
                start.ForceReady();
            if (destination.IsDisabled())
                destination.ForceReady();

            List<List<Cell>> paths = new List<List<Cell>>();
            bool isEnd = false;
            System.Action<Cell> calcurate = null;                
            calcurate = (Cell cell) => {
                if (isEnd)
                    return;
                if (cell == null)
                    return;
                if (cell == destination){
                    path = MakePath(cell);
                    isEnd = true;
                    return;
                }

                cell.Close();
                    
                List<Cell> openedCells = OpenNextCells(cell);
                if (openedCells.Count == 0)
                    return;            

                foreach(var openedCell in openedCells)
                    openedCell.SetScore(destination);

                openedCells.Sort(delegate(Cell a, Cell b){
                    float diff = a.score - b.score;
                    while (diff != 0
                        && diff * diff < 1)
                            diff *= 10;
                    return (int)(diff);
                });

                foreach(var openedCell in openedCells)
                    calcurate(openedCell);
            };

            start.Evaluate(destination);
            calcurate(start);

            // debug
            if (isTest)
                ShowGraph(cells, path, start, destination);

            return path;
        }

        // Returns Opened cells. 
        private List<Cell> OpenNextCells (Cell cell)
        {
            List<Cell> openedCells = new List<Cell>();

            foreach(var nextCell in cell.nexts)
            {
                if (!nextCell.IsReady())
                {
                    continue;
                }

                nextCell.Open(cell);
                openedCells.Add(nextCell);
            }

            return openedCells;
        }

        private List<Cell> MakePath(Cell cell)
        {
            List<Cell> path = new List<Cell>();

            Cell current = cell;
            while(current != null)
            {
                path.Add(current);
                current = current.parent;         
            }

            path.Reverse();
            return path;
        }

        //////////////////////////////////// Optimize
        private List<Point> Optimize(LWShape subject, List<Point> rawPath)
        {
            List<Point> optimizedPath = new List<Point>();

            // Set the start point. 
            optimizedPath.Add(rawPath[0]);

            if (rawPath.Count < 3)
            {
                optimizedPath.Add(rawPath[1]);
                return optimizedPath;
            }

            int pos = 0;
            for (int i = pos + 2; i < rawPath.Count; i++)
            {
                if (IsPassThrough(subject, rawPath[pos], rawPath[i]))
                {
                    continue;
                }
                    
                pos = i - 1;
                optimizedPath.Add(rawPath[i - 1]);
            }
        
            // Set the destination point.
            optimizedPath.Add(rawPath[rawPath.Count - 1]);

            return optimizedPath;
        }

        // Can pass through between two points or not
        private bool IsPassThrough(LWShape subject, Point from, Point to)
        {
            if (from.Equals(to))
            return true;

            Vector velocity = Vector.Create(from, to);

            foreach(LWShape obstacle in _obstacles)
            {
                if(subject.Collided(velocity, obstacle))
                    return false;
            }

            return true;
        }


        // Debug
        private void ShowGraph(List<Cell> cells, List<Cell> path, Cell start, Cell destination)
        {
            Console.WriteLine("---------- ShowGraph ----------");
            for (int i = _verticalSize - 1; i >= 0; i--)
            {
                String lineGraph = "|";
                for (int j = 0; j < _horizonSize; j++)
                {
                    Cell cell = FindCell(cells, j, i);
                    if (cell == null)
                        continue;
                    
                    if (cell.IsDisabled())
                        lineGraph += "/";
                    else if (cell == start)
                        lineGraph += "S";
                    else if (cell == destination)
                        lineGraph += "D";
                    else if (path.Contains(cell))
                        lineGraph += "*";
                    else
                        lineGraph += " ";
                }
                
                Console.WriteLine(lineGraph);
            }
            Console.WriteLine("------------------------------");
        }

        private Cell FindCell(List<Cell> cells, int gridX, int gridY)
        {
            foreach(Cell cell in cells)
                if (cell.gridX == gridX
                && cell.gridY == gridY)
                return cell;

            return null;
        }
    }
}
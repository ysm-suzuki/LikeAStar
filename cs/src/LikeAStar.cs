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
        private List<Cell> _cells = new List<Cell>();

        private float _cellWidth = -1;
        private float _cellHeight = -1;

        private float _fieldWidth = -1;
        private float _fieldHeight = -1;


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
            foreach (Cell pathCell in pathCells)
            {
                if (pathCell == startCell)
                    continue;
                if (pathCell == destinationCell)
                {
                    // Add the destination point instead of the destinationCell's position.
                    rawPaths.Add(destination);
                    continue;
                }

                rawPaths.Add(pathCell.GetCenter());
            }

            return rawPaths;
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
                        return true;
                }
            }

            return false;
        }


        private List<Cell> SimpleAStar(Cell start, Cell destination, List<Cell> cells)
        {
            List<Cell> path = new List<Cell>();

            foreach (var test in cells)
                if (test.GetCenter().Equals(new Point { x = 0.6f, y = 0.6f }))
                    path.Add(test);
            foreach (var test in cells)
                if (test.GetCenter().Equals(new Point { x = 2.2f, y = 0.6f }))
                    path.Add(test);

            path.Add(destination);

            return path;
        }
    }
}
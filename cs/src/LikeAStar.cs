using System;
using System.Collections.Generic;
using System.Diagnostics;
using LikeAStar.Core;
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

        private Point _start;
        private Point _destination;

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


        public List<Point> Run(Point start, Point destination)
        {
            System.Diagnostics.Debug.Assert(_cellWidth > 0);
            Debug.Assert(_cellHeight > 0);
            Debug.Assert(_fieldWidth > 0);
            Debug.Assert(_fieldHeight > 0);

            _start = start;
            _destination = destination;

            MakeCells();

            if (_cells.Count > 20)
                OptimizeCells();


            List<Point> paths = new List<Point>();


            Cell startCell = null;
            Cell destinationCell = null;
            foreach (Cell cell in _cells)
            {
                if (cell.IsWithIn(_start))
                    startCell = cell;
                if (cell.IsWithIn(_destination))
                    destinationCell = cell;
            }

            Debug.Assert(startCell != null && destinationCell != null);

            //run

            return paths;
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
                    LWCollide.Core.Point point = new LWCollide.Core.Point
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

        private void OptimizeCells()
        {

        }
    }
}
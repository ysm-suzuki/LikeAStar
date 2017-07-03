using System;
using System.Collections.Generic;
using System.Diagnostics;

using Atagoal.Core;
using LWCollide;


namespace JustLikeAStar
{
    public class SimpleAStar : AStar
    {
        private List<Cell> _cells;

        override public Stack<Cell> Get(Cell start, Cell destination, List<Cell> cells)
        {
            _cells = cells;

            if (start.IsDisabled())
                start.ForceReady();
            if (destination.IsDisabled())
                destination.ForceReady();

            start.Evaluate(destination);
            Process(start, destination);

            return _result == null
                ? new Stack<Cell>()
                : _result.path;
        }

        AStarPath _result = null;
        bool _isEnd = false;
        private void Process(Cell cell, Cell destination)
        {
            if (_isEnd)
                return;
            if (cell == null)
            {

                return;
            }
            if (cell == destination)
            {
                Stack<Cell> path = MakePath(cell);
                _result = new AStarPath
                {
                    path = path,
                    score = AStarPath.GetScore(path)
                };

                _isEnd = true;
                return;
            }

            cell.Close();
            OpenNextCells(cell);

            List<Cell> openedCells = GetOpenedCells();
            if (openedCells.Count == 0)
                return;

            foreach (var openedCell in openedCells)
                openedCell.SetScore(destination);

            openedCells.Sort(delegate(Cell a, Cell b)
            {
                float diff = a.score - b.score;
                while (diff != 0
                    && diff * diff < 1)
                    diff *= 10;
                return (int)(diff);
            });

            foreach (var openedCell in openedCells)
                Process(openedCell, destination);
        }

        // Returns Opened cells. 
        private void OpenNextCells(Cell cell)
        {
            foreach (var nextCell in cell.nexts)
            {
                if (!nextCell.IsReady())
                {
                    continue;
                }

                nextCell.Open(cell);
            }
        }

        private List<Cell> GetOpenedCells()
        {
            List<Cell> openedCells = new List<Cell>();
            foreach (var cell in _cells)
                if (cell.status == Cell.Status.Open)
                    openedCells.Add(cell);

            return openedCells;
        }
    }
}
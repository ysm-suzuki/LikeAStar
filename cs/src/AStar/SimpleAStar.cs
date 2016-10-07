using System;
using System.Collections.Generic;
using System.Diagnostics;

using Atagoal.Core;
using LWCollide;


namespace JustLikeAStar
{
    public class SimpleAStar : AStar
    {
        override public Stack<Cell> Get(Cell start, Cell destination, List<Cell> cells)
        {
            if (start.IsDisabled())
                start.ForceReady();
            if (destination.IsDisabled())
                destination.ForceReady();

            start.Evaluate(destination);
            Process(start, destination);

            return GetMinScorePath(_results).path;
        }

        List<AStarPath> _results = new List<AStarPath>();
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
                _results.Add(new AStarPath
                {
                    path = path,
                    score = AStarPath.GetScore(path)
                });

                _isEnd = true;
                return;
            }

            cell.Close();

            List<Cell> openedCells = OpenNextCells(cell);
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
        private List<Cell> OpenNextCells(Cell cell)
        {
            List<Cell> openedCells = new List<Cell>();

            foreach (var nextCell in cell.nexts)
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
    }
}
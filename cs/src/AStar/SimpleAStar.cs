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
            List<AStarPath> results = new List<AStarPath>();

            if (start.IsDisabled())
                start.ForceReady();
            if (destination.IsDisabled())
                destination.ForceReady();

            bool isEnd = false;
            System.Action<Cell> calcurate = null;
            calcurate = (Cell cell) =>
            {
                if (isEnd)
                    return;
                if (cell == null)
                {

                    return;
                }
                if (cell == destination)
                {
                    Stack<Cell> path = MakePath(cell);
                    results.Add(new AStarPath
                    {
                        path = path,
                        score = AStarPath.GetScore(path)
                    });

                    isEnd = true;
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
                    calcurate(openedCell);
            };

            start.Evaluate(destination);
            calcurate(start);

            return GetMinScorePath(results).path;
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
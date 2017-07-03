using System;
using System.Collections.Generic;
using System.Diagnostics;

using Atagoal.Core;
using LWCollide;


namespace JustLikeAStar
{
    public class AStar
    {
        protected class AStarPath
        {
            public Stack<Cell> path;
            public float score;

            public static float GetScore(Stack<Cell> target)
            {
                float ret = 0;

                foreach (Cell cell in target)
                    ret += cell.score;

                return ret;
            }
        }


        virtual public Stack<Cell> Get(Cell start, Cell destination, List<Cell> cells)
        {
            return null;
        }

        protected Stack<Cell> MakePath(Cell cell)
        {
            Stack<Cell> path = new Stack<Cell>();
            List<Cell> tempPath = new List<Cell>();

            Cell current = cell;
            while (current != null)
            {
                tempPath.Add(current);
                current = current.parent;
            }

            foreach (Cell pathCell in tempPath)
                path.Push(pathCell);

            return path;
        }
    }
}
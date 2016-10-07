using System;
using System.Collections.Generic;

using Atagoal.Core;

namespace JustLikeAStar
{
    public class Cell
    {
        public enum Status
        {
            Ready = 0,
            Open,
            Close,
            Disabled
        }

        public float x = 0;
        public float y = 0;
        public float width = 1;
        public float height = 1;

        public int gridX = 0;
        public int gridY = 0;

        public Status status = Status.Ready;

        public Cell parent = null;
        public List<Cell> nexts = new List<Cell>();

        public float estimateCost;
        public float cost;
        public float score;


        public bool IsWithIn(Point point)
        {
            if (point == null)
                return false;

            return (x <= point.x && point.x <= x + width)
                && (y <= point.y && point.y <= y + height);
        }

        public Point GetCenter()
        {
            return new Point
            {
                x = this.x + width / 2,
                y = this.y + height / 2
            };
        }

        public bool IsReady()
        {
            return status == Status.Ready;
        }
        
        public bool IsDisabled()
        {
            return status == Status.Disabled;
        }

        public void ForceReady()
        {
            status = Status.Ready;
        }

        public void Open(Cell opener)
        {
            status = Status.Open;
            parent = opener;
        }
        
        public void Close()
        {
            status = Status.Close;
        }

        public float Evaluate(Cell destination)
        {
            SetScore(destination);
            Close();
            return score;
        }

        public void SetScore(Cell destination)
        {
            float deltaX = destination.x - x;
            float deltaY = destination.y - y;

            estimateCost = deltaX * deltaX + deltaY * deltaY;
            cost = parent == null 
                    ? 0 
                    : parent.cost + (parent.x - x) * (parent.x - x) + (parent.y - y) * (parent.y - y);
            score = estimateCost + cost;
        }
    }
}
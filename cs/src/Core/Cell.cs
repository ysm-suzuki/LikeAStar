using System;

namespace LikeAStar
{
    namespace Core
    {
        public class Cell
        {
            enum Status
            {
                None = 0,
                Open,
                Close
            }

            public float x = 0;
            public float y = 0;
            public float width = 1;
            public float height = 1;

            public Status status = None;

            public Cell parent = null;

            public float estimateCost;
            public float cost;


            public bool IsWithIn(Point point)
            {
                if (point == null)
                    return false;

                return (x <= point.x && point.x <= x + width)
                    && (y <= point.y && point.y <= y + height);
            }

            public Point GetPoint()
            {
                return new Point
                    {
                        x = this.x + width / 2,
                        y = this.y + height / 2
                    };
            }
        }
    }
}
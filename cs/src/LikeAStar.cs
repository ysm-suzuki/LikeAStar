using System;
using System.Collections.Generic;
using LikeAStar.Core;
using LWCollide;

namespace LikeAStar
{
    // This is the main interface
    public class LikeAStar
    {
        private List<Cell> _cells = new List<Cell>();

        private float _cellWidth;
        private float _cellHeight;

        private float _fieldWidth;
        private float _fieldHeight;

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


        public List<Point> Run(Point start, Point Destination)
        {
            _start = start;
            _destination = destination;

            List<Point> paths = new List<Point>();

            // run

            return paths;
        }
    }
}
using System;
using System.Collections.Generic;

using Atagoal.Core;

namespace LikeAStar
{
    namespace UnitTest
    {
        public class TestMain
        {
            public static void Main(string[] args)
            {
                new TestMain().Run();
            }

            public void Run()
            {
                var likeAStar = new LikeAStarTest();
                likeAStar.RunTest();

                var cell = new CellTest();
                cell.IsWithInTest();
                cell.GetPointTest();
            }
        }


        public class LikeAStarTest
        {
            //-----------------------------------------------------------------------------
            // Test LikeAStar.Run().
            // This provide the plots of the route between two points.
            public void RunTest()
            {

            }
        }
    }
}
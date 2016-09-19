using System;
using System.Collections.Generic;

using Atagoal.Core;
using LWCollide;

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
                cell.GetCenterTest();
            }
        }


        public class LikeAStarTest
        {
            //-----------------------------------------------------------------------------
            // Test LikeAStar.Run().
            // This provide the plots of the route between two points.
            public void RunTest()
            {
                Console.WriteLine("Test LikeAStar.Run() ... ");


                List<TestCase> testCases = new List<TestCase>
                {
                    case1,
                    case2
                };

                foreach (var testCase in testCases)
                {
                    LikeAStar likeAStar = new LikeAStar();
                    likeAStar.SetCellSize(testCase.cellWidth, testCase.cellHeight);
                    likeAStar.SetFieldSize(testCase.fieldWidth, testCase.fieldHeight);
                    likeAStar.SetObsacles(testCase.obstacles);
                    List<Point> result = likeAStar.Run(
                            testCase.subject,
                            testCase.start,
                            testCase.destination
                        );

                    int count = result.Count;

                    if (count != testCase.expected.Count)
                    {
                        Console.WriteLine("  X Test Failed.");
                        Console.WriteLine("X LikeAStar.Run() is red.");
                        return;
                    }

                    for (int i = 0; i < count; i++ )
                    {
                        
                Console.WriteLine("result (" + result[i].x + "," + result[i].y + ")");
                        if (!result[i].Equals(testCase.expected[i]))
                        {
                            Console.WriteLine("  X Test Failed.");
                            Console.WriteLine("X LikeAStar.Run() is red.");
                            return;
                        }
                    }

                    Console.WriteLine("  O Test Passed.");
                }

                Console.WriteLine("O LikeAStar.Run() is green.");
            }

            class TestCase
            {
                public float cellWidth;
                public float cellHeight;
                public float fieldWidth;
                public float fieldHeight;
                public LWShape subject;
                public Point start;
                public Point destination;
                public List<LWShape> obstacles;
                public List<Point> expected;
            }


            // No Obstacles case.
            readonly TestCase case1 = new TestCase
            {
                cellWidth = 1,
                cellHeight = 1,
                fieldWidth = 4,
                fieldHeight = 4,
                subject = LWShape.Create(
                    // position
                    new Point
                    {
                        x = 0,
                        y = 2
                    },
                    // vertexes
                    new List<Point>
                    {
                        new Point
                        {
                            x = -0.2f,
                            y = -0.2f
                        },
                        new Point
                        {
                            x = -0.2f,
                            y = 0.2f
                        },
                        new Point
                        {
                            x = 0.2f,
                            y = 0.2f
                        },
                        new Point
                        {
                            x = 0.2f,
                            y = -0.2f
                        }
                    }
                ),
                start = new Point
                {
                    x = 0,
                    y = 2
                },
                destination = new Point
                {
                    x = 3,
                    y = 2
                },
                obstacles = new List<LWShape>
                {
                },
                expected = new List<Point>
                {
                    new Point
                    {
                        x = 3,
                        y = 2
                    }
                }
            };

            // A Obstacle is in the way.
            readonly TestCase case2 = new TestCase
            {
                cellWidth = 0.4f,
                cellHeight = 0.4f,
                fieldWidth = 4,
                fieldHeight = 4,
                subject = LWShape.Create(
                    // position
                    new Point
                    {
                        x = 0,
                        y = 2
                    },
                    // vertexes
                    new List<Point>
                    {
                        new Point
                        {
                            x = -0.2f,
                            y = -0.2f
                        },
                        new Point
                        {
                            x = -0.2f,
                            y = 0.2f
                        },
                        new Point
                        {
                            x = 0.2f,
                            y = 0.2f
                        },
                        new Point
                        {
                            x = 0.2f,
                            y = -0.2f
                        }
                    }
                ),
                start = new Point
                {
                    x = 0,
                    y = 2
                },
                destination = new Point
                {
                    x = 3,
                    y = 2
                },
                obstacles = new List<LWShape>
                {
                    LWShape.Create(
                        // position
                        new Point
                        {
                            x = 1.0f,
                            y = 1.0f
                        },
                        // vertexes
                        new List<Point>
                        {
                            new Point
                            {
                                x = 0,
                                y = 0
                            },
                            new Point
                            {
                                x = 0,
                                y = 2.5f
                            },
                            new Point
                            {
                                x = 1,
                                y = 2.5f
                            },
                            new Point
                            {
                                x = 1,
                                y = 0
                            }
                        }
                    )
                },
                expected = new List<Point>
                {
                    new Point
                    {
                        x = 0.6f,
                        y = 0.6f
                    },
                    new Point
                    {
                        x = 2.2f,
                        y = 0.6f
                    },
                    new Point
                    {
                        x = 3,
                        y = 2
                    }
                }
            };
        }
    }
}
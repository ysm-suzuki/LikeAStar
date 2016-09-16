using System;
using System.Collections.Generic;

using Atagoal.Core;

namespace LikeAStar
{
    namespace UnitTest
    {
        public class CellTest
        {
            //-----------------------------------------------------------------------------
            // Test IsWithIn().
            // This calculate the point is with in the cell or not.
            public void IsWithInTest()
            {
                Console.WriteLine("Test Cell.IsWithInTest() ... ");

                // A point is with in the cell
                IsWithInTestCase withInCase = IsWithInTestCase.WithIn();
                if (withInCase.cell.IsWithIn(withInCase.point) == withInCase.expected)
                {
                    Console.WriteLine("  Åõ Testing with in case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing with in case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the top of the cell
                IsWithInTestCase overTopCase = IsWithInTestCase.OverTop();
                if (overTopCase.cell.IsWithIn(overTopCase.point) == overTopCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over top case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over top case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the top right of the cell
                IsWithInTestCase overTopRightCase = IsWithInTestCase.OverTopRight();
                if (overTopRightCase.cell.IsWithIn(overTopRightCase.point) == overTopRightCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over top right case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over top right case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the right of the cell
                IsWithInTestCase overRightCase = IsWithInTestCase.OverRight();
                if (overRightCase.cell.IsWithIn(overRightCase.point) == overRightCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over right case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over right case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the bottom right of the cell
                IsWithInTestCase overBottomRightCase = IsWithInTestCase.OverBottomRight();
                if (overBottomRightCase.cell.IsWithIn(overBottomRightCase.point) == overBottomRightCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over bottom right case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over bottom right case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the bottom of the cell
                IsWithInTestCase overBottomCase = IsWithInTestCase.OverBottom();
                if (overBottomCase.cell.IsWithIn(overBottomCase.point) == overBottomCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over bottom case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over bottom case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the bottom left of the cell
                IsWithInTestCase overBottomLeftCase = IsWithInTestCase.OverBottomLeft();
                if (overBottomLeftCase.cell.IsWithIn(overBottomLeftCase.point) == overBottomLeftCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over bottom left case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over bottom left case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the left of the cell
                IsWithInTestCase overLeftCase = IsWithInTestCase.OverLeft();
                if (overLeftCase.cell.IsWithIn(overLeftCase.point) == overLeftCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over left case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over left case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is over the top left of the cell
                IsWithInTestCase overTopLeftCase = IsWithInTestCase.OverTopLeft();
                if (overTopLeftCase.cell.IsWithIn(overTopLeftCase.point) == overTopLeftCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over top left case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over top left case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }

                // A point is null
                IsWithInTestCase nullPointCase = IsWithInTestCase.NullPoint();
                if (nullPointCase.cell.IsWithIn(nullPointCase.point) == nullPointCase.expected)
                {
                    Console.WriteLine("  Åõ Testing over null point case Passed.");
                }
                else
                {
                    Console.WriteLine("  Å~ Testing over null point case Failed.");
                    Console.WriteLine("Å~ Cell.IsWithInTest() is red.");
                    return;
                }


                Console.WriteLine("Åù Cell.IsWithInTest() is green.");
            }

            //-----------------------------------------------------------------------------
            // Test GetCenter().
            // This provide a center position of the cell.
            public void GetCenterTest()
            {
                Console.WriteLine("Test Cell.GetCenter() ... ");

                List<GetCenterTestCase> testCases = new List<GetCenterTestCase>
                {
                    GetCenterTestCase.Case1(),
                    GetCenterTestCase.Case2(),
                    GetCenterTestCase.Case3()
                };

                foreach (var testCase in testCases)
                {
                    if (testCase.cell.GetCenter().Equals(testCase.expected))
                    {
                        Console.WriteLine("  Åõ Test Passed.");
                    }
                    else
                    {
                        Console.WriteLine("  Å~ Test Failed.");
                        Console.WriteLine("Å~ Cell.GetCenter() is red.");
                        return;
                    }
                }


                Console.WriteLine("Åù Cell.GetCenter() is green.");
            }
        }

        class IsWithInTestCase
        {
            public Cell cell;
            public Point point;
            public bool expected;


            public static IsWithInTestCase WithIn()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = 1,
                        y = 1
                    },
                    expected = true
                };
            }

            public static IsWithInTestCase OverTop()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = 1,
                        y = 3
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverTopRight()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = 3,
                        y = 3
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverRight()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = 3,
                        y = 1
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverBottomRight()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = 3,
                        y = -1
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverBottom()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = 1,
                        y = -1
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverBottomLeft()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = -1,
                        y = -1
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverLeft()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = -1,
                        y = 1
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase OverTopLeft()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = new Point
                    {
                        x = -1,
                        y = 3
                    },
                    expected = false
                };
            }

            public static IsWithInTestCase NullPoint()
            {
                return new IsWithInTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    point = null,
                    expected = false
                };
            }
        }

        class GetCenterTestCase
        {
            public Cell cell;
            public Point expected;

            public static GetCenterTestCase Case1()
            {
                return new GetCenterTestCase
                {
                    cell = new Cell
                    {
                        x = 0,
                        y = 0,
                        width = 2,
                        height = 2
                    },
                    expected = new Point
                    {
                        x = 1,
                        y = 1
                    }
                };
            }

            public static GetCenterTestCase Case2()
            {
                return new GetCenterTestCase
                {
                    cell = new Cell
                    {
                        x = 20,
                        y = 3,
                        width = 1,
                        height = 1
                    },
                    expected = new Point
                    {
                        x = 20.5f,
                        y = 3.5f
                    }
                };
            }

            public static GetCenterTestCase Case3()
            {
                return new GetCenterTestCase
                {
                    cell = new Cell
                    {
                        x = -20,
                        y = -20,
                        width = 4,
                        height = 4
                    },
                    expected = new Point
                    {
                        x = -18,
                        y = -18
                    }
                };
            }
        }
    }
}
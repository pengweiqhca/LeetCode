using LeetCode.P4;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace LeetCode.Tests
{
    public class P4Test
    {
        private readonly ILogger<P4Test> _logger;

        public P4Test(ILogger<P4Test> logger) => _logger = logger;

        [Theory]
        [MemberData(nameof(LeetCode407Data))]
        public void LeetCode407Test(int result, int[][] heightMap) => Assert.Equal(result, new LeetCode407(_logger).TrapRainWater(heightMap));

        public static IEnumerable<object[]> LeetCode407Data()
        {
            yield return new object[]
            {
                0, new[]
                {
                    new[] {1, 4, 3, 1, 3, 2},
                    new[] {3, 2, 1, 3, 2, 4},
                }
            };
            yield return new object[]
            {
                0, new[]
                {
                    new[] {1, 4},
                    new[] {3, 2},
                    new[] {2, 3}
                }
            };
            yield return new object[]
            {
                1, new[]
                {
                    new[] {1, 4, 3},
                    new[] {3, 2, 4},
                    new[] {2, 3, 3}
                }
            };
            yield return new object[]
            {
                4, new[]
                {
                    new[] {1, 4, 3, 1, 3, 2},
                    new[] {3, 2, 1, 3, 2, 4},
                    new[] {2, 3, 3, 2, 3, 1}
                }
            };
            yield return new object[]
            {
                8, new[]
                {
                    new[] {1, 4, 3, 1, 3, 2},
                    new[] {3, 1, 2, 4, 2, 4},
                    new[] {3, 2, 1, 3, 2, 4},
                    new[] {2, 3, 3, 2, 3, 1}
                }
            };
            yield return new object[]
            {
                57, new[]
                {
                    new[] {9, 9, 9, 9, 9},
                    new[] {9, 2, 1, 2, 9},
                    new[] {9, 2, 8, 2, 9},
                    new[] {9, 2, 3, 2, 9},
                    new[] {9, 9, 9, 9, 9}
                }
            };
            yield return new object[]
            {
                44, new[]
                {
                    new[] {78, 16, 94, 36},
                    new[] {87, 93, 50, 22},
                    new[] {63, 28, 91, 60},
                    new[] {64, 27, 41, 27},
                    new[] {73, 37, 12, 69},
                    new[] {68, 30, 83, 31},
                    new[] {63, 24, 68, 36}
                }
            };
            yield return new object[]
            {
                11, new[]
                {
                    new[] {14, 20, 11, 19, 19, 16},
                    new[] {11, 10, 7, 4, 9, 6},
                    new[] {17, 2, 2, 6, 10, 9},
                    new[] {15, 9, 2, 1, 4, 1},
                    new[] {15, 5, 5, 5, 8, 7},
                    new[] {14, 2, 8, 6, 10, 7}
                }
            };
            yield return new object[]
            {
                10, new[]
                {
                    new[] {14, 20, 11, 19, 19, 16},
                    new[] {11, 10, 7, 4, 9, 6},
                    new[] {17, 2, 2, 6, 10, 9},
                    new[] {15, 9, 2, 2, 4, 1},
                    new[] {15, 5, 5, 5, 8, 7},
                    new[] {14, 2, 8, 6, 10, 7}
                }
            };
            yield return new object[]
            {
                12, new[]
                {
                    new[] {14, 20, 11, 19, 19, 16},
                    new[] {11, 10, 7, 4, 5, 6},
                    new[] {17, 2, 2, 6, 10, 9},
                    new[] {15, 9, 2, 1, 4, 1},
                    new[] {15, 5, 5, 5, 8, 7},
                    new[] {14, 2, 8, 6, 10, 7}
                }
            };
            yield return new object[]
            {
                68900, new[]
                {
                    new[] {10795, 10570, 11434, 10378, 17467, 16601, 10097, 12902, 13317, 10492},
                    new[] {16652, 756, 7301, 280, 4286, 9441, 3865, 9689, 8444, 6619},
                    new[] {18440, 4729, 8031, 8117, 8097, 5771, 4481, 675, 709, 8927},
                    new[] {14567, 7856, 9497, 2353, 4586, 6965, 5306, 4683, 6219, 8624},
                    new[] {11528, 2871, 5732, 8829, 9503, 19, 8270, 3368, 9708, 6715},
                    new[] {16340, 8149, 7796, 723, 2618, 2245, 2846, 3451, 2921, 3555},
                    new[] {12379, 7488, 7764, 8228, 9841, 2350, 5193, 1500, 7034, 7764},
                    new[] {10124, 4914, 6987, 5856, 3743, 6491, 2227, 8365, 9859, 1936},
                    new[] {11432, 2551, 6437, 9228, 3275, 5407, 1474, 6121, 8858, 4395},
                    new[] {16029, 1237, 8235, 3793, 5818, 4428, 6143, 1011, 5928, 9529}
                }
            };
            yield return new object[]
            {
                1592346, JsonConvert.DeserializeObject<int[][]>(File.ReadAllText("407.json"))
            };
            yield return new object[]
            {
                215, new[]
                {
                    new[] {9, 9, 9, 9, 9, 9, 8, 9, 9, 9, 9},
                    new[] {9, 0, 0, 0, 0, 0, 1, 0, 0, 0, 9},
                    new[] {9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9},
                    new[] {9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9},
                    new[] {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9}
                }
            };
        }

        [Theory]
        [MemberData(nameof(LeetCode445Data))]
        public void LeetCode445Test(ListNode a, ListNode b, ListNode c) =>
            Assert.Equal(c, new LeetCode445().AddTwoNumbers(a, b));

        public static IEnumerable<object[]> LeetCode445Data()
        {
            yield return new object[]
            {
                new ListNode(1),
                new ListNode(9)
                {
                    next = new ListNode(9)
                    {
                        next = new ListNode(9)
                    }
                },
                new ListNode(1)
                {
                    next = new ListNode(0)
                    {
                        next = new ListNode(0)
                        {
                            next = new ListNode(0)
                        }
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(3)
                {
                    next = new ListNode(4)
                    {
                        next = new ListNode(2)
                    }
                },
                new ListNode(4)
                {
                    next = new ListNode(6)
                    {
                        next = new ListNode(5)
                    }
                },
                new ListNode(8)
                {
                    next = new ListNode(0)
                    {
                        next = new ListNode(7)
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(4)
                {
                    next = new ListNode(2)
                },
                new ListNode(4)
                {
                    next = new ListNode(6)
                    {
                        next = new ListNode(5)
                    }
                },
                new ListNode(5)
                {
                    next = new ListNode(0)
                    {
                        next = new ListNode(7)
                    }
                },
            };
        }
    }
}

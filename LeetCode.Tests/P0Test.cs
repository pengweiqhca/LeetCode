using System;
using LeetCode.P0;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Tests
{
    public class P0Test
    {
        [Theory]
        [MemberData(nameof(LeetCode2Data))]
        public void LeetCode2Test(ListNode a, ListNode b, ListNode c)
        {
            var d = new LeetCode2().AddTwoNumbers(a, b);

            while (c != null && d != null)
            {
                Assert.Equal(c.val, d.val);
                c = c.next;
                d = d.next;
            }

            Assert.True(c == null && d == null);
        }

        public static IEnumerable<object[]> LeetCode2Data()
        {
            yield return new object[]
            {
                null,
                new ListNode(1),
                new ListNode(1),
            };
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
                new ListNode(0)
                {
                    next = new ListNode(0)
                    {
                        next = new ListNode(0)
                        {
                            next = new ListNode(1)
                        }
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(2)
                {
                    next = new ListNode(4)
                    {
                        next = new ListNode(3)
                    }
                },
                new ListNode(5)
                {
                    next = new ListNode(6)
                    {
                        next = new ListNode(4)
                    }
                },
                new ListNode(7)
                {
                    next = new ListNode(0)
                    {
                        next = new ListNode(8)
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(2)
                {
                    next = new ListNode(4)
                },
                new ListNode(5)
                {
                    next = new ListNode(6)
                    {
                        next = new ListNode(4)
                    }
                },
                new ListNode(7)
                {
                    next = new ListNode(0)
                    {
                        next = new ListNode(5)
                    }
                },
            };
        }

        [Theory]
        [InlineData(false, "a", "")]
        [InlineData(false, "", ".")]
        [InlineData(false, "aa", "a")]
        [InlineData(true, "aa", "a*")]
        [InlineData(true, "ab", ".*")]
        [InlineData(true, "aab", "c*a*b")]
        [InlineData(false, "aaa", "aaaa")]
        [InlineData(false, "mississippi", "mis*is*p*.")]
        [InlineData(false, "ab", ".*c")]
        [InlineData(true, "ab", "**a.")]
        [InlineData(true, "aaa", "ab*a*c*a")]
        [InlineData(true, "a", "ab*")]
        [InlineData(true, "abbabaaabbabbaababbabbbbbabbbabbbabaaaaababababbbabababaabbababaabbbbbbaaaabababbbaabbbbaabbbbababababbaabbaababaabbbababababbbbaaabbbbbabaaaabbababbbbaababaabbababbbbbababbbabaaaaaaaabbbbbaabaaababaaaabb", ".*aa.*ba.*a.*bb.*aa.*ab.*a.*aaaaaa.*a.*aaaa.*bbabb.*b.*b.*aaaaaaaaa.*a.*ba.*bbb.*a.*ba.*bb.*bb.*a.*b.*bb")]
        public void LeetCode10Test(bool result, string s, string p) =>
            Assert.Equal(result, new LeetCode10().IsMatch(s, p));

        [Theory]
        [MemberData(nameof(LeetCode19Data))]
        public void LeetCode19Test(ListNode head, int n, ListNode b) =>
            Assert.Equal(b, new LeetCode19().RemoveNthFromEnd(head, n));

        public static IEnumerable<object[]> LeetCode19Data()
        {
            yield return new object[]
            {
                new ListNode(1),
                1,
                null
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
                1,
                new ListNode(1)
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
                2,
                new ListNode(2)
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
                3,
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                    }
                },
                1,
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                    }
                },
                2,
                new ListNode(1)
                {
                    next = new ListNode(3)
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                    }
                },
                3,
                new ListNode(2)
                {
                    next = new ListNode(3)
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                            }
                        }
                    }
                },
                2,
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(5)
                        }
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                            }
                        }
                    }
                },
                1,
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                        }
                    }
                },
            };
        }

        [Theory]
        [MemberData(nameof(LeetCode21Data))]
        public void LeetCode21Test(ListNode l1, ListNode l2, ListNode b) =>
            Assert.Equal(b, new LeetCode21().MergeTwoLists(l1, l2));

        public static IEnumerable<object[]> LeetCode21Data()
        {
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(4)
                    }
                },
                new ListNode(1)
                {
                    next = new ListNode(3)
                    {
                        next = new ListNode(4)
                    }
                },
                new ListNode(1)
                {
                    next = new ListNode(1)
                    {
                        next = new ListNode(2)
                        {
                            next = new ListNode(3)
                            {
                                next = new ListNode(4)
                                {
                                    next = new ListNode(4)
                                },
                            },
                        },
                    },
                },
            };
            yield return new object[]
            {
                new ListNode(1),
                new ListNode(2),
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
            };
        }

        [Theory]
        [MemberData(nameof(LeetCode23Data))]
        public void LeetCode23Test(ListNode[] list, ListNode b) =>
            Assert.Equal(b, new LeetCode23().MergeKLists(list));

        public static IEnumerable<object[]> LeetCode23Data()
        {
            yield return new object[]
            {
                new[]
                {
                    new ListNode(1)
                    {
                        next = new ListNode(4)
                        {
                            next = new ListNode(5)
                        }
                    },
                    new ListNode(1)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                        }
                    },
                    new ListNode(2)
                    {
                        next = new ListNode(6)
                    },
                },
                new ListNode(1)
                {
                    next = new ListNode(1)
                    {
                        next = new ListNode(2)
                        {
                            next = new ListNode(3)
                            {
                                next = new ListNode(4)
                                {
                                    next = new ListNode(4)
                                    {
                                        next = new ListNode(5)
                                        {
                                            next = new ListNode(6)
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            };
            yield return new object[]
            {
                new[]
                {
                    new ListNode(1),
                    new ListNode(2),
                    new ListNode(3),
                },
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                    }
                },
            };
        }

        [Theory]
        [MemberData(nameof(LeetCode25Data))]
        public void LeetCode25Test(ListNode head, int k, ListNode b) =>
            Assert.Equal(b, new LeetCode25().ReverseKGroup(head, k));

        public static IEnumerable<object[]> LeetCode25Data()
        {
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
                2,
                new ListNode(2)
                {
                    next = new ListNode(1)
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
                3,
                new ListNode(1)
                {
                    next = new ListNode(2)
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                            }
                        }
                    }
                },
                2,
                new ListNode(2)
                {
                    next = new ListNode(1)
                    {
                        next = new ListNode(4)
                        {
                            next = new ListNode(3)
                            {
                                next = new ListNode(5)
                            }
                        }
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                            }
                        }
                    }
                },
                3,
                new ListNode(3)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(1)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                            }
                        }
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                                {
                                    next = new ListNode(6)
                                }
                            }
                        }
                    }
                },
                2,
                new ListNode(2)
                {
                    next = new ListNode(1)
                    {
                        next = new ListNode(4)
                        {
                            next = new ListNode(3)
                            {
                                next = new ListNode(6)
                                {
                                    next = new ListNode(5)
                                }
                            }
                        }
                    }
                },
            };
            yield return new object[]
            {
                new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                            {
                                next = new ListNode(5)
                                {
                                    next = new ListNode(6)
                                }
                            }
                        }
                    }
                },
                3,
                new ListNode(3)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(1)
                        {
                            next = new ListNode(6)
                            {
                                next = new ListNode(5)
                                {
                                    next = new ListNode(4)
                                }
                            }
                        }
                    }
                },
            };
        }

        [Theory]
        [InlineData(new int[0], "", new string[0])]
        [InlineData(new[] { 0 }, "barfoo", new[] { "foo", "bar" })]
        [InlineData(new[] { 0, 9 }, "barfoothefoobarman", new[] { "foo", "bar" })]
        [InlineData(new[] { 0, 12 }, "barboofoothefooboobarman", new[] { "foo", "bar", "boo" })]
        [InlineData(new[] { 4, 8 }, "wordgoodgoodgoodbestword", new[] { "good", "good" })]
        [InlineData(new int[0], "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "word" })]
        [InlineData(new int[0], "abababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababab", new[] { "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba", "ab", "ba" })]
        public void LeetCode30Test(IList<int> result, string s, string[] words) => Assert.Equal(result, new LeetCode30().FindSubstring(s, words));

        [Theory]
        [InlineData(3, new[] { 1, 2, 0 })]
        [InlineData(2, new[] { 3, 4, -1, 1 })]
        [InlineData(1, new[] { 7, 8, 9, 11, 12 })]
        [InlineData(1, new[] { -1, -2, 0 })]
        public void LeetCode41Test(int result, int[] nums) =>
            Assert.Equal(result, new LeetCode41().FirstMissingPositive(nums));

        [Theory]
        [InlineData(0, new int[] { })]
        [InlineData(0, new[] { 0 })]
        [InlineData(0, new[] { 0, 0 })]
        [InlineData(0, new[] { 0, 0, 1, 0, 0 })]
        [InlineData(3, new[] { 1, 0, 2, 0, 3 })]
        [InlineData(3, new[] { 3, 0, 2, 0, 1 })]
        [InlineData(6, new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 })]
        public void LeetCode42Test(int result, int[] height) => Assert.Equal(result, new LeetCode42().Trap(height));

        [Theory]
        [InlineData(false, "a", "")]
        [InlineData(false, "", "?")]
        [InlineData(false, "aa", "a")]
        [InlineData(true, "aa", "a*")]
        [InlineData(true, "ab", "*")]
        [InlineData(true, "adceb", "*a*b")]
        [InlineData(false, "acdcb", "a*c?b")]
        [InlineData(false, "aaa", "aaaa")]
        [InlineData(false, "ab", "*c")]
        [InlineData(true, "ab", "**a?")]
        [InlineData(true, "abbabaaabbabbaababbabbbbbabbbabbbabaaaaababababbbabababaabbababaabbbbbbaaaabababbbaabbbbaabbbbababababbaabbaababaabbbababababbbbaaabbbbbabaaaabbababbbbaababaabbababbbbbababbbabaaaaaaaabbbbbaabaaababaaaabb", "**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb")]
        public void LeetCode44Test(bool result, string s, string p) =>
            Assert.Equal(result, new LeetCode44().IsMatch(s, p));

        [Theory]
        [MemberData(nameof(LeetCode57Data))]
        public void LeetCode57Test(int[][] intervals, int[] newInterval, int[][] result) =>
            Assert.Equal(result, new LeetCode57().Insert(intervals, newInterval));

        public static IEnumerable<object[]> LeetCode57Data()
        {
            yield return new object[]
            {
                Array.Empty<int[]>(),
                new[] {2, 5},
                new[] {new[] {2, 5}}
            }; //空合并
            yield return new object[]
            {
                new[] {new[] {4, 5}, new[] {6, 9}},
                new[] {1, 3},
                new[] {new[] {1, 3}, new[] {4, 5}, new[] {6, 9}}
            }; //左侧添加
            yield return new object[]
            {
                new[] {new[] {2, 4}, new[] {6, 9}},
                new[] {1, 3},
                new[] {new[] {1, 4}, new[] {6, 9}}
            }; //左起合并
            yield return new object[]
            {
                new[] {new[] {1, 3}, new[] {6, 9}},
                new[] {2, 5},
                new[] {new[] {1, 5}, new[] {6, 9}}
            }; //右侧合并
            yield return new object[]
            {
                new[] {new[] {1, 3}, new[] {6, 9}},
                new[] {7, 10},
                new[] {new[] {1, 3}, new[] {6, 10}}
            }; //右完合并
            yield return new object[]
            {
                new[] {new[] {1, 3}, new[] {6, 9}},
                new[] {2, 10},
                new[] {new[] {1, 10}}
            }; //右完合并
            yield return new object[]
            {
                new[] {new[] {1, 3}, new[] {6, 9}},
                new[] {12, 15},
                new[] {new[] {1, 3}, new[] {6, 9}, new[] {12, 15}}
            }; //右侧添加
            yield return new object[]
            {
                new[] {new[] {1, 3}, new[] {6, 9}},
                new[] {4, 5},
                new[] {new[] {1, 3}, new[] {4, 5}, new[] {6, 9}}
            }; //中间添加
            yield return new object[]
            {
                new[] {new[] {1, 2}, new[] {3, 5}, new[] {6, 7}, new[] {8, 10}, new[] {12, 16}},
                new[] {4, 8},
                new[] {new[] {1, 2}, new[] {3, 10}, new[] {12, 16}}
            }; //跨区合并
            yield return new object[]
            {
                new[] {new[] {3, 5}, new[] {6, 7}, new[] {8, 10}},
                new[] {1, 12},
                new[] {new[] {1, 12}}
            }; //跨全区合并
        }

        [Theory]
        [MemberData(nameof(LeetCode57IIData))]
        public void LeetCode57IITest(int[][] result, int target) => Assert.Equal(result, new LeetCode57II().FindContinuousSequence(target));

        public static IEnumerable<object[]> LeetCode57IIData()
        {
            yield return new object[] { new[] { new[] { 2, 3, 4 }, new[] { 4, 5 } }, 9 };
            yield return new object[] { new[] { new[] { 1, 2, 3, 4 } }, 10 };
            yield return new object[] { new[] { new[] { 3, 4, 5 } }, 12 };
            yield return new object[] { new[] { new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5, 6 }, new[] { 7, 8 } }, 15 };
            yield return new object[] { new[] { new[] { 9, 10, 11, 12, 13, 14, 15, 16 }, new[] { 18, 19, 20, 21, 22 } }, 100 };
            yield return new object[] { new[] { new[] { 13, 14, 15, 16, 17, 18 }, new[] { 30, 31, 32 }, new[] { 46, 47 } }, 93 };
        }
    }
}

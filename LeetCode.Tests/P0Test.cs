using System.Collections.Generic;
using LeetCode.P0;
using Xunit;

namespace LeetCode.Tests
{
    public class P0Test
    {
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
        public void LeetCode10Test(bool result, string s, string p) => Assert.Equal(result, new LeetCode10().IsMatch(s, p));

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
        [MemberData(nameof(LeetCode57Data))]
        public void LeetCode57Test(int[][] result, int target) => Assert.Equal(result, new LeetCode57().FindContinuousSequence(target));

        public static IEnumerable<object[]> LeetCode57Data()
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

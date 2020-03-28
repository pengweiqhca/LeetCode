using System.Collections.Generic;
using System.IO;
using LeetCode.P1;
using Newtonsoft.Json;
using Xunit;

namespace LeetCode.Tests
{
    public class P1Test
    {
        [Theory]
        [InlineData(1, new[] { 3, 4, 5, 1, 2 })]
        [InlineData(0, new[] { 4, 5, 6, 7, 0, 1, 2 })]
        [InlineData(1, new[] { 1, 3, 5 })]
        [InlineData(0, new[] { 2, 2, 2, 0, 1 })]
        public void LeetCode153Test(int result, int[] nums) =>
            Assert.Equal(result, new LeetCode153().FindMin(nums));

        [Theory]
        [InlineData(3, new[] { 3, 6, 9, 1 })]
        [InlineData(0, new[] { 10 })]
        [InlineData(0, new int[0])]
        [InlineData(1, new[] { 1, 2, 3 })]
        public void LeetCode164Test(int result, int[] nums) =>
            Assert.Equal(result, new LeetCode164().MaximumGap(nums));

        [Theory]
        [InlineData(-1, "0.1", "1.1")]
        [InlineData(1, "1.0.1", "1")]
        [InlineData(-1, "7.5.2.4", "7.5.3")]
        [InlineData(0, "1.01", "1.001")]
        [InlineData(0, "1.0.0", "1.0.0.0")]
        public void LeetCode165Test(int result, string version1, string version2) =>
            Assert.Equal(result, new LeetCode165().CompareVersion(version1, version2));

        [Theory]
        [MemberData(nameof(LeetCode174Data))]
        public void LeetCode19Test(int result, int[][] dungeon) =>
            Assert.Equal(result, new LeetCode174().CalculateMinimumHP(dungeon));

        public static IEnumerable<object[]> LeetCode174Data()
        {
            yield return new object[] { 1, new[] { new[] { 1 } } };
            yield return new object[] { 1, new[] { new[] { 0 } } };
            yield return new object[] { 3, new[] { new[] { -2 } } };
            yield return new object[] { 3, new[] { new[] { -2, 3 } } };
            yield return new object[] { 1, new[] { new[] { 1, 0, 3 } } };
            yield return new object[] { 1, new[] { new[] { 1, 1 } } };
            yield return new object[] { 2, new[] { new[] { 2, -3 } } };
            yield return new object[] { 2, new[] { new[] { 2, -3 }, new[] { -3, 2 } } };
            yield return new object[] { 1, new[] { new[] { 3, -20, 30 }, new[] { -3, 4, 0 } } };
            yield return new object[]
            {
                85,
                JsonConvert.DeserializeObject<int[][]>(File.ReadAllText("174.json"))
            };
            yield return new object[]
            {
                1,
                new[]
                {
                    new[] {3, 0, -3},
                    new[] {-3, -2, -2},
                    new[] {3, 1, -3}
                }
            };
            yield return new object[]
            {
                7,
                new[]
                {
                    new[] {-2, -3, 3},
                    new[] {-5, -10, 1},
                    new[] {10, 30, -5}
                }
            };
        }
    }
}

using LeetCode.P6;
using Xunit;

namespace LeetCode.Tests
{
    public class P6Test
    {
        [Theory]
        [InlineData(true, new[] { 0, }, 1)]
        [InlineData(true, new[] { 0, 0, 0 }, 2)]
        [InlineData(true, new[] { 1, 0, 0, 0, 1 }, 1)]
        [InlineData(false, new[] { 1, 0, 0, 0, 1 }, 2)]
        public void LeetCode605Test(bool result, int[] flowerbed, int n) =>
            Assert.Equal(result, new LeetCode605().CanPlaceFlowers(flowerbed, n));

        [Theory]
        [InlineData(true, new[] { 0, })]
        [InlineData(true, new[] { 2, 1 })]
        [InlineData(true, new[] { 2, 2, 1 })]
        [InlineData(true, new[] { 4, 2, 3 })]
        [InlineData(true, new[] { 2, 3, 3, 2, 4 })]
        [InlineData(true, new[] { -1, 4, 2, 3 })]
        [InlineData(false, new[] { 3, 4, 2, 3 })]
        [InlineData(false, new[] { 3, 2, 1, 0 })]
        [InlineData(false, new[] { 4, 2, 1 })]
        [InlineData(false, new[] { 3, 3, 2, 2 })]
        public void LeetCode665Test(bool result, int[] nums) =>
            Assert.Equal(result, new LeetCode665().CheckPossibility(nums));
    }
}

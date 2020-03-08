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
        public void LeetCode605Test(bool result, int[] flowerbed, int n) => Assert.Equal(result, new LeetCode605().CanPlaceFlowers(flowerbed, n));
    }
}

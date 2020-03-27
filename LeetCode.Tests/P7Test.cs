using LeetCode.P7;
using Xunit;

namespace LeetCode.Tests
{
    public class P7Test
    {
        [Theory]
        [InlineData(new[] { 5, 10 }, new[] { 5, 10, -5 })]
        [InlineData(new int[0], new[] { 8, -8 })]
        [InlineData(new[] { 10 }, new[] { 10, 2, -5 })]
        [InlineData(new[] { -2, -1, 1, 2 }, new[] { -2, -1, 1, 2 })]
        public void LeetCode735Test(int[] result, int[] asteroids) =>
            Assert.Equal(result, new LeetCode735().AsteroidCollision(asteroids));
    }
}

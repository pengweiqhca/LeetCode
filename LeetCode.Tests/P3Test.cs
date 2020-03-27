using LeetCode.P3;
using Xunit;

namespace LeetCode.Tests
{
    public class P3Test
    {
        [Theory]
        [InlineData("abc", "bcabc")]
        [InlineData("acdb", "cbacdcbc")]
        public void LeetCode316Test(string result, string s) =>
            Assert.Equal(result, new LeetCode316().RemoveDuplicateLetters(s));
    }
}

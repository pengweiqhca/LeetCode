using LeetCode.P11;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Tests
{
    public class P11Test
    {
        [Theory]
        [MemberData(nameof(LeetCode1116Data))]
        public async Task LeetCode1116Test(int n, string result)
        {
            var obj = new LeetCode1116(n);

            var sb = new StringBuilder(n * 2);

            void Action(int num) => sb.Append(num);

            var task = Task.WhenAll(Task.Run(() => obj.Zero(Action)),
                Task.Run(() => obj.Even(Action)),
                Task.Run(() => obj.Odd(Action)));

            if (await Task.WhenAny(task, Task.Delay(1000)) != task) throw new TimeoutException();

            Assert.Equal(result, sb.ToString());
        }

        public static IEnumerable<object[]> LeetCode1116Data()
        {
            yield return new object[] { 1, "01" };
            yield return new object[] { 2, "0102" };
            yield return new object[] { 3, "010203" };
            yield return new object[] { 4, "01020304" };
            yield return new object[] { 5, "0102030405" };
        }
    }
}

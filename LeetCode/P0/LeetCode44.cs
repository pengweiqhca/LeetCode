using System.Text.RegularExpressions;

namespace LeetCode.P0
{
    public class LeetCode44
    {
        public bool IsMatch(string s, string p) =>
            new LeetCode10().IsMatch(s, Regex.Replace(p.Replace("?", "."), @"\*+", ".*"));
    }
}

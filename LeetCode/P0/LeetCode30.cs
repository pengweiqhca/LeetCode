using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.P0
{
    public class LeetCode30
    {
        public IList<int> FindSubstring(string s, string[] words) =>
            FindSubstring(s, words, words.Sum(word => word.Length)).ToArray();

        private static IEnumerable<int> FindSubstring(string s, IReadOnlyList<string> words, int length)
        {
            var dic = words.Where(w => !string.IsNullOrEmpty(s))
                .GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());

            if (dic.Count == 0) yield break;

            words = dic.Keys.ToArray();
            for (var index = 0; index <= s.Length - length; index++)
            {
                if (IsMatch(s.AsSpan(index), words, dic)) yield return index;
            }
        }

        private static bool IsMatch(ReadOnlySpan<char> s, IReadOnlyCollection<string> words, IDictionary<string, int> wordsCount)
        {
            if (wordsCount.All(kv => kv.Value < 1)) return true;

            foreach (var word in words)
            {
                if (wordsCount[word] == 0) continue;

                if (s.Length < word.Length) return false;

                if (!s.StartsWith(word.AsSpan())) continue;

                wordsCount[word]--;
                if (IsMatch(s.Slice(word.Length), words, wordsCount))
                {
                    wordsCount[word]++;

                    return true;
                }

                wordsCount[word]++;
            }

            return false;
        }
    }
}

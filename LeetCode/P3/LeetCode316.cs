using System.Collections.Generic;

namespace LeetCode.P3
{
    public class LeetCode316
    {
        public string RemoveDuplicateLetters(string s)
        {
            if (s.Length < 2) return s;

            var dic = new SortedDictionary<char, List<int>>();

            for (var index = 0; index < s.Length; index++)
            {
                var c = s[index];
                if (dic.TryGetValue(c, out var list))
                {
                    var hasInsert = false;
                    for (var i = 0; i < list.Count; i++)
                    {
                        if (list[i] > index)
                        {
                            list.Insert(i, index);
                            hasInsert = true;
                            break;
                        }
                    }

                    if (!hasInsert) list.Add(index);
                }
                else
                    dic[c] = new List<int> { index };
            }

            var array = new char[dic.Count];
            var previousPosition = -1;
            for (var index = 0; index < array.Length; index++)
            {
                foreach (var c in dic.Keys)
                {
                    var position = previousPosition;
                    var list = dic[c];
                    for (var i = 0; i < list.Count; i++)
                    {
                        if (list[i] > position)
                        {
                            position = list[i];
                            break;
                        }
                    }

                    var all = true;
                    foreach (var kv in dic)
                    {
                        if (kv.Key != c && position >= kv.Value[kv.Value.Count - 1])
                        {
                            all = false;
                            break;
                        }
                    }

                    if (!all) continue;

                    array[index] = c;

                    dic.Remove(c);

                    previousPosition = position;

                    break;
                }
            }

            return new string(array);
        }
    }
}

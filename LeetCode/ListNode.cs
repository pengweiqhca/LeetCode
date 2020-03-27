using System;
using System.Collections;
using System.Collections.Generic;

namespace LeetCode
{
    public class ListNode : IComparable<ListNode>, IComparable, IEnumerable<int>
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

        public int CompareTo(object obj) => CompareTo(obj as ListNode);

        public int CompareTo(ListNode other)
        {
            if (other == null) return 1;

            var diff = val - other.val;
            if (diff != 0) return diff;

            if (next == null) return other.next == null ? 0 : -1;

            return other.next == null ? 1 : next.CompareTo(other.next);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<int> GetEnumerator()
        {
            var node = this;
            do
            {
                yield return node.val;
            } while ((node = node.next) != null);
        }

        public override string ToString() => next == null ? val.ToString() : $"{val}, {next}";
    }
}

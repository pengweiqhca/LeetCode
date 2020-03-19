using System;

namespace LeetCode.P0
{
    public partial class LeetCode23
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            ListNode head = null, current = null;

            while (true)
            {
                var minIndex = -1;
                ListNode min = null;
                for (var index = 0; index < lists.Length; index++)
                {
                    if (lists[index] != null && (min == null || min.val > lists[index].val))
                    {
                        minIndex = index;
                        min = lists[index];
                    }
                }

                if (minIndex < 0) break;

                lists[minIndex] = min.next;

                if (head == null) current = head = new ListNode(min.val);
                else current = current.next = new ListNode(min.val);
            }

            return head;
        }
    }
}

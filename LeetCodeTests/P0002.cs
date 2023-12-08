namespace LeetCodeTests;

public class P0002
{
    [Theory]
    [InlineData(new int[] { 2, 4, 3 }, new int[] { 5, 6, 4 }, new int[] { 7, 0, 8 })]
    [InlineData(new int[] { 0 }, new int[] { 0 }, new int[] { 0 })]
    [InlineData(new int[] { 9, 9, 9, 9, 9, 9, 9 }, new int[] { 9, 9, 9, 9 }, new int[] { 8, 9, 9, 9, 0, 0, 0, 1 })]
    [InlineData(
        new int[] { 0, 8, 6, 5, 6, 8, 3, 5, 7 },
        new int[] { 6, 7, 8, 0, 8, 5, 8, 9, 7 },
        new int[] { 6, 5, 5, 6, 4, 4, 2, 5, 5, 1 })]
    public void AddTwoNumbers(int[] l1, int[] l2, int[] r)
    {
        var s = new Solution();
        var ll1 = s.ToListNode(l1);
        var ll2 = s.ToListNode(l2);
        var rr = s.AddTwoNumbers(ll1, ll2);
        var rri = s.FromListNode(rr);
        Assert.True(Enumerable.SequenceEqual(r, rri));
    }

    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            return AddTwoListNodes(l1, l2, 0);
        }

        private ListNode AddTwoListNodes(ListNode l1, ListNode l2, int extra)
        {
            int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + extra;
            int val = 0;
            int toPush = 0;
            if (sum > 9)
            {
                val = sum % 10;
                toPush = (sum - val) / 10;
            }
            else
            {
                val = sum;
                toPush = 0;
            }

            if (l1?.next != null || l2?.next != null || toPush > 0)
                return new ListNode(val, AddTwoListNodes(l1?.next, l2?.next, toPush));
            else
                return new ListNode(val);

        }

        public ListNode ToListNode(int[] ints)
        {
            ListNode root = null;
            ListNode n = null;
            foreach (int i in ints)
            {
                if (root == null)
                {
                    root = new ListNode(i);
                    n = root;
                }
                else
                {
                    n.next = new ListNode(i);
                    n = n.next;
                }
            }
            return root;
        }

        public IEnumerable<int> FromListNode(ListNode rr)
        {
            var node = rr;
            List<int> l = new List<int>();
            while (node != null)
            {
                l.Add(node.val);
                node = node.next;
            }
            return l.ToArray();
        }
    }
}
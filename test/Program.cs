using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            ListNode listNode13 = new ListNode(4);
            ListNode listNode12 = new ListNode(2, listNode13);
            ListNode listNode11 = new ListNode(1, listNode12);

            ListNode listNode23 = new ListNode(4);
            ListNode listNode22 = new ListNode(3, listNode23);
            ListNode listNode21 = new ListNode(1, listNode22);

            Solution solution = new Solution();
            ListNode result = solution.mergeTwoLists(listNode11, listNode21);
            solution.Print(result);
            Console.ReadLine();
        }
    }

    //Definition for singly-linked list.						
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode() { }
        public ListNode(int val) { this.val = val; }
        public ListNode(int val, ListNode next) { this.val = val; this.next = next; }
    }

    class Solution
    {
        public ListNode mergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null) return l2;
            else if (l2 == null) return l1;
            else if (l1.val < l2.val)
            {
                l1.next = mergeTwoLists(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = mergeTwoLists(l2.next, l1);
                return l2;
            }
        }

        public void Print(ListNode node)
        {
            Console.WriteLine(node.val);
            if (node.next == null)
                return;
            else
            {
                Print(node.next);
            }
        }
    }

}

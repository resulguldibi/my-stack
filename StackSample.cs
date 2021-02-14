using System;
using System.Collections.Generic;

namespace my_stack
{
    public class StackSample
    {
        public static void Do()
        {
            //IMyStack myStack = new MyStack1();
            IMyStack myStack = new MyStack2();

            //test 1
            var itemsToPush = new int[] { 5, 7, 4, 6, 2, 1, 9, 8, 10, 3, 11 };
            
            //test 2
            //var itemsToPush = new int[] { 8, 9, 10, 11, 7, 6, 5, 4, 3, 2, 1 };
            
            //test 3
            //var itemsToPush = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            //itemsToPush = itemsToPush.ToList().OrderByDescending(e => e).ToArray();

            myStack.Push(itemsToPush);

            foreach (var item in itemsToPush)
            {
                Console.WriteLine("_______________________________________________");
                myStack.PrintItems();
                (var hasItem, var lastItem, var smallestItem) = myStack.Pop();
                Console.Write($"hasItem : {hasItem}, lastItem : {lastItem}, smallestItem : {smallestItem}\n");
            }

            Console.ReadLine();
        }
    }

    public interface IMyStack
    {
        int Count();
        void Push(int item);
        void Push(int[] items);
        (bool, int, int) Pop();
        int[] Items();
        void PrintItems();
    }

    public class MyStack1 : IMyStack
    {
        private int _smallestItemIndex;
        private List<int> _data;
        private List<int> _smallestItemIndexes;

        public MyStack1()
        {
            this._smallestItemIndex = 0;
            this._smallestItemIndexes = new List<int>();
            this._smallestItemIndexes.Add(this._smallestItemIndex);
            this._data = new List<int>();
        }

        public MyStack1(int initialCapacity)
        {
            this._smallestItemIndex = 0;
            this._data = new List<int>(initialCapacity);
            this._smallestItemIndexes = new List<int>(initialCapacity);
            this._smallestItemIndexes.Add(this._smallestItemIndex);
        }

        public int Count()
        {
            return this._data.Count;
        }
        public int[] Items()
        {
            return this._data.ToArray();
        }
        public (bool, int, int) Pop()
        {
            if (this._data.Count > 0)
            {
                var popedItemIndex = this._data.Count - 1;

                var lastItem = this._data[popedItemIndex];
                var smallestItem = this._data[this._smallestItemIndex];

                this._data.RemoveAt(popedItemIndex);

                if (popedItemIndex == this._smallestItemIndex)
                {
                    if (this._smallestItemIndexes.Count > 0)
                    {
                        this._smallestItemIndexes.RemoveAt(this._smallestItemIndexes.Count - 1);
                        if (this._smallestItemIndexes.Count > 0)
                        {
                            this._smallestItemIndex = (int)this._smallestItemIndexes[this._smallestItemIndexes.Count - 1];
                        }
                    }
                }

                return (true, lastItem, smallestItem);
            }

            return (false, 0, 0);

        }
        public void PrintItems()
        {
            Console.WriteLine(string.Join(",", this.Items()));
        }
        public void Push(int item)
        {
            this._data.Add(item);

            if (this._data.Count > 1 && item < this._data[this._smallestItemIndex])
            {
                this._smallestItemIndex = this._data.Count - 1;
                this._smallestItemIndexes.Add(this._smallestItemIndex);
            }
        }
        public void Push(int[] items)
        {
            if (items != null && items.Length > 0)
            {
                foreach (var item in items)
                {
                    this.Push(item);
                }
            }
        }
    }

    public class MyStack2 : IMyStack
    {
        private Stack<int> _data;
        private Stack<int> _smallestItems;
        public MyStack2()
        {
            this._smallestItems = new Stack<int>();
            this._data = new Stack<int>();
        }
        public MyStack2(int initialCapacity)
        {
            this._data = new Stack<int>(initialCapacity);
            this._smallestItems = new Stack<int>(initialCapacity);
        }

        public int Count()
        {
            return this._data.Count;
        }
        public int[] Items()
        {
            return this._data.ToArray();
        }
        public (bool, int, int) Pop()
        {
            if (this._data.Count > 0)
            {
                var lastItem = this._data.Pop();

                var smallestItem = this._smallestItems.Peek();

                if (lastItem == smallestItem)
                {
                    if (this._smallestItems.Count > 0)
                    {
                        this._smallestItems.Pop();
                    }
                }

                return (true, lastItem, smallestItem);
            }

            return (false, 0, 0);

        }
        public void PrintItems()
        {
            Console.WriteLine(string.Join(",", this.Items()));
        }
        public void Push(int item)
        {
            this._data.Push(item);

            if (this._smallestItems.Count == 0 || (this._smallestItems.Count > 0 && item < this._smallestItems.Peek()))
            {
                this._smallestItems.Push(item);
            }
        }
        public void Push(int[] items)
        {
            if (items != null && items.Length > 0)
            {
                foreach (var item in items)
                {
                    this.Push(item);
                }
            }
        }
    }
}

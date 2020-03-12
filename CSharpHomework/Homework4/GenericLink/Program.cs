using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLink
{
    class Node<T>
    {
        private Node<T> next;
        private T nodeValue;
        public Node<T> Next
        {
            get => next; set => next = value;
        }
        public T Value
        {
            get => nodeValue; set => nodeValue = value;
        }
        public Node(T t)
        {
            next = null;
            nodeValue = t;
        }
    }

    class GenericLink<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public Node<T> Head
        {
            get => head;
        }
        public GenericLink()
        {
            head = null;
            tail = null;
        }
        public void AddToTail(T t)
        {
            Node<T> elem = new Node<T>(t);
            if (tail == null)
            {
                head = elem;
                tail = elem;
            }
            else
            {
                tail.Next = elem;
                tail = elem;
            }  
        }
        public void ForEach(Action<T> action)
        {
            Node<T> node = head;
            while (node != null)
            {
                action(node.Value);
                node = node.Next;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GenericLink<int> genericLink = new GenericLink<int>();
            for(int i = 0; i < 10; i++)
            {
                genericLink.AddToTail(i);
            }
            genericLink.ForEach(delegate (int x)
            {
                Console.WriteLine($"node value is {x}");
            });
            int sum = 0;
            genericLink.ForEach(x => sum += x);
            Console.WriteLine("the sum is {0}", sum);
        }
    }
}

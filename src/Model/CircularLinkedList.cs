using System;
using Godot;

namespace MonsterCounty.Model
{
    public class CircularLinkedList<T>
    {
        private class Node(T value)
        {
            public readonly T Value = value;
            public Node Next;
        }

        private Node _head;
        private Node _current;

        public int Count { get; private set; }

        public CircularLinkedList(params T[] items)
        {
            foreach (var item in items) Add(item);
        }
        
        public void Add(T value)
        {
            var node = new Node(value);
            if (_head == null)
            {
                _head = node;
                node.Next = _head;
                _current = _head;
            }
            else
            {
                var tail = _head;
                while (tail.Next != _head)
                {
                    tail = tail.Next;
                }
                tail.Next = node;
                node.Next = _head;
            }
            Count++;
        }

        public T Peek()
        {
            return _current.Value;
        }

        public T Next()
        {
            if (_current == null) throw new InvalidOperationException("List is empty.");
            var value = _current.Value;
            _current = _current.Next;
            return value;
        }

        public bool Remove(T value)
        {
            if (_head == null) return false;
            Node prev = null;
            var node = _head;
            do
            {
                if (Equals(node.Value, value))
                {
                    if (prev != null) prev.Next = node.Next;
                    if (node == _head) _head = node.Next;
                    if (node == _current) _current = node.Next;
                    if (Count == 1)
                    {
                        _head = null;
                        _current = null;
                    }
                    Count--;
                    return true;
                }
                prev = node;
                node = node.Next;
            } while (node != _head);
            return false;
        }
    }
}
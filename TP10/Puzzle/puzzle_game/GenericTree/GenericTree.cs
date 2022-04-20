using System;
using System.Collections.Generic;

namespace puzzle_game.SimpleGraph
{
    public class SimpleGraph<T>
    {

        private T value;
        public T Value => value;
        
        private SimpleGraph<T> next;
        public SimpleGraph<T> Next
        {
            get => next;
            set => next = value;
        }

        private SimpleGraph<T> child;
        public SimpleGraph<T> Child
        {
            get => child;
            set => child = value;
        }

        public SimpleGraph<T> parent;
        
        public int heightGraph;
        public Direction directionFromParent;

        public SimpleGraph(T value) {
            this.value = value;
            this.child = null;
            this.next = null;
            this.parent = null;
            this.directionFromParent = Direction.NONE;
            this.heightGraph = 0;
        }

        public void AddChild(T newChild, Direction direction) {
            SimpleGraph<T> newNode = new SimpleGraph<T>(newChild);
            newNode.next = this.child;
            this.child = newNode;
            newNode.parent = this;
            newNode.directionFromParent = direction;
            newNode.heightGraph = this.heightGraph + 1;
        }

        public void AddChild(SimpleGraph<T> newNode, Direction direction)
        {
            newNode.next = this.child;
            this.child = newNode;
            newNode.directionFromParent = direction;
            newNode.parent = this;
            newNode.heightGraph = this.heightGraph + 1;
        }

        public List<SimpleGraph<T>> getAdjsList()
        {
            List < SimpleGraph < T > > result = new List<SimpleGraph<T>>();

            SimpleGraph<T> sibling = this.next;

            while (sibling != null) {
                result.Add(sibling);
                sibling = sibling.next;
            }
            return result;
        }

        public List<SimpleGraph<T>> getChilds()
        {
            if (this.child != null)
            {
                var result = this.child.getAdjsList();
                result.Add(this.child);
                return result;
            } 
            return null; 
        }

        /* You are free to add methods here ...
         For example remove, find etc ... */
    }
}
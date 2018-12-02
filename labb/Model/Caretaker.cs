using System;
using System.Collections.Generic;

namespace labb.Model
{
    public class Caretaker
    {
        private Stack<Memento> UndoMementoStack = new Stack<Memento>();
        private Stack<Memento> RedoMementoStack = new Stack<Memento>();

        public void Do(Originator originator)
        {
            UndoMementoStack.Push(originator.CreateMemento());
            RedoMementoStack.Clear();
        }

        public void ResetState(Originator originator)
        {
            if (UndoMementoStack.Count != 0)
            {
                originator.SetMemento(UndoMementoStack.Peek());
            }
        }

        public void Undo()
        {
            if (UndoMementoStack.Count > 1)
            {
                Memento MState = UndoMementoStack.Pop();
                RedoMementoStack.Push(MState);
            }
        }

        public void Redo()
        {
            if (RedoMementoStack.Count != 0)
            {
                Memento MState = RedoMementoStack.Pop();
                UndoMementoStack.Push(MState);
            }

        }



    }
}

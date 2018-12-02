using System;
namespace labb.Model
{
    public class Originator
    {

        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Memento CreateMemento()
        {
            return (new Memento(_state));
        }

        public void SetMemento(Memento memento)
        {
            State = memento.State;
        }

        public void SetState(string state)
        {
            this._state = state;
        }
    }

}

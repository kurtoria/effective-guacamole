using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using labb.Model;

namespace labb.ViewModel
{
    public class StartPageViewModel : INotifyPropertyChanged
    {

        private string textLabel;
        public string TextLabel { get { return textLabel; } set { SetProperty(ref textLabel, value); } }
        public ICommand ButtonCommand { private set; get; }
        public ICommand UndoCommand { private set; get; }
        public ICommand RedoCommand { private set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        Originator originator = new Originator();
        Caretaker caretaker = new Caretaker();

        public StartPageViewModel()
        {
            TextLabel = "Skriv någe";
            originator.SetState(TextLabel);
            caretaker.Do(originator);
            ButtonCommand = new Command<string>(
            execute: Button, 
            canExecute: obj =>
            {
                if (!TextLabel.Contains(obj))
                {
                    return true;
                } else
                {
                    return false;
                }

            });
            UndoCommand = new Command<string>(execute: Undo, canExecute: obj => { return true; });
            RedoCommand = new Command<string>(execute: Redo, canExecute: obj => { return true; });
        }

        private void Button(string obj)
        {
            if (TextLabel == "Skriv någe")
            {
                TextLabel = obj;
            }
            if (!TextLabel.Contains(obj))
            {
                TextLabel += " " + obj;
            }
            originator.SetState(TextLabel);
            caretaker.Do(originator);
            RefreshCanExcecute();
        }

        private void Undo(string obj)
        {
            caretaker.Undo();
            caretaker.RestoreState(originator);
            TextLabel = originator.State;
            RefreshCanExcecute();
        }

        private void Redo(string obj)
        {
            caretaker.Redo();
            caretaker.RestoreState(originator);
            TextLabel = originator.State;
            RefreshCanExcecute();
        }

        private void RefreshCanExcecute()
        {
            (ButtonCommand as Command).ChangeCanExecute();
            (UndoCommand as Command).ChangeCanExecute();
            (RedoCommand as Command).ChangeCanExecute();
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

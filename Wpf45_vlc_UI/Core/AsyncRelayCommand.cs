﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf45_vlc_UI.Core
{
    public class AsyncRelayCommand : ICommand
    {
        public Func<object, Task> ExecuteFunction { get; }
        public Predicate<object> CanExecutePredicate { get; }
        public event EventHandler CanExecuteChanged;
        public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool IsWorking { get; private set; }

        public AsyncRelayCommand(Func<object, Task> executeFunction) : this(executeFunction, (obj) => true) { }
        public AsyncRelayCommand(Func<object, Task> executeFunction, Predicate<object> canExecutePredicate)
        {
            ExecuteFunction = executeFunction;
            CanExecutePredicate = canExecutePredicate;
        }

        public bool CanExecute(object parameter) => !IsWorking && (CanExecutePredicate?.Invoke(parameter) ?? true);
        public async void Execute(object parameter)
        {
            IsWorking = true;
            UpdateCanExecute();

            await ExecuteFunction(parameter);

            IsWorking = false;
            UpdateCanExecute();
        }
    }
}

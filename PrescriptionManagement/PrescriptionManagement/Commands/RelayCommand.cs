﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrescriptionManagement.Commands
{
    internal class RelayCommand : ICommand
    {

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute is null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();

        }

        public event EventHandler CanExecuteChanged;
        protected virtual void OnCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
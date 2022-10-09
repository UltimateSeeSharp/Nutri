using System;
using System.Windows.Input;

namespace Nutri.Wpf.Infrastructure;

public class RelayCommand : ICommand
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Action CommandAction { get; set; }
    public Action<object> ObjectCommandAction { get; set; }
    public Func<bool> CanExecuteFunc { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public void Execute(object parameter)
    {
        if (CommandAction != null)
            CommandAction();
        else
            ObjectCommandAction(parameter);
    }

    public bool CanExecute(object parameter)
    {
        return CanExecuteFunc == null || CanExecuteFunc();
    }

#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
    public event EventHandler CanExecuteChanged
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
}
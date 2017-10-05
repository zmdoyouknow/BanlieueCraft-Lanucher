using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace System.Windows
{
    /// <summary>
    /// 广播命令：基本ICommand实现接口，带参数
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        public Action<T> ExecuteCommand { get; private set; }

        public Predicate<T> CanExecuteCommand { get; private set; }

        public RelayCommand(Action<T> executeCommand, Predicate<T> canExecuteCommand)
        {
            ExecuteCommand = executeCommand;
            CanExecuteCommand = canExecuteCommand;
        }

        public RelayCommand(Action<T> executeCommand)
            : this(executeCommand, null) { }

        /// <summary>
        /// 定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            ExecuteCommand?.Invoke((T)parameter);
        }

        /// <summary>
        /// 定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        /// 如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand == null || CanExecuteCommand((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { if (CanExecuteCommand != null) CommandManager.RequerySuggested += value; }
            remove { if (CanExecuteCommand != null) CommandManager.RequerySuggested -= value; }
        }
    }


    /// <summary>
    /// 广播命令：基本ICommand实现接口
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object, RoutedEventArgs> showVers;
        private Action<object> action;

        public Action ExecuteCommand { get; private set; }
        public Func<bool> CanExecuteCommand { get; private set; }

        public RelayCommand(Action executeCommand, Func<bool> canExecuteCommand)
        {
            ExecuteCommand = executeCommand;
            CanExecuteCommand = canExecuteCommand;
        }

        public RelayCommand(Action executeCommand)
            : this(executeCommand, null) { }

        public RelayCommand(Action<object, RoutedEventArgs> showVers)
        {
            this.showVers = showVers;
        }

        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        /// <summary>
        /// 定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            ExecuteCommand?.Invoke();
        }

        /// <summary>
        /// 定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        /// 如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand == null || CanExecuteCommand();
        }

        public event EventHandler CanExecuteChanged
        {
            add { if (CanExecuteCommand != null) CommandManager.RequerySuggested += value; }
            remove { if (CanExecuteCommand != null) CommandManager.RequerySuggested -= value; }
        }
    }
}

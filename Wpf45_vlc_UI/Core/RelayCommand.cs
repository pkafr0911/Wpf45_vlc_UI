using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf45_vlc_UI.Core
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute; //lưu trữ điều kiện
        private readonly Action<T> _execute; //lưu trữ hàm ủy thác lmj đó

        //khi khởi tạo thì truyền điều kiện vào ủy thác để lm ủy thác
        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _canExecute = canExecute;
            _execute = execute;

        }






        //điều kiện để chạy command
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }
        //hàm ủy thác khi chạy command
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        //tạo 1 event có tên tương ứng để ủy thác
        public event EventHandler CanExecuteChanged
        {
            //thêm bớt và menager
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Balda.Data;
using Balda.View;

namespace Balda.ViewModel
{
    class StartViewModel : INotifyPropertyChanged
    {
        private StartWindow _window;
        public StartViewModel(StartWindow window)
        {
            LoginBtnCommand = new Command(new Action<object>(LoginClick));
            RegistrationbtnCommand = new Command(new Action<object>(RegistrationClick));
            _window = window;
            Login = "qwe";
            Password = "qwe";
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        #region Command
        private void RegistrationClick(object obj)
        {
            RegistrationWindows registrWindow = new RegistrationWindows();
            RegistrationViewModel viewModel = new RegistrationViewModel(registrWindow);
            registrWindow.DataContext = viewModel;
            registrWindow.Show();
        }

        private void LoginClick(object obj)
        {
            User.SharedUser.Password = Password;
            User.SharedUser.Nickname = Login;

            MenuWindow menu = new MenuWindow();
            MenuViewModel viewModel = new MenuViewModel(menu);
            menu.DataContext = viewModel;
            menu.Show();
        }

        public ICommand RegistrationbtnCommand { get; set; }

        public ICommand LoginBtnCommand { get; set; }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

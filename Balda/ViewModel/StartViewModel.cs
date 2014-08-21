using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Balda.Data;
using Balda.View;
using ActiveRecord;

namespace Balda.ViewModel
{
    class StartViewModel : INotifyPropertyChanged
    {
        //private StartWindow _window;
        public StartViewModel(/*StartWindow window*/)
        {
            
           InitCommand();
           // _window = window;
            _login = "qwe";
        }

        private void InitCommand()
        {
            LoginBtnCommand = new Command(new Action<object>(LoginClick));
            RegistrationbtnCommand = new Command(new Action<object>(RegistrationClick));
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
            RowMapper<User> rowMapper = RowMapper;
            ActiveRecord<User> dao = new ActiveRecord<User>("User", rowMapper);
            User us = dao.Select(Login);
            
            if (us != null)
            {
                User.SharedUser = us;
                MenuWindow menu = new MenuWindow();
                MenuViewModel viewModel = new MenuViewModel(menu);
                menu.DataContext = viewModel;
                menu.Show();
            }

        }

        private User RowMapper(OleDbDataReader reader)
        {
            User user = new User();
            user.Nickname = reader["Nickname"] as string;
            user.FirstName = reader["Name"] as string;
            user.SecondName = reader["Sername"] as string;
            return user;
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

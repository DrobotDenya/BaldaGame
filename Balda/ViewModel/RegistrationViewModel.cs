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
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly RegistrationWindows _window;
        public RegistrationViewModel(RegistrationWindows window)
        {
            RegisterBtnCommand = new Command(new Action<object>(RegisterClick));
            _window = window;
            
        }

        private string _nickName;
        public string Nickname
        {
            get { return _nickName; }
            set
            {
                _nickName = value;
                OnPropertyChanged("Nickname");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");           
            }
        }

        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                _secondName = value;
                OnPropertyChanged("SecondName");
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        #region Command

        private void RegisterClick(object obj)
        {
            if (Nickname != "" && FirstName != ""
                && SecondName != "" && Password != ""
                && ConfirmPassword == Password)
            {
                User user = new User(Nickname, FirstName, SecondName, Password);
                user.UsingInsert();
                _window.Close();
            }
        }
        public ICommand RegisterBtnCommand { get; set; }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

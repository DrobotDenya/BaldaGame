using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ActiveRecord;
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

        #region Command

        private void RegisterClick(object obj)
        {
            if (!String.IsNullOrEmpty(Nickname) && !String.IsNullOrEmpty(FirstName)
                && !String.IsNullOrEmpty(SecondName))
            {
                RowMapper<User> rowMapper = RowMapper;
                ActiveRecord<User> dao = new ActiveRecord<User>("User", RowMapper);
                User user = new User();
                user.Nickname = Nickname;
                user.SecondName = SecondName;
                user.FirstName = FirstName;
                dao.Insert(user);

                _window.Close();
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

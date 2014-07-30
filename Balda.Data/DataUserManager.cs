using System.Data;
using System.Data.OleDb;

namespace Balda.Data
{
    public class DataUserManager
    {
        private static DataUserManager _dataUser;
        private OleDbDataReader _dr;
        private User _currentUser;
        private string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Users.accdb";
        private OleDbCommand _cmd = new OleDbCommand();
        private OleDbConnection _connect = new OleDbConnection();

        private DataUserManager()
        {
            _connect.ConnectionString = _connectionString;
            _cmd.Connection = _connect;
            ////readAllUser();
        }

        public static DataUserManager DataUser
        {
            get
            {
                if (_dataUser == null)
                {
                    _dataUser = new DataUserManager();
                }

                return _dataUser;
            }
        }

        public bool FindUser(string nickname, string password)
        {
            if (_connect.State == ConnectionState.Closed)
            {
                _connect.Open();
            }

            string q = "select * from [User]";
            _cmd.CommandText = q;
            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    if ((string)_dr[0] == nickname && (string)_dr[3] == password)
                    {
                        _currentUser = new User((string)_dr[0], (string)_dr[1], (string)_dr[2], (string)_dr[3]);
                        return true;
                    }
                    ////userList.Add(user);
                }
            }

            _dr.Close();
            return false;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace Balda.Data
{
    public class User
    {
        protected string _nickname;
        private string _firstName;
        private string _secondName;
        private string _password;
        private string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Users.accdb";
        private OleDbCommand _cmd = new OleDbCommand();
        private OleDbConnection _connect = new OleDbConnection();
        private OleDbDataReader _dr;
        private List<string> _words = new List<string>();
        private bool _isSurrender = false;

        public User(string nickname, string name, string sername, string password)
        {
            this._nickname = nickname;
            this._firstName = name;
            this._secondName = sername;
            this._password = password;
            _connect.ConnectionString = _connectionString;
            _cmd.Connection = _connect;
        }

        public User(string nickname)
        {
            this._nickname = nickname;
        }

        public User()
        {
        }

        public string GetNickname()
        {
            return this._nickname;
        }

        public void AddWord(string word)
        {
            _words.Add(word);
        }

        public List<string> GetWordsList()
        {
            return _words;
        }

        public int GetPoints()
        {
            int points = 0;
            foreach (string word in _words)
            {
                points += word.Length;
            }
            return points;
        }

        public void Surrender()
        {
            _isSurrender = true;
        }

        public bool IsSurrender()
        {
            return _isSurrender;
        }

        public void Insert()
        {
            _connect.Open();
            string q = "select * from [User]";
            _cmd.CommandText = q;
            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    if (this._nickname == _dr[0].ToString())
                    {
                        _dr.Close();
                        _connect.Close();
                        break;
                    }
                }
            }
            _dr.Close();
            if (_connect.State == ConnectionState.Open)
            {
                _cmd.CommandText = "INSERT INTO [User] ([Nickname], [Name], [Sername], [Password]) values (?,?,?,?)";

                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = _nickname;
                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = _firstName;
                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = _secondName;
                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = _password;
                _cmd.ExecuteNonQuery();
                _connect.Close();
            }
        }

        public bool ConfirmPassword(string pswd)
        {
            if (pswd == this._password)
            {
                return true;
            }
            return false;
        }
    }
}

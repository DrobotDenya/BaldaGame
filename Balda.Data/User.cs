using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace Balda.Data
{
    public class User
    {
        public  string Nickname;
        public  string FirstName;
        public  string SecondName;
        public  string Password;
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\Resources\Users.accdb";
        private OleDbCommand _cmd = new OleDbCommand();
        private OleDbConnection _connect = new OleDbConnection();
        private OleDbDataReader _dr;
        private DataSet _dataSet = new DataSet();
        private List<string> _words = new List<string>();
        private bool _isSurrender = false;
        private static User _user;

        public static User SharedUser 
        { 
           get
           {
               if (_user == null)
               {
                   _user = new User();
                   
               }
               return _user;
           }            
        }

        public User(string nickname, string name, string sername, string password)
        {
            this.Nickname = nickname;
            this.FirstName = name;
            this.SecondName = sername;
            this.Password = password;
            _connect.ConnectionString = ConnectionString;
            _cmd.Connection = _connect;
        }

        public User(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
           
        }

        public User(string nickname)
        {
            Nickname = nickname;
        }

        public User()
        {
            _connect.ConnectionString = ConnectionString;
            _cmd.Connection = _connect;
        }
        
        public string GetNickname()
        {
            return this.Nickname;
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

        public void UsingInsert()
        {
            _connect.Open();
            string q = "select * from [User]";
            _cmd.CommandText = q;
            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    if (this.Nickname == _dr[0].ToString())
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

                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = Nickname;
                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = FirstName;
                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = SecondName;
                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = Password;
                _cmd.ExecuteNonQuery();
                _connect.Close();
            }
        }

        public bool UsingSelect()
        {
            _connect.Open();
            string q = "select * from [User] where Nickname = Nickname";
            _cmd.CommandText = q;
            //string s = (string)_cmd.ExecuteScalar;
            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    if (this.Nickname == _dr[0].ToString() && Password == _dr[3].ToString())
                    {
                        Nickname = _dr[0].ToString();
                        FirstName = _dr[1].ToString();
                        SecondName = _dr[2].ToString();                      
                        _dr.Close();
                        _connect.Close();
                        return true;
                    }
                }
            } 
            _dr.Close();
            _connect.Close();
            return false;
        }

        public bool ConfirmPassword(string pswd)
        {
            if (pswd == this.Password)
            {
                return true;
            }
            return false;
        }
    }
}

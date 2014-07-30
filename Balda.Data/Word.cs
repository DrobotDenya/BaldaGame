using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class Word
    {
        private string _word;
        private int _value;
        private int _player;
        private string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\DataBase.accdb";
        private OleDbCommand _cmd = new OleDbCommand();
        private OleDbConnection _connect = new OleDbConnection();
        private OleDbDataReader _dr;

        public Word(string word, int value, int player)
        {
            this._word = word;
            this._value = value;
            this._player = player;
            _connect.ConnectionString = _connectionString;
            _cmd.Connection = _connect;
        }

        public void Insert()
        {
            _connect.Open();
            string q = "select * from [TABLE]";
            _cmd.CommandText = q;
            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    if (this._word == _dr[0].ToString())
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
                _cmd.CommandText = "INSERT INTO [TABLE] ([Word], [Value], [Player]) values (?,?,?)";

                _cmd.Parameters.Add(string.Empty, OleDbType.VarChar).Value = _word;
                _cmd.Parameters.Add(string.Empty, OleDbType.Integer).Value = _value;
                _cmd.Parameters.Add(string.Empty, OleDbType.Integer).Value = _player;
                _cmd.ExecuteNonQuery();
                _connect.Close();
            }
        }

        private void Remove()
        {
            _connect.Open();
            string q = "select * from [TABLE]";
            _cmd.CommandText = q;
            _dr = _cmd.ExecuteReader();
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    if (this._word == _dr[0].ToString())
                    {
                        _cmd.CommandText = "DELETE FROM [TABLE] WHERE [Word]=" + this._word;
                        _cmd.ExecuteNonQuery();
                    }
                }
            }
            _dr.Close();
            _connect.Close();
        }
    }
}

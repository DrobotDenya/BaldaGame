using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Balda.Data
{
    public class Word
    {
        string word;
        int value;
        int player;
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\DataBase.accdb";
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection connect = new OleDbConnection();
        OleDbDataReader dr;

        public Word(string word, int value, int player)
        {
            this.word = word;
            this.value = value;
            this.player = player;
            connect.ConnectionString = connectionString;
            cmd.Connection = connect;
      }

        public void insert()
        {
            connect.Open();
            string q = "select * from [TABLE]";
            cmd.CommandText = q;          
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (this.word == dr[0].ToString())
                    {
                        dr.Close();
                        connect.Close();
                        break;
                    }
                }
            }
            dr.Close();
            if (connect.State == ConnectionState.Open)
            {
                cmd.CommandText = "INSERT INTO [TABLE] ([Word], [Value], [Player]) values (?,?,?)";

                cmd.Parameters.Add("", OleDbType.VarChar).Value = word;
                cmd.Parameters.Add("", OleDbType.Integer).Value = value;
                cmd.Parameters.Add("", OleDbType.Integer).Value = player;
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            
        }


        void remove()
        {
            connect.Open();
            string q = "select * from [TABLE]";
            cmd.CommandText = q;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (this.word == dr[0].ToString())
                    {
                        cmd.CommandText = "DELETE FROM [TABLE] WHERE [Word]=" + this.word;
                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            dr.Close();
                connect.Close();;
           
        }
    }
}

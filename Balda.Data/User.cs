using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Balda.Data
{
    public class User
    {
        string nickname;
        string firstName;
        string secondName;
        string password;
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Users.accdb";
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection connect = new OleDbConnection();
        OleDbDataReader dr;

        public User(string nickname, string name, string sername, string password)
        {
            this.nickname = nickname;
            this.firstName = name;
            this.secondName = sername;
            this.password = password;
            connect.ConnectionString = connectionString;
            cmd.Connection = connect;

        }

        public void insert()
        {
            connect.Open();
            string q = "select * from [User]";
            cmd.CommandText = q;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (this.nickname == dr[0].ToString())
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
                cmd.CommandText = "INSERT INTO [User] ([Nickname], [Name], [Sername], [Password]) values (?,?,?,?)";

                cmd.Parameters.Add("", OleDbType.VarChar).Value = nickname;
                cmd.Parameters.Add("", OleDbType.VarChar).Value = firstName;
                cmd.Parameters.Add("", OleDbType.VarChar).Value = secondName;
                cmd.Parameters.Add("", OleDbType.VarChar).Value = password;
                cmd.ExecuteNonQuery();
                connect.Close();
            }
        }
    }
}

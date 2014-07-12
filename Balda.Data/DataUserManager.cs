using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Balda.Data
{
    public class DataUserManager
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Users.accdb";
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection connect = new OleDbConnection();
        OleDbDataReader dr;

        public DataUserManager()
        {
            connect.ConnectionString = connectionString;
            cmd.Connection = connect;
        }

        public bool findUser(string nickname)
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            string q = "select * from [User]";
            cmd.CommandText = q;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (nickname == dr[0].ToString())
                    {
                        dr.Close();
                        connect.Close();
                        return true;
                    }
                }
            }
            dr.Close();
            return false;
        }
        public bool findUserPassword(string password)
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            string q = "select * from [User]";
            cmd.CommandText = q;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (password == dr[3].ToString())
                    {
                        dr.Close();
                        connect.Close();
                        return true;
                    }
                }
            }
            dr.Close();
            return false;
        }
    }
}

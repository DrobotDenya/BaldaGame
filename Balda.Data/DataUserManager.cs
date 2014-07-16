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
        //SingleTon
        private static DataUserManager dataUser;

        private DataUserManager() 
        {
          connect.ConnectionString = connectionString;
          cmd.Connection = connect;
          readAllUser();
        }

        public static DataUserManager DataUser
        {
            get
            {
                if (dataUser == null)
                {
                    dataUser = new DataUserManager();
                }
                return dataUser;
            }
        }


        List<User> userList = new List<User>();
        User currentUser;

        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Users.accdb";
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection connect = new OleDbConnection();
        OleDbDataReader dr;

        private void readAllUser()
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
                User user;
                while (dr.Read())
                {
                    user = new User((string)dr[0], (string)dr[1], (string)dr[2], (string)dr[3]);
                    userList.Add(user);
                }
            }
            dr.Close();
 
        }

        //Проверяет существует ли пользователь с таким ником и паролем
        public bool findUser(string nickname, string password)
        {
            foreach (User user in userList)
            {
                if (user.getNickname() == nickname && user.confirmPassword(password))
                {
                    currentUser = user;
                    return true;
                }
            }
            return false;
        }

        public User getCurrentUser()
        {
            return currentUser;
        }
    }
}

using System.Data;
using System.Data.OleDb;

namespace Balda.Data
{
    public class DataUserManager
    {
        private static DataUserManager dataUser;
        private OleDbDataReader dr;
        private User currentUser;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Users.accdb";
        private OleDbCommand cmd = new OleDbCommand();
        private OleDbConnection connect = new OleDbConnection();

        private DataUserManager()
        {
            connect.ConnectionString = connectionString;
            cmd.Connection = connect;
            ////readAllUser();
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

        public bool findUser(string nickname, string password)
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
                    if ((string)dr[0] == nickname && (string)dr[3] == password)
                    {
                        currentUser = new User((string)dr[0], (string)dr[1], (string)dr[2], (string)dr[3]);
                        return true;
                    }
                    ////userList.Add(user);
                }
            }

            dr.Close();
            return false;
        }

        public User getCurrentUser()
        {
            return currentUser;
        }
    }
}

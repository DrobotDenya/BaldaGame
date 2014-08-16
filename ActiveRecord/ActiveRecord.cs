using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ActiveRecord
{
    public delegate T RowMapper<T>(OleDbDataReader reader);

    public class ActiveRecord<T>
    {
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\Resources\";
        private string _dataBase;
        private RowMapper<T> rowMapper;

        public ActiveRecord(string db, RowMapper<T> row)
        {
            _dataBase = db;
            rowMapper = row;
        }

        #region Select
        public T Select(string id)
        {
            return FindEntityById(id, rowMapper);
        }

        private T FindEntityById(string id, RowMapper<T> row)
        {
            T result = default(T);

            result = Find("select * from [" + _dataBase + "] where Nickname = ?", id, row);
            return result;


        }

        private T Find(string query, string id, RowMapper<T> row)
        {
            T result = default(T);

            using (OleDbDataReader reader = GetQueryDataReader(query, id))
            {
                result = GetSingleEntity(reader, rowMapper);
            }

            return result;
        }

        public OleDbDataReader GetQueryDataReader(string query, string parametr)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString + _dataBase + ".accdb");
            connection.Open();
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.Add("", OleDbType.Char).Value = parametr;
            OleDbDataReader dataReader;
            dataReader = command.ExecuteReader();
            return dataReader;
        }

        public T GetSingleEntity(OleDbDataReader reader, RowMapper<T> row)
        {
            while (reader.Read())
            {
                return rowMapper(reader);
            }
            return default(T);
        }
        #endregion

        #region Insert
        public void Insert(T entity)
        {
            Save(GetOleDbParametrs(entity), "INSERT INTO [" + _dataBase + "] ([Nickname], [Name], [Sername]) values (?,?,?)");
        }

        private void Save(OleDbParameter[] oleDbParameters, string query)
        {
            if (Select(oleDbParameters[0].ToString()) != null)
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString + _dataBase + ".accdb"))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);

                    foreach (OleDbParameter parameter in oleDbParameters)
                    {
                        command.Parameters.Add("", OleDbType.Char).Value = parameter.Value;
                    }

                    command.ExecuteNonQuery();
                }
            }

        }

        private OleDbParameter[] GetOleDbParametrs(T entity)
        {

            List<OleDbParameter> parameters = new List<OleDbParameter>();

            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {

                parameters.Add(new OleDbParameter(string.Format("@{0}", propertyInfo.Name), propertyInfo.GetValue(entity, null)));
            }

            return parameters.ToArray();
        }
        #endregion

        #region Delete

        public void Delete(string id)
        {
            Remove("delete from [" + _dataBase + "] where [Id] = ?", id);
        }


        private void Remove(string query, string id)
        {
            T result = default(T);
            using (OleDbConnection connection = new OleDbConnection(ConnectionString + _dataBase + ".accdb"))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.Add("", OleDbType.Char).Value = id;
                command.ExecuteNonQuery();
            }
           
        }
        #endregion

        #region

        public List<T> FindAll()
        {
            return FindEntities("select * from [" + _dataBase + "]",rowMapper);
        }

        public List<T> FindEntities(string query, RowMapper<T> row)
        {
            List<T> result = default(List<T>);

            using (OleDbDataReader reader = GetQueryDataReader(query, ""))
            {
                result = GetEntities(reader, row);
            }

            return result;

        }

        public List<T> GetEntities<T>(OleDbDataReader reader, RowMapper<T> rowMapper)
        {
           List<T> results = new List<T>();

            while (reader.Read())
            {
                T result = rowMapper(reader);
                results.Add(result);
            }

            return results;
        }

        #endregion

    }
}

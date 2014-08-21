using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: CLSCompliant(true)]
namespace ActiveRecord
{
    public delegate T RowMapper<T>(OleDbDataReader reader);
    /// <summary>
    /// Класс реализовывает шаблон ActiveRecord
    /// </summary>
    public class ActiveRecord<T>
    {
        /// <summary>
        /// Путь к базе данных
        /// </summary>
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\Resources\";
        /// <summary>
        /// Назвние базы данных
        /// </summary>
        private string _dataBase;
        private RowMapper<T> rowMapper;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="db">
        /// Название базы данных
        /// </param>
        /// <param name="row">
        /// Структура БД
        /// </param>
        public ActiveRecord(string db, RowMapper<T> row)
        {
            _dataBase = db;
            rowMapper = row;
        }

        #region Select

        public T Select(string id)
        {
            T result = default(T);

            result = Find("select * from [" + _dataBase + "] where Nickname = ?", id);
            return result;
        }
        /// <summary>
        /// Поиск обьекта в БД
        /// </summary>
        /// <param name="query">
        /// Запрос
        /// </param>
        /// <param name="id">
        /// Id обьекта
        /// </param>
        /// <returns>
        /// обьект
        /// </returns>
        private T Find(string query, string id)
        {
            T result = default(T);

            using (OleDbDataReader reader = GetQueryDataReader(query, id))
            {
                result = GetSingleEntity(reader);
            }

            return result;
        }
        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="query">
        /// Id обьекта
        /// </param>
        /// <param name="parametr">
        /// Запрос
        /// </param>
        /// <returns>
        /// OleDBDataReader
        /// </returns>
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
        /// <summary>
        /// Получение нужного обьекта
        /// </summary>
        /// <returns>
        /// обьекта
        /// </returns>
        public T GetSingleEntity(OleDbDataReader reader)
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
        /// <summary>
        /// Запись обьекта в БД
        /// </summary>
        /// <param name="oleDbParameters">
        /// OleDbParameter[]
        /// </param>
        /// <param name="query">
        /// Запрос
        /// </param>
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
        /// <summary>
        /// Получение всех полей записуемого обьекта
        /// </summary>
        /// <param name="entity">
        /// Обьект
        /// </param>
        /// <returns>
        /// OleDBDataReader
        /// </returns>
        private OleDbParameter[] GetOleDbParametrs(T entity)
        {

            Collection<OleDbParameter> parameters = new Collection<OleDbParameter>();

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

        /// <summary>
        /// Удаление обьекта с БД
        /// </summary>
        /// <param name="id">
        /// ID Обьекта
        /// </param>
        /// /// <param name="query">
        /// Запрос
        /// </param>
        private void Remove(string query, string id)
        {
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
       
        public Collection<T> FindAll()
        {
            return FindEntities("select * from [" + _dataBase + "]", rowMapper);
        }
        /// <summary>
        /// Поиск всех обьектов БД
        /// </summary>
        /// /// <param name="query">
        /// Список обьектов БД
        /// </param>
        public Collection<T> FindEntities(string query, RowMapper<T> row)
        {
            Collection<T> result = default(Collection<T>);

            using (OleDbDataReader reader = GetQueryDataReader(query, ""))
            {
                result = GetEntities(reader, row);
            }

            return result;
        }
        /// <summary>
        /// Формирование списка обектов
        /// </summary>
        /// /// <param name="query">
        /// Список обьектов БД
        /// </param>
        public Collection<T> GetEntities(OleDbDataReader reader, RowMapper<T> rowMapper)
        {
            Collection<T> results = new Collection<T>();

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

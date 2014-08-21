using System;
using System.Collections.ObjectModel;

[assembly: CLSCompliant(true)]
namespace Balda.Data
{
    /// <summary>
    ///Клас пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        ///Ник ппользователя
        /// </summary>  
        public string Nickname { get; set; }
        /// <summary>
        ///Имя пользователя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        ///Фамилия пользователя
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        ///Лист слов исплльзованых в игровом процесе
        /// </summary>
        private Collection<string> _words = new Collection<string>();
        /// <summary>
        ///Поле отвечает за статус игрока, сдался ли он
        /// </summary>
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

            set { _user = value; }
        }

        /// <summary>
        ///Конструктор
        /// </summary>
        public User(string nickname, string name, string sername)
        {
            this.Nickname = nickname;
            this.FirstName = name;
            this.SecondName = sername;
        }
        /// <summary>
        ///Конструктор
        /// </summary>
        public User(string nickname)
        {
            Nickname = nickname;
        }
        /// <summary>
        ///Конструктор
        /// </summary>
        public User()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Ник пользователя
        /// </returns>
        public string GetNickname()
        {
            return this.Nickname;
        }
        /// <summary>
        /// Добавление слова в список использованых слов
        /// </summary>
        /// <param name="word">
        /// </param>
        public void AddWord(string word)
        {
            _words.Add(word);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Список использованых слов
        /// </returns>
        /// 
        public Collection<string> GetWordsList()
        {
            return _words;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Набраные очки
        /// </returns>
        /// 
        public int GetPoints()
        {
            int points = 0;
            foreach (string word in _words)
            {
                points += word.Length;
            }
            return points;
        }
        /// <summary>
        /// Присвоение игроку статуса "Сдался"
        /// </summary>
        public void Surrender()
        {
            _isSurrender = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Возвращает статус игрока
        /// </returns>
        public bool IsSurrender()
        {
            return _isSurrender;
        }
    }
}

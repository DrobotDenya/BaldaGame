namespace Balda.Data
{
    public class Settings
    {
        private static Settings _settings;
        /// <summary>
        ///Сложность бота
        /// </summary> 
        private int _botComplexity = 1;
        /// <summary>
        ///С кем будет играть игрок, с ботом или нет
        /// </summary> 
        private bool _isBot = true;
        /// <summary>
        ///Имя второго игрока
        /// </summary> 
        private string _playerName;
        /// <summary>
        ///Конструктор
        /// </summary> 
        private Settings()
        {
        }

        public static Settings Setting
        {
            get
            {
                if(_settings == null)
                {
                    _settings = new Settings();
                }
                return _settings;
            }
        }
        /// <summary>
        /// Установка сложности бота
        /// </summary>
        /// <param name="complexity"> 
        /// </param>
        public void SetBotComplexity(int complexity)
        {
            _botComplexity = complexity;
        }
        /// <summary>
        /// Установка второго игрока
        /// </summary>
        /// <param name="b"> 
        /// </param>
        public void SetIsBot(bool b)
        {
            _isBot = b;
        }
        /// <summary>
        /// Установка имени второго игрока
        /// </summary>
        /// <param name="name"> 
        /// </param>
        public void SetPlayerName(string name)
        {
            _playerName = name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Возвращает сложность бота
        /// </returns>
        /// 
        public int GetBotComplexity()
        {
            return _botComplexity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Возвращает второгоро игрока
        /// </returns>
        /// 
        public bool GetIsBot()
        {
            return _isBot;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Возвращает имя второго игрока
        /// </returns>
        /// 
        public string GetNamePlayer()
        {
            return _playerName;
        }
    }
}

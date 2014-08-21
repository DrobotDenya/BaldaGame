using System.Collections.ObjectModel;

namespace Balda.Data
{
    public class GameManager
    {

        private GameBoard _board = new GameBoard();
        private GameKeys _keyBoard = new GameKeys();
        private Dictionary _dictionary = new Dictionary();
        /// <summary>
        ///Список всех использованых слов
        /// </summary>  
        private Collection<string> _usedWords = new Collection<string>();
        /// <summary>
        ///Список игрокав
        /// </summary>  
        private Collection<User> _playersList = new Collection<User>();
        /// <summary>
        ///Размер поля
        /// </summary>  
        private int _size = 5;
        /// <summary>
        ///Ширина клавиатуры
        /// </summary>  
        private int _width = 11;
        /// <summary>
        ///Висота клавиатуры
        /// </summary>  
        private int _heigth = 3;
        /// <summary>
        ///Количество игроков
        /// </summary>  
        private int _playersCount;
        /// <summary>
        ///Сложность боа
        /// </summary>  
        private int _botComplexity;
        /// <summary>
        ///Кл-во ботов
        /// </summary>  
        private int _botsCount;
        /// <summary>
        ///Текуший игрок
        /// </summary>  
        private int _activePlayer;

        private FindWordAlgorithm _algorithm;
        /// <summary>
        ///Конструктор
        /// </summary>  
        public GameManager()
        {
            _board.SetSize(_size, _size);
            _algorithm = new FindWordAlgorithm(_board, _usedWords);
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        /// Словарь
        /// </returns> 
        public Dictionary GetDictionary()
        {
            return _dictionary;
        }
        /// <summary>
        ///Генерирует стартовое поле
        /// </summary> 
        public void GeneretaBoard()
        {
            _board.Clear();
            _board.SetSize(_size, _size);

            for (int row = 0; row < _board.Heigth(); row++)
            {
                for (int col = 0; col < _board.Width(); col++)
                {
                    _board.SetCellValue(string.Empty, row, col);
                }
            }

            string randWord = GetDictionary().GetRandomWord(_size);

            if (randWord != null)
            {
                int middleRow = _size / 2;
                for (int i = 0; i < _size; ++i)
                {
                    _board.SetCellValue(randWord.Substring(i, 1), middleRow, i);
                }

                _usedWords.Add(randWord);
            }
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        /// Словарь
        /// </returns> 
        ////Return поле
        public GameBoard GetGameBoard()
        {
            return _board;
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        /// Словарь
        /// </returns> 
        ////Return клавиатуру
        public GameKeys GetKeyBoard()
        {
            return _keyBoard;
        }

        public int GetSizeBoard()
        {
            return _size;
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        /// ширина клавиатуры
        /// </returns>  
        public int GetWidth()
        {
            return _width;
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        /// висота клавиатуры
        /// </returns> 
        public int GetHeigth()
        {
            return _heigth;
        }
        /// <summary>
        ///Определяет, было ли слово использовано в игре
        /// </summary> 
        public bool WordIsUsed(string word)
        {
            return _usedWords.Contains(word);
        }
        /// <summary>
        ///Генерируем игроков
        /// </summary> 
        public void GeneratePalyers()
        {
            _playersList.Clear();
            _playersList.Add(User.SharedUser);
            if (Settings.Setting.GetIsBot())
            {
                _playersList.Add(new Bot("Bot ", _botComplexity, _algorithm));
            }
            else
            {
                _playersList.Add(new User(Settings.Setting.GetNamePlayer()));
            }
        }
        /// <summary>
        ///Передает управление между игроками
        /// </summary> 
        public void ExchangePlayer()
        {
            if (DetermineWinner() != null)
            {
                return;
            }

            UpdateUsedWords();

            //// Сменить игрока
            _activePlayer++;
            if (_activePlayer >= _playersList.Count)
            {
                _activePlayer = 0;
            }

            //// Если следующий игрок сдался, переходим к ещё следующему
            if (ActivePlayer().IsSurrender())
            {
                ExchangePlayer();
            }

            //// Если текущий игрок бот, то он сразу делает ход
            //// и переходим к следующему игроку
            if (ActivePlayer() is Bot)
            {
                ((Bot)ActivePlayer()).Solution();
                ExchangePlayer();
            }
        }
        /// <summary>
        ///Обновляет список использованых слов
        /// </summary> 
        public void UpdateUsedWords()
        {
            string firstWord = _usedWords[0];
            _usedWords.Clear();
            _usedWords.Add(firstWord);
            foreach (User user in Players())
                foreach (string word in user.GetWordsList())
                    _usedWords.Add(word);
        }
        /// <summary>
        ///Определяет победителя
        /// </summary> 
        /// <returns>
        /// Игрок
        /// </returns> 
        public User DetermineWinner()
        {
            ////Игра закончена, если поле заполнено
            if (FieldIsFull() == true)
            {
                User winner = new User();
                foreach (User user in _playersList)
                {
                    if (!user.IsSurrender())
                    {
                        if (user.GetPoints() > winner.GetPoints())
                        {
                            winner = user;
                        }
                    }
                }
                return winner;
            }
            ////Игра закончена, если в игре остался только один игрок
            if (CountPlayersInGame() < 2)
            {
                foreach (User user in _playersList)
                {
                    if (!user.IsSurrender())
                    {
                        return user;
                    }
                }
            }
            ////Игра закончена, если все оставшиеся игроки боты
            bool humanInGame = false;

            foreach (User user in _playersList)
            {
                if (!user.IsSurrender() && !(user is Bot))
                {
                    humanInGame = true;
                }
            }

            if (!humanInGame)
            {
                foreach (User user in _playersList)
                {
                    if (!user.IsSurrender())
                    {
                        return user;
                    }
                }
            }
            return null;
        }
        /// <summary>
        ///Возвращает список использованых слов в игре
        /// </summary> 
        /// <returns>
        ///Список
        /// </returns> 
        public Collection<string> GetUsedWord()
        {
            return _usedWords;
        }
        /// <summary>
        ///Задаёт количество игроков-людей
        /// </summary> 
        public void SetPlayerCount(int count)
        {
            if (count > 0 && count < 6)
            {
                _playersCount = count;
            }
        }
        /// <summary>
        ///Задаёт количество игроков-ботов
        /// </summary> 
        public void SetBotsCount(int count)
        {
            if (count > 0 && count < 3)
            {
                _botsCount = count;
            }
        }
        /// <summary>
        ///Задаёт сложность ботов
        /// </summary> 
        public void SetBotsComplexity(int compl)
        {
            if (compl == 0 || compl == 1 || compl == 2)
            {
                _botComplexity = compl;
            }
        }
        /// <summary>
        ///Возвращает текущего игрока
        /// </summary> 
        /// <returns>
        ///Игрок
        /// </returns> 
        public User ActivePlayer()
        {
            return _playersList[_activePlayer];
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        ///Список игроков
        /// </returns> 
        public Collection<User> Players()
        {
            return _playersList;
        }
        /// <summary>
        ///Запуск игры
        /// </summary> 
        public void Start()
        {
            GeneretaBoard();
            GeneratePalyers();
            _activePlayer = _playersList.Count - 1;
            ExchangePlayer();
        }
        /// <summary>
        ///
        /// </summary> 
        /// <returns>
        ///Номер Ттекущего игрока
        /// </returns> 
        public int GetActivePlayerIndex()
        {
            return _activePlayer;
        }
        /// <summary>
        ///Очистка всех использованых слов
        /// </summary> 
        public void ClearUsedWords()
        {
            _usedWords.Clear();
            foreach (User user in _playersList)
            {
                user.GetWordsList().Clear();
            }
        }
        /// <summary>
        ///Определяет, заполнено или нет игровое поле
        /// </summary> 
        private bool FieldIsFull()
        {
            for (int row = 0; row < _board.Heigth(); row++)
            {
                for (int col = 0; col < _board.Width(); col++)
                {
                    if (string.IsNullOrEmpty(_board.CellPool[row][col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        ///Возвращает к-во играющих игроков
        /// </summary> 
        /// <returns>
        ///Кл-во игроков
        /// </returns> 
        private int CountPlayersInGame()
        {
            int countPlayers = 0;
            foreach (User p in _playersList)
            {
                if (!p.IsSurrender())
                {
                    countPlayers++;
                }
            }
            return countPlayers;
        }
    }
}

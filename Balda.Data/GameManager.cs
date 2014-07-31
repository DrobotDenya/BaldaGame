using System.Collections.Generic;

namespace Balda.Data
{
    public class GameManager
    {
        private GameBoard _board = new GameBoard();
        private GameKeys _keyBoard = new GameKeys();
        private Dictionary _dictionary = new Dictionary();
        private List<string> _usedWords = new List<string>();
        private List<User> _playersList = new List<User>();
        private int _size = 5;
        private int _width = 11;
        private int _heigth = 3;
        private int _playersCount;
        private int _botComplexity;
        private int _botsCount;
        private int _activePlayer;

        private FindWordAlgorithm _algorithm;

        public GameManager()
        {
            _board.SetSize(_size, _size);
            _algorithm = new FindWordAlgorithm(_board, _usedWords);
        }

        ////Return ширину словарь
        public Dictionary GetDictionary()
        {
            return _dictionary;
        }

        ////Генерирует стартовое поле
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

        ////Return поле
        public GameBoard GetGameBoard()
        {
            return _board;
        }

        ////Return клавиатуру
        public GameKeys GetKeyBoard()
        {
            return _keyBoard;
        }

        public int GetSizeBoard()
        {
            return _size;
        }

        ////Return ширину клавиатуры
        public int GetWidth()
        {
            return _width;
        }

        ////Return висоту клавиатуры
        public int GetHeigth()
        {
            return _heigth;
        }

        ////Определяет, было ли слово использовано в игре
        public bool WordIsUsed(string word)
        {
            return _usedWords.Contains(word);
        }

        ////Генерируем игроков
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

        ////Передает управление между игроками
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

        ////Обновляет список использованых слов
        public void UpdateUsedWords()
        {
            string firstWord = _usedWords[0];
            _usedWords.Clear();
            _usedWords.Add(firstWord);
            foreach (User user in Players())
                foreach (string word in user.GetWordsList())
                    _usedWords.Add(word);
        }

        ////Определяет победителя
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

        ////Возвращает список использованых слов в игре
        public List<string> GetUsedWord()
        {
            return _usedWords;
        }

        ////Задаёт количество игроков-людей
        public void SetPlayerCount(int count)
        {
            if (count > 0 && count < 6)
            {
                _playersCount = count;
            }
        }

        ////Задаёт количество игроков-ботов
        public void SetBotsCount(int count)
        {
            if (count > 0 && count < 3)
            {
                _botsCount = count;
            }
        }

        ////Задаёт сложность ботов
        public void SetBotsComplexity(int compl)
        {
            if (compl == 0 || compl == 1 || compl == 2)
            {
                _botComplexity = compl;
            }
        }

        ////Возвращает текущего игрока
        public User ActivePlayer()
        {
            return _playersList[_activePlayer];
        }

        public List<User> Players()
        {
            return _playersList;
        }

        public void Start()
        {
            GeneretaBoard();
            GeneratePalyers();
            _activePlayer = _playersList.Count - 1;
            ExchangePlayer();
        }

        public int GetActivePlayerIndex()
        {
            return _activePlayer;
        }

        public void ClearUsedWords()
        {
            _usedWords.Clear();
            foreach (User user in _playersList)
            {
                user.GetWordsList().Clear();
            }
        }

        ////Определяет, заполнено или нет игровое поле
        private bool FieldIsFull()
        {
            for (int row = 0; row < _board.Heigth(); row++)
            {
                for (int col = 0; col < _board.Width(); col++)
                {
                    if (_board.GetCellValue(row, col).Equals(string.Empty))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        ////Возвращает к-во играющих игроков
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

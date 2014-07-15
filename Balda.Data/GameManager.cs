using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class GameManager
    {
        GameBoard _board = new GameBoard();
        GameKeys _keyBoard = new GameKeys();
        Dictionary _dictionary = new Dictionary();
        List<string> usedWords = new List<string>();
        List<User> playersList = new List<User>();
        int size = 5;
        int width = 11;
        int heigth = 3;
        int playersCount;
        int botComplexity;
        int botsCount;
        int _activePlayer;


        FindWordAlgorithm algorithm;

        public GameManager()
        {
            _board.setSize(size, size);
            algorithm = new FindWordAlgorithm(_board, usedWords);

            playersCount = 1;
            botsCount = 1;
            botComplexity = 2;

            
        }

        //Return ширину словарь
        public Dictionary getDictionary()
        {
            return _dictionary;
        }

        //Генерирует стартовое поле
        public void generetaBoard()
        {
            _board.clear();
            _board.setSize(size, size);

            for (int row = 0; row < _board.heigth(); row++)
            {
                for (int col = 0; col < _board.width(); col++)
                {
                    _board.setCellValue("", row, col);
                }
            }

            string randWord = getDictionary().getRandomWord(size);

            if (randWord != null)
            {
                int middleRow = size / 2;
                for (int i = 0; i < size; ++i)
                {
                    _board.setCellValue(randWord.Substring(i, 1), middleRow, i);
                }

                usedWords.Add(randWord);
            }
        }

        //Return поле
        public GameBoard getGameBoard()
        {
            return _board;
        }

        //Return клавиатуру
        public GameKeys getKeyBoard()
        {
            return _keyBoard;
        }

        public int getSizeBoard()
        {
            return size;
        }

        //Return ширину клавиатуры
        public int getWidth()
        {
            return width;
        }

        //Return висоту клавиатуры
        public int getHeigth()
        {
            return heigth;
        }

        //Определяет, было ли слово использовано в игре
        public bool wordIsUsed(string word)
        {
            return usedWords.Contains(word);
        }

        //Генерируем игроков
        public void generatePalyers()
        {
            playersList.Clear();
            for (int i = 0; i < playersCount; ++i)
            {
                playersList.Add(new User("Player " + i));
            }

            for (int i = 0; i < botsCount; ++i)
            {
                playersList.Add(new Bot("Bot " + i, botComplexity, algorithm));
            }

        }

        //Передает управление между игроками
        public void exchangePlayer()
        {
            if(determineWinner() != null)
            {
                return;
            }
        
        updateUsedWords();

        // Сменить игрока
        _activePlayer++;
        if(_activePlayer >= playersList.Count) {
            _activePlayer = 0;
        }
        
        // Если следующий игрок сдался, переходим к ещё следующему
        if(activePlayer().isSurrender()) {
            exchangePlayer();
        }
        
        // Если текущий игрок бот, то он сразу делает ход 
        // и переходим к следующему игроку
        if(activePlayer() is Bot) {
            ((Bot)activePlayer()).solution();
            exchangePlayer();
        }
 
        }

        //Обновляет список использованых слов
        public void updateUsedWords()
        {
            string firstWord = usedWords[0];
            usedWords.Clear();
            usedWords.Add(firstWord);
            foreach (User user in players())
            {
                foreach (string word in user.getWordsList())
                {

                    usedWords.Add(word);
                }
            }
        }

        //Определяет победителя
        public User determineWinner()
        {
            //Игра закончена, если поле заполнено
            if (fieldIsFull() == true)
            {
                User winner = new User();
                foreach (User user in playersList)
                {
                    if (!user.isSurrender())
                    {
                        if (user.getPoints() > winner.getPoints())
                        {
                            winner = user;
                        }
                    }
                }
                return winner;
            }
            //Игра закончена, если в игре остался только один игрок
            if (countPlayersInGame() < 2)
            {
                foreach (User user in playersList)
                {
                    if (!user.isSurrender())
                    {
                        return user;
                    }
                }
            }
            //Игра закончена, если все оставшиеся игроки боты
            bool humanInGame = false;
            
                foreach (User user in playersList)
                {
                    if (!user.isSurrender() && (user is Bot))//!!!!!!!!!!!!!!!!!
                    {
                        humanInGame = true;
 
                    }
                }
                if (!humanInGame)
                {
                    foreach (User user in playersList)
                    {
                        if (!user.isSurrender())
                        {
                            return user;
                        }
                    }
 
                }
                return null;
        }

        //Определяет, заполнено или нет игровое поле
        bool fieldIsFull()
        {
            for (int row = 0; row < _board.heigth(); row++)
            {
                for (int col = 0; col < _board.width(); col++)
                {
                    if (_board.getCellValue(row, col).Equals(""))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Возвращает к-во играющих игроков
        int countPlayersInGame() {
        int countPlayers = 0;
        foreach(User p in playersList) {
            if(!p.isSurrender()) {
                countPlayers++;
            }
        }
        return countPlayers;
    }

        //Возвращает список использованых слов в игре
        public List<string> getUsedWord()
        {
            return usedWords;
        }

        //Задаёт количество игроков-людей
        public void setPlayerCOunt(int count)
        {
            if (count > 0 && count < 6)
            {
                playersCount = count;
            }
 
        }

        //Задаёт количество игроков-ботов
        public void setBotsCount(int count)
        {
            if (count > 0 && count < 3)
            {
                botsCount = count;
            }
        }

        //Задаёт сложность ботов
        public void setBotsComplexity(int compl)
        {
            if (compl == 0 || compl == 1)
            {
                botComplexity = compl;
            }
        }

        //Возвращает текущего игрока
        public User activePlayer()
        {
            return playersList[_activePlayer];
        }


        public List<User> players()
        {
            return playersList;
        }

        public void start()
        {
            generetaBoard();
            generatePalyers();
            _activePlayer = playersList.Count - 1;
            exchangePlayer();
        }

        public int getActivePlayerIndex()
        {
            return _activePlayer;
        }
    }
}

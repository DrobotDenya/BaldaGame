﻿namespace Balda.Data
{
    /// <summary>
    /// Класс бота
    /// </summary>
    public class Bot : User
    {
        /// <summary>
        /// Сложность бота
        /// </summary>
        private int _levelOfComplexity = 0;
        private FindWordAlgorithm _alho = new FindWordAlgorithm();
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">
        /// Имя бота
        /// </param>
        /// <param name="level">
        /// Уровень сожности игрока
        /// </param>
        ///  <param name="alho">
        /// Класс поиска слова
        /// </param>
        public Bot(string name, int level, FindWordAlgorithm alho)
        {
            Nickname = name;
            this._alho = alho;
            if (level >= 0 && level <= 2)
            {
                _levelOfComplexity = level;
            }
            else
            {
                _levelOfComplexity = 0;
            }
        }

        public int Solution()
        {
            ////_alho.FindWords();
            string findedWord;
            if (_levelOfComplexity == 1 || _levelOfComplexity == 0)
            {
                findedWord = _alho.FindWord();
                if (!string.IsNullOrEmpty(findedWord))
                {
                    AddWord(findedWord);
                    return 1;
                }
            }

            if (_levelOfComplexity == 2)
            {
                findedWord = _alho.FindWordWithMaxLength();
                if (!string.IsNullOrEmpty(findedWord))
                {
                    AddWord(findedWord);
                    return 1;
                }
            }
            ////Если слово не найдено и бот легкий, то он сдается
            if (_levelOfComplexity == 0)
            {
                Surrender();
                return 0;
            }
            ////Иначе пропускает ход
            return 0;
        }
    }
}

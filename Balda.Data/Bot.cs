﻿namespace Balda.Data
{
    public class Bot : User
    {
        private int _levelOfComplexity = 0;
        private FindWordAlgorithm _alho = new FindWordAlgorithm();

        public Bot(string name, int level, FindWordAlgorithm alho)
        {
            _nickname = name;
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
                if (findedWord.Equals(string.Empty) == false)
                {
                    AddWord(findedWord);
                    return 1;
                }
            }

            if (_levelOfComplexity == 2)
            {
                findedWord = _alho.FindWordWithMaxLength();
                if (findedWord.Equals(string.Empty) == false)
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

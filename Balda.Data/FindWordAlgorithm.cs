using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class FindWordAlgorithm
    {
        private GameBoard _gameBoard = new GameBoard();
        private List<string> _userWord = new List<string>();
        private Dictionary _dictionary = new Dictionary();
        private string[,] _strings;
        private int[,] _pr;
        private int _ai = 0;
        private int _aj = 0;
        private int _p, _t;
        private int _curr = 0;
        private string _word;
        private bool _tl;
        private bool _bl;

        public FindWordAlgorithm(GameBoard gameBoard, List<string> userWord)
        {
            this._gameBoard = gameBoard;
            this._userWord = userWord;
        }

        public FindWordAlgorithm()
        {
        }

        ////Ищет все подходящие слова в словаре
        public List<string> FindWords()
        {
            List<string> res = new List<string>();
            _strings = new string[_gameBoard.Width(), _gameBoard.Heigth()];
            for (int row = 0; row < _gameBoard.Width(); row++)
                for (int column = 0; column < _gameBoard.Heigth(); column++)
                    _strings[row, column] = string.Empty;

            _pr = new int[_gameBoard.Width(), _gameBoard.Heigth()];
            for (int row = 0; row < _gameBoard.Width(); row++)
                for (int column = 0; column < _gameBoard.Heigth(); column++)
                    _pr[row, column] = 0;

            while (_curr < _dictionary.GetCount())
            {
                _word = _dictionary.GetDictionary()[_curr];
                if (!_userWord.Contains(_word))
                {
                    for (int row = 0; row < _gameBoard.Width(); row++)
                        for (int column = 0; column < _gameBoard.Heigth(); column++)
                            _pr[row, column] = 0;

                    _tl = true;
                    ////Цикл по слову из словаря
                    for (int k = 0; k < _word.Length; k++)
                    {
                        //// Цикл по самому массиву.
                        for (int i = 0; i < _gameBoard.Width(); i++)
                        {
                            for (int j = 0; j < _gameBoard.Heigth(); j++)
                            {
                                if (_gameBoard.GetCellValue(i, j) == string.Empty)
                                {
                                    for (_ai = 0; _ai < _gameBoard.Width(); _ai++)
                                        for (_aj = 0; _aj < _gameBoard.Heigth(); _aj++)
                                            _strings[_ai, _aj] = _gameBoard.GetCellValue(_ai, _aj);

                                    _gameBoard.SetCellValue(_word.Substring(k, 1), i, j);
                                    _bl = true;
                                    _p = k; // С какой буквы подставляли для поиска в конец слова
                                    _t = k; // С какой буквы подставляли для поиска в начало слова
                                    _ai = i;
                                    _aj = j;

                                    for (int i1 = 0; i1 < _gameBoard.Width(); ++i1)
                                        for (int j1 = 0; j1 < _gameBoard.Heigth(); ++j1)
                                            _pr[i1, j1] = 0;

                                    _pr[i, j] = 1;
                                    for (_p = _p + 1; _p < _word.Length; _p++)
                                    {
                                        if (CheckLetter(_p))
                                            continue;
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    _ai = i;
                                    _aj = j;
                                    if (_bl)
                                    {
                                        for (_t = _t - 1; _t >= 0; _t--)
                                            if (CheckLetter(_t))
                                                continue;
                                            else
                                            {
                                                break;
                                            }

                                        if (_bl)
                                        {
                                            res.Add(_word);
                                            for (_ai = 0; _ai < 5; _ai++)
                                                for (_aj = 0; _aj < 5; _aj++)
                                                    _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);

                                            _tl = false;
                                            break;   //// Слово подошло переходим к следующему слову
                                        }
                                    }

                                    for (_ai = 0; _ai < _gameBoard.Width(); _ai++)
                                        for (_aj = 0; _aj < _gameBoard.Heigth(); _aj++)
                                            _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);  //// восстановили массив, в текущем состоянии.
                                }
                            }

                            if (!_tl)
                            {
                                break;
                            }
                        }
                    }

                    _curr++;
                }
                else
                {
                    _curr++;
                }
            }
            return res;
        }

        public int MaxLength(List<string> words)
        {
            int length = 0;

            foreach (string w in words)
            {
                if (length < w.Length)
                {
                    length = w.Length;
                }
            }
            return length;
        }

        ////Возворащает первое подходящие слово из словаря
        public string FindWord()
        {
            _strings = new string[_gameBoard.Width(), _gameBoard.Heigth()];
            for (int row = 0; row < _gameBoard.Width(); row++)
            {
                for (int column = 0; column < _gameBoard.Heigth(); column++)
                {
                    _strings[row, column] = string.Empty;
                }
            }
            _pr = new int[_gameBoard.Width(), _gameBoard.Heigth()];

            for (int row = 0; row < _gameBoard.Width(); row++)
            {
                for (int column = 0; column < _gameBoard.Heigth(); column++)
                {
                    _pr[row, column] = 0;
                }
            }

            while (_curr < _dictionary.GetCount())
            {
                _word = _dictionary.GetDictionary()[_curr];
                if (!_userWord.Contains(_word))
                {
                    for (int i = 0; i < _gameBoard.Width(); ++i)
                    {
                        for (int j = 0; j < _gameBoard.Heigth(); ++j)
                        {
                            _pr[i, j] = 0;
                        }
                    }
                    _tl = true;
                    for (int k = 0; k < _word.Length; k++)
                    {
                        for (int i = 0; i < _gameBoard.Width(); i++)
                        {
                            for (int j = 0; j < _gameBoard.Heigth(); j++)
                            {
                                if (_gameBoard.GetCellValue(i, j) == string.Empty)
                                {
                                    for (_ai = 0; _ai < _gameBoard.Width(); _ai++)
                                    {
                                        for (_aj = 0; _aj < _gameBoard.Heigth(); _aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            _strings[_ai, _aj] = _gameBoard.GetCellValue(_ai, _aj);
                                        }
                                    }
                                    _gameBoard.SetCellValue(_word.Substring(k, 1), i, j);
                                    _bl = true;
                                    _p = k; // С какой буквы подставляли для поиска в конец слова
                                    _t = k; // С какой буквы подставляли для поиска в начало слова
                                    _ai = i;
                                    _aj = j;
                                    for (int i1 = 0; i1 < _gameBoard.Width(); ++i1)
                                    {
                                        for (int j1 = 0; j1 < _gameBoard.Heigth(); ++j1)
                                        {
                                            _pr[i1, j1] = 0;
                                        }
                                    }
                                    _pr[i, j] = 1;
                                    for (_p = _p + 1; _p < _word.Length; _p++)
                                    {
                                        if (CheckLetter(_p))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    _ai = i;
                                    _aj = j;
                                    if (_bl)
                                    {
                                        for (_t = _t - 1; _t >= 0; _t--)
                                        {
                                            if (CheckLetter(_t))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (_bl)
                                        {
                                            return _word;
                                        }
                                    }
                                    for (_ai = 0; _ai < _gameBoard.Width(); _ai++)
                                    {
                                        for (_aj = 0; _aj < _gameBoard.Heigth(); _aj++)
                                        {
                                            _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }
                            if (!_tl)
                            {
                                break;
                            }
                        }
                    }
                    _curr++;
                }
                else
                {
                    _curr++;
                }
            }
            return string.Empty;
        }

        public string FindWordWithMaxLength()
        {
            _strings = new string[_gameBoard.Width(), _gameBoard.Heigth()];

            for (int row = 0; row < _gameBoard.Width(); row++)
            {
                for (int column = 0; column < _gameBoard.Heigth(); column++)
                {
                    _strings[row, column] = string.Empty;
                }
            }
            _pr = new int[_gameBoard.Width(), _gameBoard.Heigth()];

            for (int row = 0; row < _gameBoard.Width(); row++)
            {
                for (int column = 0; column < _gameBoard.Heigth(); column++)
                {
                    _pr[row, column] = 0;
                }
            }
            List<string> dict = FindWords();
            _curr = 0;
            while (_curr < dict.Count)
            {
                _word = dict[_curr];
                if (!_userWord.Contains(_word) && _word.Length == MaxLength(dict))
                {
                    for (int i = 0; i < _gameBoard.Width(); ++i)
                    {
                        for (int j = 0; j < _gameBoard.Heigth(); ++j)
                        {
                            _pr[i, j] = 0;
                        }
                    }

                    _tl = true;
                    for (int k = 0; k < _word.Length; k++)
                    {
                        for (int i = 0; i < _gameBoard.Width(); i++)
                        {
                            for (int j = 0; j < _gameBoard.Heigth(); j++)
                            {
                                if (_gameBoard.GetCellValue(i, j) == string.Empty)
                                {
                                    for (_ai = 0; _ai < _gameBoard.Width(); _ai++)
                                    {
                                        for (_aj = 0; _aj < _gameBoard.Heigth(); _aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            _strings[_ai, _aj] = _gameBoard.GetCellValue(_ai, _aj);
                                        }
                                    }
                                    _gameBoard.SetCellValue(_word.Substring(k, 1), i, j);
                                    _bl = true;
                                    _p = k; // С какой буквы подставляли для поиска в конец слова
                                    _t = k; // С какой буквы подставляли для поиска в начало слова
                                    _ai = i;
                                    _aj = j;
                                    for (int i1 = 0; i1 < _gameBoard.Width(); ++i1)
                                    {
                                        for (int j1 = 0; j1 < _gameBoard.Heigth(); ++j1)
                                        {
                                            _pr[i1, j1] = 0;
                                        }
                                    }
                                    _pr[i, j] = 1;
                                    for (_p = _p + 1; _p < _word.Length; _p++)
                                    {
                                        if (CheckLetter(_p))
                                            continue;
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    _ai = i;
                                    _aj = j;
                                    if (_bl)
                                    {
                                        for (_t = _t - 1; _t >= 0; _t--)
                                        {
                                            if (CheckLetter(_t))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (_bl)
                                        {
                                            return _word;
                                        }
                                    }
                                    for (_ai = 0; _ai < _gameBoard.Width(); _ai++)
                                    {
                                        for (_aj = 0; _aj < _gameBoard.Heigth(); _aj++)
                                        {
                                            _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }
                            if (!_tl)
                            {
                                break;
                            }
                        }
                    }
                    _curr++;
                }
                else
                {
                    _curr++;
                }
            }
            return string.Empty;
        }

        ////Определяет, есть ли буква на поле сверху или снизу или влева или справа текущей ячейки
        public bool CheckLetter(int index)
        {
            string letter = _word.Substring(index, 1);

            if (_ai + 1 != _gameBoard.Width())
            {
                if ((_gameBoard.GetCellValue(_ai + 1, _aj) == letter)
                        && (_pr[_ai + 1, _aj] != 1))
                {
                    _pr[_ai + 1, _aj] = 1;
                    _ai++;
                    return true;
                }
            }
            if (_ai - 1 != -1)
            {
                if ((_gameBoard.GetCellValue(_ai - 1, _aj) == letter)
                        && (_pr[_ai - 1, _aj] != 1))
                {
                    _pr[_ai - 1, _aj] = 1;
                    _ai--;
                    return true;
                }
            }
            if (_aj + 1 != _gameBoard.Heigth())
            {
                if ((_gameBoard.GetCellValue(_ai, _aj + 1) == letter)
                        && (_pr[_ai, _aj + 1] != 1))
                {
                    _pr[_ai, _aj + 1] = 1;
                    _aj++;
                    return true;
                }
            }
            if (_aj - 1 != -1)
            {
                if ((_gameBoard.GetCellValue(_ai, _aj - 1) == letter)
                        && (_pr[_ai, _aj - 1] != 1))
                {
                    _pr[_ai, _aj - 1] = 1;
                    _aj--;
                    return true;
                }
            }

            _bl = false; // Если не совпало ни одно условие выше то это слово не подходит.
            return false;
        }
    }
}

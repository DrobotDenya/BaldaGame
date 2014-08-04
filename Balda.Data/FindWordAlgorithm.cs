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
        private int[,] _binaryMatrixLetters;
        private int _ai = 0;
        private int _aj = 0;
        private int _bottomNumber, _topNumber;
        private int _currentWordInDict = 0;
        private string _word;
        private bool _isAcceptWord;
        private int _width;
        private int _heigth;

        public FindWordAlgorithm(GameBoard gameBoard, List<string> userWord)
        {
            this._gameBoard = gameBoard;
            this._userWord = userWord;
            _width = _gameBoard.Width();
            _heigth = _gameBoard.Heigth();
        }

        public FindWordAlgorithm()
        {
        }

        ////Ищет все подходящие слова в словаре
        public List<string> FindWords()
        {
            List<string> res = new List<string>();
            _strings = new string[_width, _heigth];
            for (int row = 0; row < _width; row++)
                for (int column = 0; column < _heigth; column++)
                    _strings[row, column] = string.Empty;

            _binaryMatrixLetters = new int[_width, _heigth];
            for (int row = 0; row < _width; row++)
                for (int column = 0; column < _heigth; column++)
                    _binaryMatrixLetters[row, column] = 0;

            while (_currentWordInDict < _dictionary.GetCount())
            {
                _word = _dictionary.GetDictionary()[_currentWordInDict];
                if (!_userWord.Contains(_word))
                {
                    for (int row = 0; row < _width; row++)
                        for (int column = 0; column < _heigth; column++)
                            _binaryMatrixLetters[row, column] = 0;

                    bool tl = true;
                    ////Цикл по слову из словаря
                    for (int k = 0; k < _word.Length; k++)
                    {
                        //// Цикл по самому массиву.
                        for (int i = 0; i < _width; i++)
                        {
                            for (int j = 0; j < _heigth; j++)
                            {
                                if (_gameBoard.CellPool[i, j] == string.Empty)
                                {
                                    for (_ai = 0; _ai < _width; _ai++)
                                        for (_aj = 0; _aj < _heigth; _aj++)
                                            _strings[_ai, _aj] = _gameBoard.CellPool[_ai, _aj];

                                    _gameBoard.SetCellValue(_word.Substring(k, 1), i, j);
                                    _isAcceptWord = true;
                                    _bottomNumber = k; // С какой буквы подставляли для поиска в конец слова
                                    _topNumber = k; // С какой буквы подставляли для поиска в начало слова
                                    _ai = i;
                                    _aj = j;

                                    for (int i1 = 0; i1 < _width; ++i1)
                                        for (int j1 = 0; j1 < _heigth; ++j1)
                                            _binaryMatrixLetters[i1, j1] = 0;

                                    _binaryMatrixLetters[i, j] = 1;
                                    for (_bottomNumber = _bottomNumber + 1; _bottomNumber < _word.Length; _bottomNumber++)
                                    {
                                        if (CheckLetter(_bottomNumber))
                                            continue;
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    _ai = i;
                                    _aj = j;
                                    if (_isAcceptWord)
                                    {
                                        for (_topNumber = _topNumber - 1; _topNumber >= 0; _topNumber--)
                                            if (CheckLetter(_topNumber))
                                                continue;
                                            else
                                            {
                                                break;
                                            }

                                        if (_isAcceptWord)
                                        {
                                            res.Add(_word);
                                            for (_ai = 0; _ai < 5; _ai++)
                                                for (_aj = 0; _aj < 5; _aj++)
                                                    _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);

                                            tl = false;
                                            break;   //// Слово подошло переходим к следующему слову
                                        }
                                    }

                                    for (_ai = 0; _ai < _width; _ai++)
                                        for (_aj = 0; _aj < _heigth; _aj++)
                                            _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);  //// восстановили массив, в текущем состоянии.
                                }
                            }

                            if (!tl)
                            {
                                break;
                            }
                        }
                    }

                    _currentWordInDict++;
                }
                else
                {
                    _currentWordInDict++;
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
            _strings = new string[_width, _heigth];
            for (int row = 0; row < _width; row++)
            {
                for (int column = 0; column < _heigth; column++)
                {
                    _strings[row, column] = string.Empty;
                }
            }
            _binaryMatrixLetters = new int[_width, _heigth];

            for (int row = 0; row < _width; row++)
            {
                for (int column = 0; column < _heigth; column++)
                {
                    _binaryMatrixLetters[row, column] = 0;
                }
            }

            while (_currentWordInDict < _dictionary.GetCount())
            {
                _word = _dictionary.GetDictionary()[_currentWordInDict];
                if (!_userWord.Contains(_word))
                {
                    for (int i = 0; i < _width; ++i)
                    {
                        for (int j = 0; j < _heigth; ++j)
                        {
                            _binaryMatrixLetters[i, j] = 0;
                        }
                    }
                    bool tl = true;
                    for (int k = 0; k < _word.Length; k++)
                    {
                        for (int i = 0; i < _width; i++)
                        {
                            for (int j = 0; j < _heigth; j++)
                            {
                                if (_gameBoard.CellPool[i,j] == string.Empty)
                                {
                                    for (_ai = 0; _ai < _width; _ai++)
                                    {
                                        for (_aj = 0; _aj < _heigth; _aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            _strings[_ai, _aj] = _gameBoard.CellPool[_ai,_aj];
                                        }
                                    }
                                    _gameBoard.SetCellValue(_word.Substring(k, 1), i, j);
                                    _isAcceptWord = true;
                                    _bottomNumber = k; // С какой буквы подставляли для поиска в конец слова
                                    _topNumber = k; // С какой буквы подставляли для поиска в начало слова
                                    _ai = i;
                                    _aj = j;
                                    for (int i1 = 0; i1 < _width; ++i1)
                                    {
                                        for (int j1 = 0; j1 < _heigth; ++j1)
                                        {
                                            _binaryMatrixLetters[i1, j1] = 0;
                                        }
                                    }
                                    _binaryMatrixLetters[i, j] = 1;
                                    for (_bottomNumber = _bottomNumber + 1; _bottomNumber < _word.Length; _bottomNumber++)
                                    {
                                        if (CheckLetter(_bottomNumber))
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
                                    if (_isAcceptWord)
                                    {
                                        for (_topNumber = _topNumber - 1; _topNumber >= 0; _topNumber--)
                                        {
                                            if (CheckLetter(_topNumber))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (_isAcceptWord)
                                        {
                                            return _word;
                                        }
                                    }
                                    for (_ai = 0; _ai < _width; _ai++)
                                    {
                                        for (_aj = 0; _aj < _heigth; _aj++)
                                        {
                                            _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }
                            if (!tl)
                            {
                                break;
                            }
                        }
                    }
                    _currentWordInDict++;
                }
                else
                {
                    _currentWordInDict++;
                }
            }
            return string.Empty;
        }

        public string FindWordWithMaxLength()
        {
            _strings = new string[_width, _heigth];

            for (int row = 0; row < _width; row++)
            {
                for (int column = 0; column < _heigth; column++)
                {
                    _strings[row, column] = string.Empty;
                }
            }
            _binaryMatrixLetters = new int[_width, _heigth];

            for (int row = 0; row < _width; row++)
            {
                for (int column = 0; column < _heigth; column++)
                {
                    _binaryMatrixLetters[row, column] = 0;
                }
            }
            List<string> dict = FindWords();
            _currentWordInDict = 0;
            while (_currentWordInDict < dict.Count)
            {
                _word = dict[_currentWordInDict];
                if (!_userWord.Contains(_word) && _word.Length == MaxLength(dict))
                {
                    for (int i = 0; i < _width; ++i)
                    {
                        for (int j = 0; j < _heigth; ++j)
                        {
                            _binaryMatrixLetters[i, j] = 0;
                        }
                    }

                    bool tl = true;
                    for (int k = 0; k < _word.Length; k++)
                    {
                        for (int i = 0; i < _width; i++)
                        {
                            for (int j = 0; j < _heigth; j++)
                            {
                                if (_gameBoard.CellPool[i,j]== string.Empty)
                                {
                                    for (_ai = 0; _ai < _width; _ai++)
                                    {
                                        for (_aj = 0; _aj < _heigth; _aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            _strings[_ai, _aj] = _gameBoard.CellPool[_ai,_aj];
                                        }
                                    }
                                    _gameBoard.SetCellValue(_word.Substring(k, 1), i, j);
                                    _isAcceptWord = true;
                                    _bottomNumber = k; // С какой буквы подставляли для поиска в конец слова
                                    _topNumber = k; // С какой буквы подставляли для поиска в начало слова
                                    _ai = i;
                                    _aj = j;
                                    for (int i1 = 0; i1 < _width; ++i1)
                                    {
                                        for (int j1 = 0; j1 < _heigth; ++j1)
                                        {
                                            _binaryMatrixLetters[i1, j1] = 0;
                                        }
                                    }
                                    _binaryMatrixLetters[i, j] = 1;
                                    for (_bottomNumber = _bottomNumber + 1; _bottomNumber < _word.Length; _bottomNumber++)
                                    {
                                        if (CheckLetter(_bottomNumber))
                                            continue;
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    _ai = i;
                                    _aj = j;
                                    if (_isAcceptWord)
                                    {
                                        for (_topNumber = _topNumber - 1; _topNumber >= 0; _topNumber--)
                                        {
                                            if (CheckLetter(_topNumber))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (_isAcceptWord)
                                        {
                                            return _word;
                                        }
                                    }
                                    for (_ai = 0; _ai < _width; _ai++)
                                    {
                                        for (_aj = 0; _aj < _heigth; _aj++)
                                        {
                                            _gameBoard.SetCellValue(_strings[_ai, _aj], _ai, _aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }
                            if (!tl)
                            {
                                break;
                            }
                        }
                    }
                    _currentWordInDict++;
                }
                else
                {
                    _currentWordInDict++;
                }
            }
            return string.Empty;
        }

        ////Определяет, есть ли буква на поле сверху или снизу или влева или справа текущей ячейки
        public bool CheckLetter(int index)
        {
            string letter = _word.Substring(index, 1);

            if (_ai + 1 != _width)
            {
                if ((_gameBoard.CellPool[_ai + 1, _aj] == letter)
                        && (_binaryMatrixLetters[_ai + 1, _aj] != 1))
                {
                    _binaryMatrixLetters[_ai + 1, _aj] = 1;
                    _ai++;
                    return true;
                }
            }
            if (_ai - 1 != -1)
            {
                if ((_gameBoard.CellPool[_ai - 1, _aj] == letter)
                        && (_binaryMatrixLetters[_ai - 1, _aj] != 1))
                {
                    _binaryMatrixLetters[_ai - 1, _aj] = 1;
                    _ai--;
                    return true;
                }
            }
            if (_aj + 1 != _heigth)
            {
                if ((_gameBoard.CellPool[_ai, _aj + 1] == letter)
                        && (_binaryMatrixLetters[_ai, _aj + 1] != 1))
                {
                    _binaryMatrixLetters[_ai, _aj + 1] = 1;
                    _aj++;
                    return true;
                }
            }
            if (_aj - 1 != -1)
            {
                if ((_gameBoard.CellPool[_ai, _aj - 1] == letter)
                        && (_binaryMatrixLetters[_ai, _aj - 1] != 1))
                {
                    _binaryMatrixLetters[_ai, _aj - 1] = 1;
                    _aj--;
                    return true;
                }
            }

            _isAcceptWord = false; // Если не совпало ни одно условие выше то это слово не подходит.
            return false;
        }
    }
}

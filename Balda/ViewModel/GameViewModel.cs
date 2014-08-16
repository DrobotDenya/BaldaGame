using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Balda.Data;
using Balda.View;

namespace Balda.ViewModel
{
    public class GameViewModel
    {
        private readonly GameManager _gameManager = new GameManager();
        private readonly GameWindow _gameWindow;
        private Cell[,] _cellArray;
        private readonly List<Cell> _keysList = new List<Cell>();
        private string _lastKey = "";
        private Cell _lastCell = null;
        private bool _isCurrentPlayerMove = false;
        private string _currentWord = "";

        public GameViewModel(GameWindow gameWindow)
        {
            MissbtnCommand = new Command(new Action<object>(Miss));
            CancelbtnCommand = new Command(new Action<object>(Cancel));
            SubmitbtnCommand = new Command(new Action<object>(Submit));
            _gameWindow = gameWindow;
            StartGame();
        }

        public void CellMouseDown(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (_gameWindow.Keyboard.Children.Contains(cell))
            {
                _lastKey = cell.GetText();
                //DisableBoardCell();
                ////disableBoardCellWithLetter();

            }

            if (_gameWindow.Board.Children.Contains(cell))
            {
                if (!String.IsNullOrEmpty(_lastKey))
                {
                    if (String.IsNullOrEmpty(cell.GetText()))
                    {
                        _lastCell = cell;
                        _lastCell.SetText(_lastKey);
                        //_lastCell.Background = Brushes.Purple;
                        _lastKey = string.Empty;

                        SetEnableKeyboard(false);

                        _isCurrentPlayerMove = true;
                        //setEnabledButtons(true);
                        //buttonsPanel.getComponent(1).setEnabled(false);
                    }
                }
                else if (_isCurrentPlayerMove)
                {
                    //// Иначе если игрок выделяет слово

                    if (!String.IsNullOrEmpty(cell.GetText()))
                    {
                        cell.IsEnabled = false;
                        _currentWord += cell.GetText();
                        ////cell.BorderBrush = Brushes.Purple;
                    }

                    ////if (!_lastCell.IsEnabled)
                    ////{
                    ////    buttonsPanel.getComponent(1).setEnabled(true);
                    ////}
                }
            }
        }

        private void StartGame()
        {
            _gameManager.SetBotsComplexity(Settings.Setting.GetBotComplexity());

            _gameManager.Start();

            CreateCellForBoard();
            CreateKeysForKeyBoard();

            _gameWindow.DrawBoardCell(_cellArray);
            _gameWindow.DrawKeys(_keysList);

            ReloadDataBoard();
            ReloadKeyBoard();

            _gameWindow.SetNamePlayers(User.SharedUser.Nickname, _gameManager.Players()[1].GetNickname());
        }

        private void CreateKeysForKeyBoard()
        {
            for (int i = 0; i < _gameManager.GetKeyBoard().GetKeys().Count; i++)
            {
                Cell cell = new Cell();
                _keysList.Add(cell);
            }
        }

        private void CreateCellForBoard()
        {
            Cell cell;
            _cellArray = new Cell[_gameManager.GetSizeBoard(), _gameManager.GetSizeBoard()];
            for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                {
                    cell = new Cell();
                    _cellArray[i, j] = cell;
                    cell.MouseDown += CellMouseDown;
                }
            }
        }

        private void DisableBoardCell()
        {
            for (int i = 0; i < _gameManager.GetGameBoard().Width(); i++)
            {
                for (int j = 0; j < _gameManager.GetGameBoard().Heigth(); j++)
                {
                    if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(i, j)))
                    {
                        if (!EnableCellOnBoard(i, j))
                        {
                            _cellArray[i, j].IsEnabled = false;
                            ////_cellArray[i, j].BorderBrush = Brushes.Red;

                        }
                    }

                }
            }
        }

        /* Определяет существует ли сверху, снизу, 
        слева, справа ячейка с буквой*/
        private bool EnableCellOnBoard(int i, int j)
        {
            if (i + 1 != _gameManager.GetGameBoard().Width())
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(i + 1, j)))
                {
                    return true;
                }
            }
            if (i - 1 != -1)
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(i - 1, j)))
                {
                    return true;
                }
            }
            if (j + 1 != _gameManager.GetGameBoard().Heigth())
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(i, j + 1)))
                {
                    return true;
                }
            }
            if (j - 1 != -1)
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(i, j - 1)))
                {
                    return true;
                }
            }

            return false;

        }

        ////Активирует/деактевирует клавиатуру
        private void SetEnableKeyboard(bool a)
        {
            foreach (Cell cell in _keysList)
            {
                cell.IsEnabled = a;
            }

        }

        //Активирует/деактевирует поле
        private void SetEnableBoard(bool a)
        {
            for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                {
                    _cellArray[i, j].IsEnabled = a;

                }
            }
        }

        /*Возвращает окно в состояние начала хода игрока */
        private void UndoWindow()
        {
            SetEnableBoard(true);
            SetEnableKeyboard(true);
            CreateKeysForKeyBoard();
            ////createButtons();
            _currentWord = "";
            if (_lastCell != null)
            {
                //// lastButton.setBackground(keysPanel.getComponent(0).getBackground());
                _lastCell.SetText("");
                _lastCell = null;
            }
        }

        private void UpdateListBox()
        {
            _gameWindow.UpdateListP1(_gameManager.Players()[0].GetWordsList());
            _gameWindow.UpdateListP2(_gameManager.Players()[1].GetWordsList());
        }

        private void UpdateValue()
        {
            _gameWindow.UpdateValueP1(_gameManager.Players()[0].GetPoints());
            _gameWindow.UpdateValueP2(_gameManager.Players()[1].GetPoints());

        }

        private void ExchangePlayer()
        {
            if (_gameManager.DetermineWinner() != null)
            {
                MessageBox.Show("Выиграл игрок " + _gameManager.DetermineWinner().GetNickname() + " Конец игры!");
            }
            else
            {
                _gameManager.ExchangePlayer();
                UpdateListBox();
                UpdateValue();
            }

            ReloadDataBoard();
        }

        private void ReloadDataBoard()
        {
            for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                {
                    _cellArray[i, j].SetText(_gameManager.GetGameBoard().GetCellValue(i, j));
                }
            }

        }

        private void ReloadKeyBoard()
        {
            int i = 0;
            foreach (Cell cell in _keysList)
            {
                string a = (string)(_gameManager.GetKeyBoard().GetKeys())[i];
                cell.SetText(a);
                i++;
                cell.MouseDown += CellMouseDown;
            }
        }

        #region Button Command
        private void Miss(object obj)
        {
            SetEnableBoard(true);
            UndoWindow();
            ExchangePlayer();
        }

        private void Cancel(object obj)
        {
            SetEnableBoard(true);
            UndoWindow();
        }

        private void Submit(object obj)
        {
            if (!_gameManager.WordIsUsed(_currentWord))
            {
                if (_gameManager.GetDictionary().IsExist(_currentWord))
                {
                   _gameManager.ActivePlayer().AddWord(_currentWord);
                    _gameManager.UpdateUsedWords();
                    UpdateListBox();
                    UpdateValue();
                    CreateKeysForKeyBoard();
                    _currentWord = "";

                    if (_lastCell != null)
                    {
                        for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
                        {
                            for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                            {
                                _gameManager.GetGameBoard().SetCellValue(_cellArray[i, j].GetText(), i, j);

                            }
                        }
                        _lastCell = null;
                    }
                    MessageBox.Show("Конец хода!");
                    ExchangePlayer();
                }
                else
                {
                    if (String.IsNullOrEmpty(_currentWord))
                    {
                        MessageBox.Show("Слово не выделено!");
                    }
                    else
                    {
                        MessageBox.Show("Слово " + "'" + _currentWord + "'" + " не найдено в словаре! Попробуйте еще раз!");
                    }
                    UndoWindow();
                }
                UndoWindow();
            }
            else
            {
                MessageBox.Show("Слово " + _currentWord + " уже использувалось в игре! Попробуйте еще раз!");
                UndoWindow();
            }
        }

        public ICommand MissbtnCommand { get; set; }

        public ICommand CancelbtnCommand { get; set; }

        public ICommand SubmitbtnCommand { get; set; }
        #endregion
    }
}



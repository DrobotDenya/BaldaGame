using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Balda.Data;

namespace Balda
{
    public partial class GameWindow : Window
    {

        private GameManager _gameManager = new GameManager();
        private Cell[,] _cellArray;
        private List<Cell> _keysList = new List<Cell>();
        private string _lastKey = "";
        private Cell _lastCell = null;
        private bool _isCurrentPlayerMove = false;
        private string _currentWord = "";

        public GameWindow()
        {
            InitializeComponent();

            _gameManager.SetBotsComplexity(Settings.Setting.GetBotComplexity());

            _gameManager.Start();

            CreateCellForBoard();
            ////SetEnableBoard(false);

            CreateKeysForKeyBoard();
            ////SetEnableKeyboard(false);
        }

        public void CellMouseDown(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (Keyboard.Children.Contains(cell))
            {
                _lastKey = cell.GetText();
                DisableBoardCell();
                ////disableBoardCellWithLetter();

            }

            if (board.Children.Contains(cell))
            {
                if (_lastKey.Equals("") == false)
                {
                    if (cell.GetText().Equals(""))
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
                { //// Иначе если игрок выделяет слово

                    if (!cell.GetText().Equals(""))
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadDataBoard();
            ReloadKeyBoard();
            TitleP1.Text = User.SharedUser.GetNickname();
            TitleP2.Text = _gameManager.Players()[1].GetNickname();
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
                    board.Children.Add(_cellArray[i, j]);
                }
            }
        }

        private void CreateKeysForKeyBoard()
        {
            for (int i = 0; i < _gameManager.GetKeyBoard().GetKeys().Count; i++)
            {
                Cell cell = new Cell();
                _keysList.Add(cell);
                Keyboard.Children.Add(cell);
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
        ////Активирует/деактевирует клавиатуру
        private void SetEnableKeyboard(bool a)
        {
            foreach (Cell cell in _keysList)
            {
                cell.IsEnabled = a;
            }

        }

        ////Активирует/деактевирует поле
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

        /* Определяет существует ли сверху, снизу, 
        слева, справа ячейка с буквой*/
        private bool EnableCellOnBoard(int i, int j)
        {
            if (i + 1 != _gameManager.GetGameBoard().Width())
            {
                if (_gameManager.GetGameBoard().GetCellValue(i + 1, j) != "")
                {
                    return true;
                }
            }
            if (i - 1 != -1)
            {
                if (_gameManager.GetGameBoard().GetCellValue(i - 1, j) != "")
                {
                    return true;
                }
            }
            if (j + 1 != _gameManager.GetGameBoard().Heigth())
            {
                if (_gameManager.GetGameBoard().GetCellValue(i, j + 1) != "")
                {
                    return true;
                }
            }
            if (j - 1 != -1)
            {
                if (_gameManager.GetGameBoard().GetCellValue(i, j - 1) != "")
                {
                    return true;
                }
            }

            return false;

        }

        /* Деактивирует ячейки, которые не содержат 
        буквы и в соседних клетках нет буквы*/
        private void DisableBoardCell()
        {
            for (int i = 0; i < _gameManager.GetGameBoard().Width(); i++)
            {
                for (int j = 0; j < _gameManager.GetGameBoard().Heigth(); j++)
                {
                    if (_gameManager.GetGameBoard().GetCellValue(i, j) == "")
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

        private void BtnMissClick(object sender, RoutedEventArgs e)
        {
            SetEnableBoard(true);
            UndoWindow();
            ExchangePlayer();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            SetEnableBoard(true);
            UndoWindow();

        }

        private void BtnSubmitClick(object sender, RoutedEventArgs e)
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
                    if (_currentWord.Equals(""))
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

        private void UpdateListBox()
        {
            ListBoxP1.Items.Clear();
            List<string> words = _gameManager.Players()[0].GetWordsList();
            foreach (string word in words)
            {
                if (word != _gameManager.GetUsedWord()[0])
                {
                    ListBoxP1.Items.Add(word);
                }
            }
            ListBoxP2.Items.Clear();
            List<string> wordss = _gameManager.Players()[1].GetWordsList();
            foreach (string word in wordss)
            {
                if (word != _gameManager.GetUsedWord()[0])
                {
                    ListBoxP2.Items.Add(word);
                }
            }
        }

        private void UpdateValue()
        {
            Value1.Content = _gameManager.Players()[0].GetPoints();
            Value2.Content = _gameManager.Players()[1].GetPoints();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            _gameManager.ClearUsedWords();
        }
    }
}

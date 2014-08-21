using System;
using System.Collections.ObjectModel;
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
        /// <summary>
        /// Список ячеек поля
        /// </summary>
        private Cell[][] _cellArray;
        /// <summary>
        /// Список ячеек клавиатуры
        /// </summary>
        private readonly Collection<Cell> _keysList = new Collection<Cell>();
        /// <summary>
        /// Значение выделеной ячейки
        /// </summary>
        private string _lastKey = "";
        /// <summary>
        /// Выделеная ячейка
        /// </summary>
        private Cell _lastCell = null;
        private bool _isCurrentPlayerMove = false;
        /// <summary>
        /// Выделеное слово
        /// </summary>
        private string _currentWord = "";

        public GameViewModel(GameWindow gameWindow)
        {
            MissbtnCommand = new Command(new Action<object>(Miss));
            CancelbtnCommand = new Command(new Action<object>(Cancel));
            SubmitbtnCommand = new Command(new Action<object>(Submit));
            _gameWindow = gameWindow;
            StartGame();
        }
        /// <summary>
        /// Вызывается при выделении ячеейки поля или клавиатуры
        /// </summary>
        public void CellMouseDown(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (_gameWindow.Keyboard.Children.Contains(cell))
            {
                _lastKey = cell.Text();
                //DisableBoardCell();
                ////disableBoardCellWithLetter();

            }

            if (_gameWindow.Board.Children.Contains(cell))
            {
                if (!String.IsNullOrEmpty(_lastKey))
                {
                    if (String.IsNullOrEmpty(cell.Text()))
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

                    if (!String.IsNullOrEmpty(cell.Text()))
                    {
                        cell.IsEnabled = false;
                        _currentWord += cell.Text();
                        ////cell.BorderBrush = Brushes.Purple;
                    }

                    ////if (!_lastCell.IsEnabled)
                    ////{
                    ////    buttonsPanel.getComponent(1).setEnabled(true);
                    ////}
                }
            }
        }
        /// <summary>
        /// Начало новй игры;
        /// Генерация всех елементов игры
        /// </summary>
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
        /// <summary>
        /// Создание ячеек для клавиатуры
        /// </summary>
        private void CreateKeysForKeyBoard()
        {
            for (int i = 0; i < _gameManager.GetKeyBoard().GetKeys().Count; i++)
            {
                Cell cell = new Cell();
                _keysList.Add(cell);
            }
        }
        /// <summary>
        /// Создание ячеек для поля
        /// </summary>
        private void CreateCellForBoard()
        {
            Cell cell;
            _cellArray = new Cell[_gameManager.GetSizeBoard()][];
            for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
            {
                _cellArray[i] = new Cell[_gameManager.GetSizeBoard()];
                for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                {
                    cell = new Cell();
                    _cellArray[i][j] = cell;
                    cell.MouseDown += CellMouseDown;
                }
            }
        }
        /// <summary>
        /// Определяет существует ли сверху, снизу, слева, справа ячейка с буквой
        /// </summary>
        /// /// <param name="row">
        /// Координата рядка
        /// </param>
        /// /// <param name="column">
        /// Координата колонки
        /// </param>
        protected bool EnableCellOnBoard(int row, int column)
        {
            if (row++ != _gameManager.GetGameBoard().Width())
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(row++, column)))
                {
                    return true;
                }
            }
            if (row-- != -1)
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(row--, column)))
                {
                    return true;
                }
            }
            if (column++ != _gameManager.GetGameBoard().Heigth())
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(row, column++)))
                {
                    return true;
                }
            }
            if (column-- != -1)
            {
                if (String.IsNullOrEmpty(_gameManager.GetGameBoard().GetCellValue(row, column--)))
                {
                    return true;
                }
            }

            return false;

        }
        /// <summary>
        /// Активирует/деактевирует клавиатуру
        /// </summary>
        private void SetEnableKeyboard(bool a)
        {
            foreach (Cell cell in _keysList)
            {
                cell.IsEnabled = a;
            }

        }
        /// <summary>
        /// Активирует/деактевирует поле
        /// </summary>
        private void SetEnableBoard(bool a)
        {
            for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                {
                    _cellArray[i][j].IsEnabled = a;

                }
            }
        }
        /// <summary>
        /// Возвращает окно в состояние начала хода игрока
        /// </summary>
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
        /// <summary>
        /// Обновление списка  первого игрока
        /// </summary>
        private void UpdateListBox()
        {
            _gameWindow.UpdateListP1(_gameManager.Players()[0].GetWordsList());
            _gameWindow.UpdateListP2(_gameManager.Players()[1].GetWordsList());
        }
        /// <summary>
        /// Обновление списка игрока
        /// </summary>
        private void UpdateValue()
        {
            _gameWindow.UpdateValueP1(_gameManager.Players()[0].GetPoints());
            _gameWindow.UpdateValueP2(_gameManager.Players()[1].GetPoints());

        }
        /// <summary>
        /// Передача хода второму игроку
        /// </summary>
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
        /// <summary>
        /// Обновление ячеек поля
        /// </summary>
        private void ReloadDataBoard()
        {
            for (int i = 0; i < _gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < _gameManager.GetSizeBoard(); j++)
                {
                    _cellArray[i][j].SetText(_gameManager.GetGameBoard().GetCellValue(i, j));
                }
            }

        }
        /// <summary>
        /// Обновление ячеек клавиатуры
        /// </summary>
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
        /// <summary>
        /// Пропуск хода
        /// </summary>
        private void Miss(object obj)
        {
            SetEnableBoard(true);
            UndoWindow();
            ExchangePlayer();
        }
        /// <summary>
        /// Отмена хода
        /// </summary>
        private void Cancel(object obj)
        {
            SetEnableBoard(true);
            UndoWindow();
        }
        /// <summary>
        /// Отправка слова
        /// </summary>
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
                                _gameManager.GetGameBoard().SetCellValue(_cellArray[i][j].Text(), i, j);

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



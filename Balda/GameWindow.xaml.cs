using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Balda.Data;

namespace Balda
{
    public partial class GameWindow : Window
    {

        GameManager gameManager = new GameManager();
        Cell[,] cellArray;
        List<Cell> keysList = new List<Cell>();
        string lastKey = "";
        Cell lastCell = null;
        bool isCurrentPlayerMove = false;
        string currentWord = "";

        public GameWindow()
        {
            InitializeComponent();

            gameManager.SetBotsComplexity(Settings.Setting.GetBotComplexity());

            gameManager.Start();

            CreateCellForBoard();
            //SetEnableBoard(false);

            CreateKeysForKeyBoard();
            //SetEnableKeyboard(false);


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadDataBoard();
            ReloadKeyBoard();
            TitleP1.Text = User.SharedUser.GetNickname();
            TitleP2.Text = gameManager.Players()[1].GetNickname();
        }

        void CreateCellForBoard()
        {
            Cell cell;
            cellArray = new Cell[gameManager.GetSizeBoard(), gameManager.GetSizeBoard()];
            for (int i = 0; i < gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.GetSizeBoard(); j++)
                {
                    cell = new Cell();
                    cellArray[i, j] = cell;
                    cell.MouseDown += Cell_MouseDown;
                    board.Children.Add(cellArray[i, j]);

                }
            }
        }
        void CreateKeysForKeyBoard()
        {
            for (int i = 0; i < gameManager.GetKeyBoard().GetKeys().Count; i++)
            {
                Cell cell = new Cell();
                keysList.Add(cell);
                Keyboard.Children.Add(cell);
            }
        }
        void ReloadKeyBoard()
        {
            int i = 0;
            foreach (Cell cell in keysList)
            {
                string a = (string)(gameManager.GetKeyBoard().GetKeys())[i];
                cell.SetText(a);
                i++;
                cell.MouseDown += Cell_MouseDown;


            }

        }
        void ReloadDataBoard()
        {
            for (int i = 0; i < gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.GetSizeBoard(); j++)
                {
                    cellArray[i, j].SetText(gameManager.GetGameBoard().GetCellValue(i, j));
                }
            }

        }



        /*Возвращает окно в состояние начала хода игрока */
        private void UndoWindow()
        {
            SetEnableBoard(true);
            SetEnableKeyboard(true);
            CreateKeysForKeyBoard();
            //createButtons();
            currentWord = "";
            if (lastCell != null)
            {
                // lastButton.setBackground(keysPanel.getComponent(0).getBackground());
                lastCell.SetText("");
                lastCell = null;
            }
        }
        //Активирует/деактевирует клавиатуру
        void SetEnableKeyboard(bool a)
        {
            foreach (Cell cell in keysList)
            {
                cell.IsEnabled = a;
            }

        }

        //Активирует/деактевирует поле
        void SetEnableBoard(bool a)
        {
            for (int i = 0; i < gameManager.GetSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.GetSizeBoard(); j++)
                {
                    cellArray[i, j].IsEnabled = a;

                }
            }
        }

        /* Определяет существует ли сверху, снизу, 
        слева, справа ячейка с буквой*/
        private bool EnableCellOnBoard(int i, int j)
        {
            if (i + 1 != gameManager.GetGameBoard().Width())
            {
                if (gameManager.GetGameBoard().GetCellValue(i + 1, j) != "")
                {
                    return true;
                }
            }
            if (i - 1 != -1)
            {
                if (gameManager.GetGameBoard().GetCellValue(i - 1, j) != "")
                {
                    return true;
                }
            }
            if (j + 1 != gameManager.GetGameBoard().Heigth())
            {
                if (gameManager.GetGameBoard().GetCellValue(i, j + 1) != "")
                {
                    return true;
                }
            }
            if (j - 1 != -1)
            {
                if (gameManager.GetGameBoard().GetCellValue(i, j - 1) != "")
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
            for (int i = 0; i < gameManager.GetGameBoard().Width(); i++)
            { // Цикл по самому массиву.
                for (int j = 0; j < gameManager.GetGameBoard().Heigth(); j++)
                {
                    if (gameManager.GetGameBoard().GetCellValue(i, j) == "")
                    {
                        if (!EnableCellOnBoard(i, j))
                        {
                            cellArray[i, j].IsEnabled = false;
                            //cellArray[i, j].BorderBrush = Brushes.Red;

                        }
                    }

                }
            }
        }

        private void btnMiss_Click(object sender, RoutedEventArgs e)
        {
            SetEnableBoard(true);
            UndoWindow();
            ExchangePlayer();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetEnableBoard(true);
            UndoWindow();

        }

        public void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (Keyboard.Children.Contains(cell))
            {
                lastKey = cell.GetText();
                DisableBoardCell();
                //disableBoardCellWithLetter();

            }


            if (board.Children.Contains(cell))
            {
                if (lastKey.Equals("") == false)
                {
                    if (cell.GetText().Equals(""))
                    {
                        lastCell = cell;
                        lastCell.SetText(lastKey);
                        //lastCell.Background = Brushes.Purple;
                        lastKey = "";

                        SetEnableKeyboard(false);

                        isCurrentPlayerMove = true;

                        //Активация/деактивация необходимых кнопок игрока
                        //setEnabledButtons(true);
                        //buttonsPanel.getComponent(1).setEnabled(false);
                        // TODO: Активация/деактивация нужных ячеек поля
                    }

                }
                else if (isCurrentPlayerMove)
                { // Иначе если игрок выделяет слово

                    if (!cell.GetText().Equals(""))
                    {
                        cell.IsEnabled = false; ;
                        currentWord += cell.GetText();
                        //cell.BorderBrush = Brushes.Purple;
                    }

                    //if (!lastCell.IsEnabled)
                    //{
                    //    buttonsPanel.getComponent(1).setEnabled(true);
                    //}
                }
            }
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!gameManager.WordIsUsed(currentWord))
            {
                if (gameManager.GetDictionary().IsExist(currentWord))
                {
                    gameManager.ActivePlayer().AddWord(currentWord);
                    gameManager.UpdateUsedWords();
                    UpdateListBox();
                    UpdateValue();
                    CreateKeysForKeyBoard();
                    currentWord = "";

                    if (lastCell != null)
                    {
                        for (int i = 0; i < gameManager.GetSizeBoard(); i++)
                        {
                            for (int j = 0; j < gameManager.GetSizeBoard(); j++)
                            {
                                gameManager.GetGameBoard().SetCellValue(cellArray[i, j].GetText(), i, j);

                            }
                        }
                        lastCell = null;
                    }
                    MessageBox.Show("Конец хода!");
                    ExchangePlayer();



                }
                else
                {
                    if (currentWord.Equals(""))
                    {
                        MessageBox.Show("Слово не выделено!");
                    }
                    else
                    {
                        MessageBox.Show("Слово " + "'" + currentWord + "'" + " не найдено в словаре! Попробуйте еще раз!");
                    }
                    UndoWindow();
                }
                UndoWindow();
            }
            else
            {
                MessageBox.Show("Слово " + currentWord + " уже использувалось в игре! Попробуйте еще раз!");
                UndoWindow();
            }

        }


        void ExchangePlayer()
        {
            if (gameManager.DetermineWinner() != null)
            {
                MessageBox.Show("Выиграл игрок " + gameManager.DetermineWinner().GetNickname() + " Конец игры!");
            }
            else
            {

                gameManager.ExchangePlayer();
                UpdateListBox();
                UpdateValue();

            }

            ReloadDataBoard();
        }

        void UpdateListBox()
        {


            ListBoxP1.Items.Clear();
            List<string> words = gameManager.Players()[0].GetWordsList();
            foreach (string word in words)
            {
                if (word != gameManager.GetUsedWord()[0])
                {
                    ListBoxP1.Items.Add(word);
                }
            }


            ListBoxP2.Items.Clear();
            List<string> wordss = gameManager.Players()[1].GetWordsList();
            foreach (string word in wordss)
            {
                if (word != gameManager.GetUsedWord()[0])
                {
                    ListBoxP2.Items.Add(word);
                }
            }




        }

        void UpdateValue()
        {

            Value1.Content = gameManager.Players()[0].GetPoints();
            Value2.Content = gameManager.Players()[1].GetPoints();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            gameManager.ClearUsedWords();
        }
    }
}

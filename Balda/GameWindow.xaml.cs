using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Collections;
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

            createCellForBoard();
            //setEnableBoard(false);

            createKeysForKeyBoard();
            //setEnableKeyboard(false);

           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reloadDataBoard();
            reloadKeyBoard();
            titleP1.Text = User.SharedUser.GetNickname();
            titleP2.Text = gameManager.Players()[1].GetNickname();
        }

        void createCellForBoard()
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
        void createKeysForKeyBoard()
        {
            for (int i = 0; i < gameManager.GetKeyBoard().GetKeys().Count; i++)
            {
                Cell cell = new Cell();
                keysList.Add(cell);
                keyboard.Children.Add(cell);
            }
        }
        void reloadKeyBoard()
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
        void reloadDataBoard()
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
        private void undoWindow()
        {
            setEnableBoard(true);
            setEnableKeyboard(true);
            createKeysForKeyBoard();
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
        void setEnableKeyboard(bool a)
        {
            foreach (Cell cell in keysList)
            {
                cell.IsEnabled = a;
            }

        }

        //Активирует/деактевирует поле
        void setEnableBoard(bool a)
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
        private bool enableCellOnBoard(int i, int j)
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
        private void disableBoardCell()
        {
            for (int i = 0; i < gameManager.GetGameBoard().Width(); i++)
            { // Цикл по самому массиву.
                for (int j = 0; j < gameManager.GetGameBoard().Heigth(); j++)
                {
                    if (gameManager.GetGameBoard().GetCellValue(i, j) == "")
                    {
                        if (!enableCellOnBoard(i, j))
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
            setEnableBoard(true);
            undoWindow();
            exchangePlayer();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setEnableBoard(true);
            undoWindow();
            
        }

        public void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (keyboard.Children.Contains(cell))
            {
                lastKey = cell.GetText();
                disableBoardCell();
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

                        setEnableKeyboard(false);

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
                    updateListBox();
                    updateValue();
                    createKeysForKeyBoard();
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
                    exchangePlayer();
                   
                    

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
                    undoWindow();
                }
                undoWindow();
            }
            else
            {
                MessageBox.Show("Слово " + currentWord + " уже использувалось в игре! Попробуйте еще раз!");
                undoWindow();
            }

        }


        void exchangePlayer()
        {
            if (gameManager.DetermineWinner() != null)
            {
                MessageBox.Show("Выиграл игрок " + gameManager.DetermineWinner().GetNickname() + " Конец игры!");
            }
            else
            {
                
                gameManager.ExchangePlayer();
                updateListBox();
                updateValue();

            }

            reloadDataBoard();
        }

        void updateListBox()
        {

            
                listBoxP1.Items.Clear();
                List<string> words = gameManager.Players()[0].GetWordsList();
                foreach (string word in words)
                {
                    if (word != gameManager.GetUsedWord()[0])
                    {
                        listBoxP1.Items.Add(word);
                    }
                }


                    listBoxP2.Items.Clear();
                    List<string> wordss = gameManager.Players()[1].GetWordsList();
                    foreach (string word in wordss)
                    {
                        if (word != gameManager.GetUsedWord()[0])
                        {
                            listBoxP2.Items.Add(word);
                        }
                    }
                
 
           
 
        }

        void updateValue()
        {
            
            value1.Content = gameManager.Players()[0].GetPoints();
            value2.Content = gameManager.Players()[1].GetPoints();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            gameManager.ClearUsedWords();
        }
    }   
}

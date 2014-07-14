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
        List<Cell> newWord = new List<Cell>();
        string lastKey = "";
        Cell lastCell = null;
        bool isCurrentPlayerMove = false;
        string currentWord = "";

        public GameWindow()
        {
            InitializeComponent();

            createCellForBoard();
            createKeysForKeyBoard();

            for (int i = 0; i < gameManager.getSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.getSizeBoard(); j++)
                {
                    board.Children.Add(cellArray[i, j]);

                }
            }

            foreach (Cell cell in keysList)
            {
                keyboard.Children.Add(cell);
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gameManager.genaretaBoard();
            reloadDataBoard();
            reloadKeyBoard();

        }

        void createCellForBoard()
        {
            Cell cell;
            cellArray = new Cell[gameManager.getSizeBoard(), gameManager.getSizeBoard()];
            for (int i = 0; i < gameManager.getSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.getSizeBoard(); j++)
                {
                    cell = new Cell();
                    cellArray[i, j] = cell;
                    cell.MouseDown += Cell_MouseDown;

                }
            }
        }
        void createKeysForKeyBoard()
        {
            for (int i = 0; i < gameManager.getKeyBoard().getKeys().Count; i++)
            {
                Cell cell = new Cell();
                keysList.Add(cell);
            }
        }
        void reloadKeyBoard()
        {
            int i = 0;
            foreach (Cell cell in keysList)
            {
                string a = (string)(gameManager.getKeyBoard().getKeys())[i];
                cell.setText(a);
                i++;
                cell.MouseDown += Cell_MouseDown;


            }

        }
        void reloadDataBoard()
        {
            for (int i = 0; i < gameManager.getSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.getSizeBoard(); j++)
                {
                    cellArray[i, j].setText(gameManager.getGameBoard().getCellValue(i, j));

                }
            }

        }

       // void enableBoardCell()
        //{

        //    for (int i = 0; i < gameManager.getGameBoard().width(); i++)
        //    { // Цикл по самому массиву.
        //        for (int j = 0; j < gameManager.getGameBoard().heigth(); j++)
        //        {
        //            if (gameManager.getGameBoard().getCellValue(i, j) == "")
        //            {
        //                if (!enableCellOnBoard(i, j))
        //                {
        //                    cellArray[i, j].IsEnabled = false;
        //                }
        //            }

        //        }
        //    }
        //}

        //bool isExistEmptyCell()
        //{
        //    foreach (Cell cell in newWord)
        //    {
        //        if (cell.getText() == "")
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        public void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (keyboard.Children.Contains(cell))
            {
                lastKey = cell.getText();
                disableBoardCell();
                //disableBoardCellWithLetter();

            }

           
            if (board.Children.Contains(cell))
            {
                if (lastKey.Equals("") == false)
                {
                    if (cell.getText().Equals(""))
                    {
                        lastCell = cell;
                        lastCell.setText(lastKey);
                        lastCell.Background = Brushes.Purple;
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
                    
                    if (!cell.getText().Equals(""))
                    {
                        cell.IsEnabled = false; ;
                        currentWord += cell.getText();
                        cell.BorderBrush = Brushes.Purple;
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
            if (!gameManager.wordIsUsed(currentWord))
            {
                if (gameManager.getDictionary().isExist(currentWord))
                {
                    listBoxP1.Items.Add(currentWord);

                    currentWord = "";

                    MessageBox.Show("+");

                }
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
            for (int i = 0; i < gameManager.getSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.getSizeBoard(); j++)
                {
                    cellArray[i, j].IsEnabled = false;

                }
            }
        }

        /* Определяет существует ли сверху, снизу, 
        слева, справа ячейка с буквой*/
        private bool enableCellOnBoard(int i, int j)
        {
            if (i + 1 != gameManager.getGameBoard().width())
            {
                if (gameManager.getGameBoard().getCellValue(i + 1, j) != "")
                {
                    return true;
                }
            }
            if (i - 1 != -1)
            {
                if (gameManager.getGameBoard().getCellValue(i - 1, j) != "")
                {
                    return true;
                }
            }
            if (j + 1 != gameManager.getGameBoard().heigth())
            {
                if (gameManager.getGameBoard().getCellValue(i, j + 1) != "")
                {
                    return true;
                }
            }
            if (j - 1 != -1)
            {
                if (gameManager.getGameBoard().getCellValue(i, j - 1) != "")
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
            for (int i = 0; i < gameManager.getGameBoard().width(); i++)
            { // Цикл по самому массиву.
                for (int j = 0; j < gameManager.getGameBoard().heigth(); j++)
                {
                    if (gameManager.getGameBoard().getCellValue(i, j) == "")
                    {
                        if (!enableCellOnBoard(i, j))
                        {
                            cellArray[i, j].IsEnabled = false;

                        }
                    }

                }
            }
        }

        

    }   
}

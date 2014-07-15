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

            gameManager.start();

            createCellForBoard();
            //setEnableBoard(false);

            createKeysForKeyBoard();
            //setEnableKeyboard(false);

           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                    board.Children.Add(cellArray[i, j]);

                }
            }
        }
        void createKeysForKeyBoard()
        {
            for (int i = 0; i < gameManager.getKeyBoard().getKeys().Count; i++)
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
                    Console.WriteLine(i + " " + j + " " + cellArray[i, j].getText());
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
                lastCell.setText("");
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
            for (int i = 0; i < gameManager.getSizeBoard(); i++)
            {
                for (int j = 0; j < gameManager.getSizeBoard(); j++)
                {
                    cellArray[i, j].IsEnabled = a;

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

                    if (!cell.getText().Equals(""))
                    {
                        cell.IsEnabled = false; ;
                        currentWord += cell.getText();
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
            if (!gameManager.wordIsUsed(currentWord))
            {
                if (gameManager.getDictionary().isExist(currentWord))
                {
                    gameManager.activePlayer().addWord(currentWord);
                    gameManager.updateUsedWords();
                    updateListBox();
                    updateValue();
                    createKeysForKeyBoard();
                    currentWord = "";

                    if (lastCell != null)
                    {
                        for (int i = 0; i < gameManager.getSizeBoard(); i++)
                        {
                            for (int j = 0; j < gameManager.getSizeBoard(); j++)
                            {
                                gameManager.getGameBoard().setCellValue(cellArray[i, j].getText(), i, j);

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
            if (gameManager.determineWinner() != null)
            {
                MessageBox.Show("Выиграл игрок " + gameManager.determineWinner().getNickname() + " Конец игры!");
            }
            else
            {
                
                gameManager.exchangePlayer();
                updateListBox();
                updateValue();

            }

            reloadDataBoard();
        }

        void updateListBox()
        {

            
                listBoxP1.Items.Clear();
                List<string> words = gameManager.players()[0].getWordsList();
                foreach (string word in words)
                {
                    if (word != gameManager.getUsedWord()[0])
                    {
                        listBoxP1.Items.Add(word);
                    }
                }


                    listBoxP2.Items.Clear();
                    List<string> wordss = gameManager.players()[1].getWordsList();
                    foreach (string word in wordss)
                    {
                        if (word != gameManager.getUsedWord()[0])
                        {
                            listBoxP2.Items.Add(word);
                        }
                    }
                
 
           
 
        }

        void updateValue()
        {
            
            value1.Content = gameManager.players()[0].getPoints();
            value2.Content = gameManager.players()[1].getPoints();
        }
    }   
}

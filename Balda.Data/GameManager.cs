using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class GameManager
    {
        GameBoard _board = new GameBoard();
        GameKeys _keyBoard = new GameKeys();
        Dictionary _dictionary = new Dictionary();
        List<string> usedWords = new List<string>();
        int size = 5;
        int width = 11;
        int heigth = 3;

        FindWordAlgorithm algorithm;

        public GameManager()
        {
            _board.setSize(size, size);
            algorithm = new FindWordAlgorithm(_board, usedWords);
        }

        public Dictionary getDictionary()
        {
            return _dictionary;
        }

        public void genaretaBoard()
        {
            _board.clear();
            _board.setSize(size, size);

            for (int row = 0; row < _board.heigth(); row++)
            {
                for (int col = 0; col < _board.width(); col++)
                {
                    _board.setCellValue("", row, col);
                }
            }

            string randWord = getDictionary().getRandomWord(size);

            if (randWord != null)
            {
                int middleRow = size / 2;
                for (int i = 0; i < size; ++i)
                {
                    _board.setCellValue(randWord.Substring(i, 1), middleRow, i);
                }

                usedWords.Add(randWord);
            }
        }

        public GameBoard getGameBoard()
        {
            return _board;
        }

        public GameKeys getKeyBoard()
        {
            return _keyBoard;
        }

        public int getSizeBoard()
        {
            return size;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeigth()
        {
            return heigth;
        }

    }
}

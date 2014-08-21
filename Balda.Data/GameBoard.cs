using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class GameBoard
    {
        public string[][] CellPool;
        private int _width;
        private int _heigth;

        public GameBoard(int width, int heigth)
        {
            SetSize(width, heigth);
        }

        public GameBoard()
        {
        }
        ////set size for board
        public void SetSize(int width, int heigth)
        {
            this._width = width;
            this._heigth = heigth;

            CellPool = new string[width][];
            for (int row = 0; row < heigth; row++)
            {
                CellPool[row] = new string[heigth];
                for (int column = 0; column < width; column++)
                {
                    CellPool[row][column] = string.Empty;
                }
            }
        }

        ////Retutn Width
        public int Width()
        {
            return this._width;
        }

        ////Return Heigth
        public int Heigth()
        {
            return this._heigth;
        }

        ////Clear gameBoard
        public void Clear()
        {
            CellPool = new string[_width][];
            CellPool[0] = new string[5];
            CellPool[1] = new string[5];
            CellPool[2] = new string[5];
            CellPool[3] = new string[5];
            CellPool[4] = new string[5];
        }

        ////Return value cell
        public string GetCellValue(int row, int column)
        {
            return CellPool[row][column];
        }

        ////Set value for cell
        public void SetCellValue(string value, int row, int column)
        {
            CellPool[row][column] = value;
        }
    }
}

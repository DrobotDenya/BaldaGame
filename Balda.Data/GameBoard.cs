﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Balda.Data
{
    public class GameBoard
    {
        int _width;
        int _heigth;

        public string [,] cellPool;
        public GameBoard(int width, int heigth)
        {
            setSize(width, heigth);
        }

        public GameBoard()
        {
 
        }
        //set size for board
        public void setSize(int width, int heigth)
        {
            this._width = width;
            this._heigth = heigth;
            
           cellPool = new string[width, heigth];
            for (int row = 0; row < heigth; row++)
            {
                for(int column = 0; column < width; column++)
                {
                    
                    cellPool[row ,column] = "";
                }               
            }
        }

        //Retutn width
        public int width()
        {
            return this._width;
        }

        //Return heigth
        public int heigth()
        {
            return this._heigth;
        }
        
        //Clear gameBoard
        public void clear()
        {
            cellPool = new string[_width, _heigth];
        }

        //Return value cell
        public string getCellValue(int row, int column)
        {
            return cellPool[row,column];
        }

        //Set value for cell
        public void setCellValue(string value, int row, int column)
        {
            cellPool[row ,column] = value;
        }



    }
}
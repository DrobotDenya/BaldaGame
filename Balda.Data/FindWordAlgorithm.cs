using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Balda.Data
{
    public class FindWordAlgorithm
    {

        GameBoard gameBoard = new GameBoard();
        List<string> userWord = new List<string>();
        Dictionary dictionary = new Dictionary();
        string[,] temp;
        int[,] pr;
        int ai = 0;
        int aj = 0;
        int p, t;
        int curr = 0;
        string word;
        bool tl;
        bool bl;



        public FindWordAlgorithm(GameBoard gameBoard, List<string> userWord)
        {
            this.gameBoard = gameBoard;
            this.userWord = userWord;
        }

        public FindWordAlgorithm()
        {
           
        }

        //Ищет все подходящие слова в словаре
        public List<string> findWords()
        {
            List<string> res = new List<string>();
            ///////////////////////////
            temp = new string[gameBoard.width(), gameBoard.heigth()];

            for (int row = 0; row < gameBoard.width(); row++)
            {
                for (int column = 0; column < gameBoard.heigth(); column++)
                {
                    temp[row, column] = "";
                }
            }
            //////////////////////////////
            pr = new int[gameBoard.width(), gameBoard.heigth()];

            for (int row = 0; row < gameBoard.width(); row++)
            {
                for (int column = 0; column < gameBoard.heigth(); column++)
                {
                    pr[row, column] = 0;
                }
            }
            //////////////////////////////////
            while (curr < dictionary.getCount())
            {
                word = dictionary.getDictionary()[curr];
                if (!userWord.Contains(word))
                {
                    for (int row = 0; row < gameBoard.width(); row++)
                    {
                        for (int column = 0; column < gameBoard.heigth(); column++)
                        {
                            pr[row, column] = 0;
                        }
                    }


                    tl = true;

                    for (int k = 0; k < word.Length; k++) //Цикл по слову из словаря
                    {
                        for (int i = 0; i < gameBoard.width(); i++) // Цикл по самому массиву.
                        {
                            for (int j = 0; j < gameBoard.heigth(); j++)
                            {
                                if (gameBoard.getCellValue(i, j) == "")
                                {

                                    for (ai = 0; ai < gameBoard.width(); ai++)
                                    {
                                        for (aj = 0; aj < gameBoard.heigth(); aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            temp[ai, aj] = gameBoard.getCellValue(ai, aj);
                                        }
                                    }

                                    gameBoard.setCellValue(word.Substring(k, 1), i, j);
                                    bl = true;
                                    p = k; // С какой буквы подставляли для поиска в конец слова
                                    t = k; // С какой буквы подставляли для поиска в начало слова
                                    ai = i;
                                    aj = j;

                                    for (int i1 = 0; i1 < gameBoard.width(); ++i1)
                                    {
                                        for (int j1 = 0; j1 < gameBoard.heigth(); ++j1)
                                        {
                                            pr[i1, j1] = 0;
                                        }
                                    }

                                    pr[i, j] = 1;
                                    for (p = p + 1; p < word.Length; p++)
                                    {
                                        if (checkLetter(p))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    ai = i;
                                    aj = j;
                                    if (bl)
                                    {
                                        for (t = t - 1; t >= 0; t--)
                                        {
                                            if (checkLetter(t))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (bl)
                                        {
                                            res.Add(word);
                                            for (ai = 0; ai < 5; ai++)
                                            {
                                                for (aj = 0; aj < 5; aj++)
                                                {
                                                    gameBoard.setCellValue(temp[ai, aj], ai, aj);  // восстановили массив, в текущем состоянии.
                                                }
                                            }
                                            tl = false;
                                            break;   // Слово подошло переходим к следующему слову*/
                                        }
                                    }

                                    for (ai = 0; ai < gameBoard.width(); ai++)
                                    {
                                        for (aj = 0; aj < gameBoard.heigth(); aj++)
                                        {
                                            gameBoard.setCellValue(temp[ai, aj], ai, aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }

                            if (!tl)
                            {
                                break;
                            }

                        }
                    }
                    curr++;

                }
                else
                {
                    curr++;
                }
            }

            return res;
        }

        public int maxLength(List<string> words)
        {
            int length = 0;
            
            foreach (string w in words)
            {
                if (length < w.Length)
                {
                    length = w.Length;
                    
                }
 
            }
            return length;
        }

        //Возворащает первое подходящие слово из словаря
        public string findWord()
        {
            ////////////////
            temp = new string[gameBoard.width(), gameBoard.heigth()];

            for (int row = 0; row < gameBoard.width(); row++)
            {
                for (int column = 0; column < gameBoard.heigth(); column++)
                {
                    temp[row, column] = "";
                }
            }
            ////////////////
            pr = new int[gameBoard.width(), gameBoard.heigth()];

            for (int row = 0; row < gameBoard.width(); row++)
            {
                for (int column = 0; column < gameBoard.heigth(); column++)
                {
                    pr[row, column] = 0;
                }
            }
            //////////////////////////

            while (curr < dictionary.getCount())//Цикл по словарю
            {
                word = dictionary.getDictionary()[curr];
                if (!userWord.Contains(word))
                {

                    for (int i = 0; i < gameBoard.width(); ++i)
                    {
                        for (int j = 0; j < gameBoard.heigth(); ++j)
                        {
                            pr[i, j] = 0;
                        }
                    }

                     tl = true;

                    for (int k = 0; k < word.Length; k++) //Цикл по слову из словаря
                    {
                        for (int i = 0; i < gameBoard.width(); i++) // Цикл по самому массиву.
                        {
                            for (int j = 0; j < gameBoard.heigth(); j++)
                            {
                                if (gameBoard.getCellValue(i, j) == "")
                                {

                                    for (ai = 0; ai < gameBoard.width(); ai++)
                                    {
                                        for (aj = 0; aj < gameBoard.heigth(); aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            temp[ai,aj] = gameBoard.getCellValue(ai, aj);
                                        }
                                    }

                                    gameBoard.setCellValue(word.Substring(k, 1), i, j);

                                    bl = true;
                                    p = k; // С какой буквы подставляли для поиска в конец слова
                                    t = k; // С какой буквы подставляли для поиска в начало слова
                                    ai = i;
                                    aj = j;

                                    for (int i1 = 0; i1 < gameBoard.width(); ++i1)
                                    {
                                        for (int j1 = 0; j1 < gameBoard.heigth(); ++j1)
                                        {
                                            pr[i1 ,j1] = 0;
                                        }
                                    }

                                    pr[i, j] = 1;
                                    for (p = p + 1; p < word.Length; p++)
                                    {
                                        if (checkLetter(p))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    ai = i;
                                    aj = j;
                                    if (bl)
                                    {
                                        for (t = t - 1; t >= 0; t--)
                                        {
                                            if (checkLetter(t))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (bl)
                                        {
                                            return word;
                                        }
                                    }

                                    for (ai = 0; ai < gameBoard.width(); ai++)
                                    {
                                        for (aj = 0; aj < gameBoard.heigth(); aj++)
                                        {
                                            gameBoard.setCellValue(temp[ai, aj], ai, aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }
                            if (!tl)
                            {
                                break;
                            }
                        }
                    }

                    curr++;
                }
                else
                {
                    curr++;
                }
            }

            return "";
        }

        public string findWordWithMaxLength()
        {
            ////////////////
            temp = new string[gameBoard.width(), gameBoard.heigth()];

            for (int row = 0; row < gameBoard.width(); row++)
            {
                for (int column = 0; column < gameBoard.heigth(); column++)
                {
                    temp[row, column] = "";
                }
            }
            ////////////////
            pr = new int[gameBoard.width(), gameBoard.heigth()];

            for (int row = 0; row < gameBoard.width(); row++)
            {
                for (int column = 0; column < gameBoard.heigth(); column++)
                {
                    pr[row, column] = 0;
                }
            }
            //////////////////////////
            
            List<string> dict = findWords();
            curr = 0;
            while (curr < dict.Count)//Цикл по словарю
            {
                word = dict[curr];
                if (!userWord.Contains(word) && word.Length == maxLength(dict))
                {

                    for (int i = 0; i < gameBoard.width(); ++i)
                    {
                        for (int j = 0; j < gameBoard.heigth(); ++j)
                        {
                            pr[i, j] = 0;
                        }
                    }

                    tl = true;

                    for (int k = 0; k < word.Length; k++) //Цикл по слову из словаря
                    {
                        for (int i = 0; i < gameBoard.width(); i++) // Цикл по самому массиву.
                        {
                            for (int j = 0; j < gameBoard.heigth(); j++)
                            {
                                if (gameBoard.getCellValue(i, j) == "")
                                {

                                    for (ai = 0; ai < gameBoard.width(); ai++)
                                    {
                                        for (aj = 0; aj < gameBoard.heigth(); aj++)
                                        {
                                            // сохранили массив, для возврата в предыдущее состояние.
                                            temp[ai, aj] = gameBoard.getCellValue(ai, aj);
                                        }
                                    }

                                    gameBoard.setCellValue(word.Substring(k, 1), i, j);

                                    bl = true;
                                    p = k; // С какой буквы подставляли для поиска в конец слова
                                    t = k; // С какой буквы подставляли для поиска в начало слова
                                    ai = i;
                                    aj = j;

                                    for (int i1 = 0; i1 < gameBoard.width(); ++i1)
                                    {
                                        for (int j1 = 0; j1 < gameBoard.heigth(); ++j1)
                                        {
                                            pr[i1, j1] = 0;
                                        }
                                    }

                                    pr[i, j] = 1;
                                    for (p = p + 1; p < word.Length; p++)
                                    {
                                        if (checkLetter(p))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    ai = i;
                                    aj = j;
                                    if (bl)
                                    {
                                        for (t = t - 1; t >= 0; t--)
                                        {
                                            if (checkLetter(t))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (bl)
                                        {
                                            
                                            return word;
                                        }
                                    }

                                    for (ai = 0; ai < gameBoard.width(); ai++)
                                    {
                                        for (aj = 0; aj < gameBoard.heigth(); aj++)
                                        {
                                            gameBoard.setCellValue(temp[ai, aj], ai, aj);  // восстановили массив, в текущем состоянии.
                                        }
                                    }
                                }
                            }
                            if (!tl)
                            {
                                break;
                            }
                        }
                    }

                    curr++;
                }
                else
                {
                    curr++;
                }
            }
 return "";
        }

        //Определяет, есть ли буква на поле сверху или снизу или влева или справа текущей ячейки
        public bool checkLetter(int index)
        {
            string letter = word.Substring(index, 1);

            if (ai + 1 != gameBoard.width())
            {
                if ((gameBoard.getCellValue(ai + 1, aj) == letter)
                        && (pr[ai + 1,aj] != 1))
                {
                    pr[ai + 1,aj] = 1;
                    ai++;
                    return true;
                }
            }
            if (ai - 1 != -1)
            {
                if ((gameBoard.getCellValue(ai - 1, aj) == letter)
                        && (pr[ai - 1,aj] != 1))
                {
                    pr[ai - 1,aj] = 1;
                    ai--;
                    return true;
                }
            }
            if (aj + 1 != gameBoard.heigth())
            {
                if ((gameBoard.getCellValue(ai, aj + 1) == letter)
                        && (pr[ai,aj + 1] != 1))
                {
                    pr[ai,aj + 1] = 1;
                    aj++;
                    return true;
                }
            }
            if (aj - 1 != -1)
            {
                if ((gameBoard.getCellValue(ai, aj - 1) ==  letter)
                        && (pr[ai,aj - 1] != 1))
                {
                    pr[ai,aj - 1] = 1;
                    aj--;
                    return true;
                }
            }

            bl = false; // Если не совпало ни одно условие выше то это слово не подходит.
            return false;
 
        }

    }
}

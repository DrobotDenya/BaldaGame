using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class Bot : User
    {
        int levelOfComplexity = 0;
        FindWordAlgorithm alho = new FindWordAlgorithm();


        public Bot(string name, int level, FindWordAlgorithm alho)
        {
            nickname = name;
            this.alho = alho;
            if (level >= 0 && level <= 2)
            {
                levelOfComplexity = level;
            }
            else
            {
                levelOfComplexity = 0;
            }
        }

        public int solution()
        {
            //alho.findWords();
            string findedWord;
            if (levelOfComplexity == 1)
            {
                findedWord = alho.findWord();
                if (findedWord.Equals("") == false)
                {
                    addWord(findedWord);
                    return 1;
 
                }
            }

            if (levelOfComplexity == 2)
            {
                findedWord = alho.findWordWithMaxLength();
                if (findedWord.Equals("") == false)
                {
                    addWord(findedWord);
                    return 1;

                }
            }
            //Если слово не найдено и бот легкий, то он сдается
            if (levelOfComplexity == 0)
            {
                surrender();
                return 0;
            }
            //Иначе пропускает ход
            return 0;           
        }
            
    }
}

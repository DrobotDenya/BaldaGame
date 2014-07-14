using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Balda.Data
{
    public class Dictionary
    {
        List<string> words = new List<string>();

        public Dictionary()
        {
            string word;
            string fileName = @"C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\words.txt";
            StreamReader file = new StreamReader(fileName);
            while((word = file.ReadLine()) != null)
            {
                if (!words.Contains(word))
                {
                    words.Add(word);
 
                }
            }
            file.Close();

        }

        public int getCount()
        {
            return words.Count;
        }

        public List<string> getDictionary()
        {
            return words;
        }

        public string getRandomWord(int length)
        {
            List<string> listwords = getWords(length);
            if (listwords.Count > 0)
            {
                Random rand = new Random();
                int randomNumb = rand.Next(0 ,listwords.Count);
                return listwords[randomNumb];                 
            }
            return null;
 
        }

        public List<string> getWords(int length)
        {
            List<string> listWords = new List<string>();

            foreach (string word in words)
            {
                if (word.Length == length)
                {
                    listWords.Add(word);
                }
            }
            return listWords;
        }

        public bool isExist(string word)
        {
            return words.Contains(word);
        }

        public bool isCanExist(string letters)
        {
            foreach (string word in words)
            {
                if (word.IndexOf(letters) != -1)//????????
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> possibleWords(string letters)
        {
            List<string> words = new List<string>();
            foreach (string word in words)
            {
                if (word.Substring(1).IndexOf(word) != -1)
                {
                    words.Add(word);
                }

                if (word.Substring(0, word.Length - 2).IndexOf(word) != -1)
                {
                    words.Add(word);
 
                }
            }
            return words;
        }


        
    }
}

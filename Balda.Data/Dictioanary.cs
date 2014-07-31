using System;
using System.Collections.Generic;
using System.IO;

namespace Balda.Data
{
    public class Dictionary
    {
        private List<string> _words = new List<string>();

        public Dictionary()
        {
            string word;
            string fileName = @"..\..\..\Resources\words.txt";
            StreamReader file = new StreamReader(fileName);
            while ((word = file.ReadLine()) != null)
            {               
                    _words.Add(word);
            }

            file.Close();
        }

        public int GetCount()
        {
            return _words.Count;
        }

        public List<string> GetDictionary()
        {
            return _words;
        }

        public string GetRandomWord(int length)
        {
            List<string> listwords = GetWords(length);
            if (listwords.Count > 0)
            {
                Random rand = new Random();
                int randomNumb = rand.Next(0, listwords.Count);
                return listwords[randomNumb];
            }

            return null;
        }

        public List<string> GetWords(int length)
        {
            List<string> listWords = new List<string>();

            foreach (string word in _words)
            {
                if (word.Length == length)
                {
                    listWords.Add(word);
                }
            }

            return listWords;
        }

        public bool IsExist(string word)
        {
            return _words.Contains(word);
        }

        public bool IsCanExist(string letters)
        {
            foreach (string word in _words)
            {
                if (word.IndexOf(letters) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public List<string> PossibleWords(string letters)
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

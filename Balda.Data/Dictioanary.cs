using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Balda.Data
{
    public class Dictionary
    {
        private Collection<string> _words = new Collection<string>();

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

        public int Count()
        {
            return _words.Count;
        }

        public Collection<string> GetDictionary()
        {
            return _words;
        }

        public string GetRandomWord(int length)
        {
            Collection<string> Collectionwords = GetWords(length);
            if (Collectionwords.Count > 0)
            {
                Random rand = new Random();
                int randomNumb = rand.Next(0, Collectionwords.Count);
                return Collectionwords[randomNumb];
            }

            return null;
        }

        public Collection<string> GetWords(int length)
        {
            Collection<string> CollectionWords = new Collection<string>();

            foreach (string word in _words)
            {
                if (word.Length == length)
                {
                    CollectionWords.Add(word);
                }
            }

            return CollectionWords;
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

        public Collection<string> PossibleWords(string letters)
        {
            Collection<string> words = new Collection<string>();
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

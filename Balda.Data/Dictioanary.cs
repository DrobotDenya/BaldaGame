using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Balda.Data
{
    public class Dictionary
    {
        /// <summary>
        /// Список всех слов в словаре
        /// </summary>
        private Collection<string> _words = new Collection<string>();
        /// <summary>
        /// Конструктор
        /// </summary>
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
        /// <returns>
        /// Кл-во слов в словаре
        /// </returns>
        public int Count()
        {
            return _words.Count;
        }
        /// <returns>
        /// Список всех слов в словаре
        /// </returns>
        public Collection<string> GetDictionary()
        {
            return _words;
        }
        /// <summary>
        /// Возвращает рандомное слово из словаря определенной длины
        /// </summary>
        ///<param name="length">
        /// Длина слова
        /// </param>
        /// <returns>
        /// Слово
        /// </returns>
        public string GetRandomWord(int length)
        {
            Collection<string> collectionwords = GetWords(length);
            if (collectionwords.Count > 0)
            {
                Random rand = new Random();
                int randomNumb = rand.Next(0, collectionwords.Count);
                return collectionwords[randomNumb];
            }

            return null;
        }
        /// <summary>
        /// Возвращает все слова из словаря определенной длины
        /// </summary>
        ///<param name="length">
        /// Длина слова
        /// </param>
        /// <returns>
        /// Список слов
        /// </returns>
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
        /// <summary>
        /// Проверяет есть ли слова в словаре
        /// </summary>
        ///<param name="word">
        /// Слово
        /// </param>
        public bool IsExist(string word)
        {
            return _words.Contains(word);
        }
    }
}

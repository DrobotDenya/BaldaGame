using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
[assembly: CLSCompliant(true)]
namespace Balda.Data
{
    public class User
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        private Collection<string> _words = new Collection<string>();

        private bool _isSurrender = false;
        private static User _user;
        public static User SharedUser
        {
            get
            {
                if (_user == null)
                {
                    _user = new User();

                }
                return _user;
            }

            set { _user = value; }
        }

        public User(string nickname, string name, string sername)
        {
            this.Nickname = nickname;
            this.FirstName = name;
            this.SecondName = sername;
        }

        public User(string nickname)
        {
            Nickname = nickname;
        }

        public User()
        {

        }

        public string GetNickname()
        {
            return this.Nickname;
        }

        public void AddWord(string word)
        {
            _words.Add(word);
        }

        public Collection<string> GetWordsList()
        {
            return _words;
        }

        public int GetPoints()
        {
            int points = 0;
            foreach (string word in _words)
            {
                points += word.Length;
            }
            return points;
        }

        public void Surrender()
        {
            _isSurrender = true;
        }

        public bool IsSurrender()
        {
            return _isSurrender;
        }
    }
}

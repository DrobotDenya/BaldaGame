using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class GameKeys
    {
        private ArrayList _keys = new ArrayList();

        private string _path = @"C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Keyboard.txt";

        public GameKeys()
        {
            ReadKeyBoard();
        }

        public ArrayList GetKeys()
        {
            return _keys;
        }

        private void ReadKeyBoard()
        {
            string line;
            StreamReader file = new StreamReader(_path);
            while ((line = file.ReadLine()) != null)
            {
                _keys.Add(line);
            }
            file.Close();
        }
    }
}

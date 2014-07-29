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
        private ArrayList keys = new ArrayList();

        private string path = @"C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Keyboard.txt";

        public GameKeys()
        {
            readKeyBoard();
        }

        public ArrayList getKeys()
        {
            return keys;
        }

        private void readKeyBoard()
        {
            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                keys.Add(line);
            }
            file.Close();
        }
    }
}

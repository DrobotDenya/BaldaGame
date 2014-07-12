using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Balda.Data
{
    public class GameKeys
    {
        ArrayList keys = new ArrayList();
        string path = @"C:\Users\drobo_000\Documents\HG\Balda-clone\ia23-09-Balda\Resources\Keyboard.txt";
        
        public GameKeys()
        {
            readKeyBoard();
        }

        private void readKeyBoard()
        {
            string line;
            StreamReader file = new StreamReader(path); 
            while((line = file.ReadLine()) != null)
            {
                keys.Add(line);
            }
            file.Close();
        }
        



    }
}

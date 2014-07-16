using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public class Settings
    {
        private static Settings settings;

        private Settings() 
        {
          
        }

        public static Settings Setting
        {
            get
            {
                if (settings == null)
                {
                    settings = new Settings();
                }
                return settings;
            }
        }

        private int botComplexity = 1;
        private bool isBot = true;
        private string playerName;

        public void setBotComplexity(int complexity)
        {
            botComplexity = complexity;
        }

        public void setIsBot(bool b)
        {
            isBot = b;
        }

        public void setPlayerName(string name)
        {
            playerName = name;
 
        }

        public int getBotComplexity()
        {
            return botComplexity;
        }

        public bool getIsBot()
        {
            return isBot;
        }

        public string getNamePlayer()
        {
            return playerName;
        }
    }
}

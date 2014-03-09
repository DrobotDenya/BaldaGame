using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda.Data
{
    public sealed class  Player
    {
        private readonly string _name;
        private readonly string _sername;
        private readonly string _password;

        public string getName()
        {
            return _name;
        }

        private static Player player;

        private Player() { }

        public static Player sharePlayer
        {
            get
            {
                if (player == null)
                {
                    player = new Player();
                }

                return player;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BaldaWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class Service1 : IService1
    {
        public ICallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<ICallback>();

            }
        }


        private List<Game> listGame = new List<Game>();


        public void CreateGame(string id, string word)
        {
            Game game = new Game();
            game.Id = id;
            game.Player1 = id;
            game.players.Add(id, Callback);
            game.StartWord = word;
            listGame.Add(game);
        }

        public string ConnectToGame(string id, string nickname)
        {
            foreach (Game game in listGame)
            {
                if (game.Id == id)
                {
                    game.Player2 = nickname;
                    game.players.Add(nickname, Callback);
                    game.players[game.Player1].ConnectionNewPlayer(nickname);
                    //game.players[game.Player2].ConnectionNewPlayer(game.Player1);
                    return game.StartWord;
                }
            }
            return null;
        }

        public List<string> FindAllGame()
        {
            List<string> listId = new List<string>();
            foreach (Game game in listGame)
            {
                listId.Add(game.Id);
            }
            return listId;
        }

        public void LeaveGame()
        {
        }

        public void SetLetter(string[][] list, string word, string nickname)
        {
            foreach (Game game in listGame)
            {
                if (game.Player1 == nickname)
                {
                    game.players[game.Player2].Turn(list, word);
                }

                if (game.Player2 == nickname)
                {
                    game.players[game.Player1].Turn(list, word);
                }
            }
        }
    }
}

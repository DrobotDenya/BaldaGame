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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required,
                 CallbackContract = typeof(ICallback))]
    public interface IService1
    {
        [OperationContract]
        void CreateGame(string id, string word);

        [OperationContract]
        string ConnectToGame(string id, string nickname);

        [OperationContract]
        List<string> FindAllGame();

        [OperationContract]
        void LeaveGame();

        [OperationContract]
        void SetLetter(string[][] list, string word, string nickname);
    }

    public interface ICallback
    {
        [OperationContract]
        void Turn(string[][] list, string word);

        [OperationContract]
        void ConnectionNewPlayer(string nickname);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Game
    {
        public string Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string StartWord { get; set; }
        public Dictionary<string, ICallback> players = new Dictionary<string, ICallback>();

    }


}

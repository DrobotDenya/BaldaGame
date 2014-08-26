using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Balda.ServiceReference3;
using Balda.Data;
using Balda.View;
using Balda.ViewModel;

namespace Balda
{
    /// <summary>
    /// Interaction logic for FindGameWindow.xaml
    /// </summary>
    public partial class FindGameWindow : Window, ServiceReference3.IService1Callback
    {
        private Service1Client client;
        public FindGameWindow()
        {
            InitializeComponent();
            string[][] arr = new string[1][];

            InstanceContext instanceContext = new InstanceContext(this);
            client = new Service1Client(instanceContext);
            arr[0] = client.FindAllGame();
            foreach (var s in arr)
            {
                foreach (var s1 in s)
                {
                    Gameslist.Items.Add(s1);
                }

            }
        }

        private GameViewModel viewModel;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = client.ConnectToGame(Gameslist.SelectedItem.ToString(), User.SharedUser.Nickname);
            GameWindow gameWindow = new GameWindow();
            viewModel = new GameViewModel(s, gameWindow,client);
            gameWindow.DataContext = viewModel;
            gameWindow.Show();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Turn(string[][] list, string word)
        {
            viewModel.Turn(list, word);
        }

        public void ConnectionNewPlayer(string nickname)
        {
            //MessageBox.Show("Player" + nickname + "Connection to game!!!!!");
            //_gameWindow.SetNamePlayers(User.SharedUser.Nickname, nickname);
            //SetEnableBoard(true);
            //SetEnableKeyboard(true);
        }
    }
}

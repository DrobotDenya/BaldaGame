using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Balda
{
    /// <summary>
    /// Логика взаимодействия для DCMenuWindow.xaml
    /// </summary>
    public partial class DCMenuWindow : Window
    {
        public DCMenuWindow()
        {
            InitializeComponent();
            
        }
        private void buttonLoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonNewGame_Click(object sender, RoutedEventArgs e)
        {
            DCSettingGameWindow settingGameWindow = new DCSettingGameWindow();
            settingGameWindow.Show();

        }
    }
}

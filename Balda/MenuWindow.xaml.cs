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
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            
        }
        private void buttonLoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonNewGame_Click(object sender, RoutedEventArgs e)
        {
            SettingGameWindow settingGameWindow = new SettingGameWindow();
            settingGameWindow.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

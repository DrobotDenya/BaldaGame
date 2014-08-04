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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Balda.Data;

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

        private void ButtonNewGameClick(object sender, RoutedEventArgs e)
        {

            GameWindow gameWindow = new GameWindow();
            GameViewModel viewViewModel = new GameViewModel(gameWindow);
            gameWindow.DataContext = viewViewModel;
            //viewViewModel.ShowMessage("123123");
            gameWindow.Show();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingGameWindow settingGameWindow = new SettingGameWindow();
            settingGameWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TranslateTransform trans = new TranslateTransform();
            //ButtonNewGame.RenderTransform = trans;
            //var animation = new DoubleAnimation();
            //animation.From = 0;
            //animation.To = 1000;
            //animation.Duration = TimeSpan.FromSeconds(10);
            //ButtonNewGame.BeginAnimation(TranslateTransform.YProperty, animation);
        }


    }
}

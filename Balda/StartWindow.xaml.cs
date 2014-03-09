using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Balda
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            textBoxLogin.Text = "Denya";
        }

        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "Denya")
            {
                MenuWindow menu = new MenuWindow();
                menu.Show();
                this.Hide();
            }
            else
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                myDoubleAnimation.From = 1.0;
                myDoubleAnimation.To = 0.0;
                myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(.4));
                myDoubleAnimation.AutoReverse = true;
                myDoubleAnimation.RepeatBehavior = new RepeatBehavior(3);
                textBoxLogin.BeginAnimation(TextBox.OpacityProperty, myDoubleAnimation);

            }

        }
    }
}

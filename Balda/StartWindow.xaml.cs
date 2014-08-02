using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Balda.Data;

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
            TextBoxLogin.Text = "qwe";
            TbxPassword.Text = "qwe";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User.SharedUser.Password = TbxPassword.Text;
            User.SharedUser.Nickname = TextBoxLogin.Text;

            if (User.SharedUser.UsingSelect())
            {
                MenuWindow menu = new MenuWindow();
                menu.Show();
                this.Close();
            }
            else
            {
                Brush brushes = TbxPassword.BorderBrush;
                TbxPassword.BorderBrush = Brushes.Red;
                DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                myDoubleAnimation.From = 1.0;
                myDoubleAnimation.To = 0.0;
                myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(.4));
                myDoubleAnimation.AutoReverse = true;
                myDoubleAnimation.RepeatBehavior = new RepeatBehavior(3);
                TbxPassword.BeginAnimation(OpacityProperty, myDoubleAnimation);
                ////TbxPassword.BorderBrush = brushes;
            }

        }

        private void TextBlockRegistrationMouseDown(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindows registrWindow = new RegistrationWindows(this);
            registrWindow.Show();
            this.Hide();
        }
    }
}

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
    /// Interaction logic for RegistrationWindows.xaml
    /// </summary>
    public partial class RegistrationWindows : Window
    {
        private StartWindow startWindow;
        public RegistrationWindows(StartWindow window)
        {
            InitializeComponent();
            startWindow = window;
        }

        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            string nickname = tbNickname.Text;
            string firstName = tbFirstName.Text;
            string secondName = tbSecondName.Text;
            string password = tbPassword.Text;
            string confPassword = tbConfPassword.Text;

           if (nickname == String.Empty)
            {
                AnimationTextBox(tbNickname);
            }
            if (firstName == String.Empty)
            {
                AnimationTextBox(tbFirstName);
            }
            if (secondName == String.Empty)
            {
                AnimationTextBox(tbSecondName);
            }
            if (password == string.Empty)
            {
                AnimationTextBox(tbPassword);
            }
            if (confPassword == string.Empty)
            {
                AnimationTextBox(tbConfPassword);
            }
            if (nickname != "" && firstName != ""
                && secondName != "" && password != "" 
                && confPassword == password)
            {
                User user = new User(nickname, firstName, secondName, password);
                user.UsingInsert();

                startWindow.Show();
                this.Close();
            }
        }

        private void AnimationTextBox(TextBox textBox)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.From = Colors.White;
            colorAnimation.To = Colors.Red;
            colorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            colorAnimation.AutoReverse = true;
            colorAnimation.RepeatBehavior = new RepeatBehavior(3);
            textBox.Background = new SolidColorBrush(Colors.Red);
            textBox.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            startWindow.Show();
            this.Close();
        }
    }
}

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
using Balda.Data;

namespace Balda
{
    /// <summary>
    /// Interaction logic for RegistrationWindows.xaml
    /// </summary>
    public partial class RegistrationWindows : Window
    {
        public RegistrationWindows()
        {
            InitializeComponent();
        }

        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            string nickname = tbNickname.Text;
            string firstName = tbFirstName.Text;
            string secondName = tbSecondName.Text;
            string password = tbPassword.Text;
            string confPassword = tbConfPassword.Text;

            if (nickname != "" && firstName != ""
                && secondName != "" && password != ""
                && confPassword == password)
            {
                User user = new User(nickname, firstName, secondName, password);
                user.UsingInsert();
            }
        }
    }
}

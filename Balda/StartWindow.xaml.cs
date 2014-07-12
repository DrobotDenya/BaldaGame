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
using System.Data.OleDb;
using System.Data;
using Balda.Data;

namespace Balda
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        DataUserManager userManager;

        public StartWindow()
        {
            InitializeComponent();
            userManager = new DataUserManager();
        }
        
        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
            if (userManager.findUser(textBoxLogin.Text) && userManager.findUserPassword(tbxPassword.Text))
            {
                MenuWindow menu = new MenuWindow();
                menu.Show();
                this.Hide();
            }
            else
            {
                Brush brushes = tbxPassword.BorderBrush;
                tbxPassword.BorderBrush = Brushes.Red;
                DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                myDoubleAnimation.From = 1.0;
                myDoubleAnimation.To = 0.0;
                myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(.4));
                myDoubleAnimation.AutoReverse = true;
                myDoubleAnimation.RepeatBehavior = new RepeatBehavior(3);
                tbxPassword.BeginAnimation(TextBox.OpacityProperty, myDoubleAnimation);
                //tbxPassword.BorderBrush = brushes;
            }

        }

        private void textBlockRegistration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindows registrWindow = new RegistrationWindows();
            registrWindow.Show();
            this.Hide();
        }

        //void connectionToDataBase()

        //{
        //    OleDbConnection connect = new OleDbConnection();
        //    string word = "Wordddddd";
        //    int value = 4;

        //    connect.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda\Database1\Dictionary.accdb";
        //    connect.Open();

        //    OleDbCommand cmd = new OleDbCommand("INSERT INTO [Dictionary] ([Word], [Value])" + "values(?,?)", connect);
        //    OleDbCommand cmdremove = new OleDbCommand("DELETE FROM [Dictionary] where id = 1", connect);
                       
        //    if (connect.State == ConnectionState.Open)
        //    {

                
        //        cmd.Parameters.Add("", OleDbType.VarChar).Value = word;
        //        cmd.Parameters.Add("", OleDbType.Integer).Value = value;
        //        //adapter.SelectCommand = cmd;
        //        try
        //        {
        //            cmdremove.ExecuteNonQuery();
        //            cmd.ExecuteNonQuery();
        //            MessageBox.Show(@"Data Added To DataBase");
        //            connect.Close();
        //        }
        //        catch (Exception expe)
        //        {
        //            MessageBox.Show(expe.Source);
        //            connect.Close();
        //        }
        //    }
        //    else 
        //    {
        //        MessageBox.Show("Connection Failed");
        //    }
        
            


        //}

    }
}

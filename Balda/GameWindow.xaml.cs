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
using System.Data.OleDb;
using Balda.Data;

namespace Balda
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        //OleDbConnection cn = new OleDbConnection();
        //OleDbCommand cmd = new OleDbCommand();
        //OleDbDataReader dr;

        //string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\drobo_000\Documents\HG\Balda\Balda.Data\ListForPlayer1.accdb";

       

        public GameWindow()
        {
            InitializeComponent();
            //cn.ConnectionString = connectionString;
            //cmd.Connection = cn;
            //loaddata();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            lblPlayer1.Content = "Denya";
            lblPlayer2.Content = "Bot";
           
 

        }

        private void dosomething(String q)
        {
            //try
            //{
            //    cn.Open();
            //    cmd.CommandText = q;
            //    cmd.ExecuteNonQuery();
            //    cn.Close();
            //    loaddata();
            //}
            //catch (Exception e)
            //{
            //    cn.Close();
            //    MessageBox.Show(e.Message.ToString());
            //}
        }

        private void loaddata()
        {
            //listBoxP1.Items.Clear();
            //listBoxValue1.Items.Clear();

            //try
            //{
            //    string q = "select * from TableForPlayer1";
            //    cmd.CommandText = q;
            //    cn.Open();
            //    dr = cmd.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            listBoxP1.Items.Add(dr[0].ToString());
            //            listBoxValue1.Items.Add(dr[1].ToString());                   
            //        }
            //    }
            //    dr.Close();
            //    cn.Close();
            //}
            //catch (Exception e)
            //{
            //    cn.Close();
            //    MessageBox.Show(e.Message.ToString());
            //}
        }
        private void btnAddWord_Click(object sender, RoutedEventArgs e)
        {
          
            //if (textBox1.Text != "")
            //{
            //    string q = "insert into TableForPlayer1 (Word) values ('" + textBox1.Text.ToString() + "')";
            //    dosomething(q);
            //    textBox1.Text = null;
            //}
        }

        private void btnAddButton2_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void listBoxValue1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        void updataTable()
        {
            //if (textBox1.Text != "" & listBoxP1.SelectedIndex != -1)
            //{
            //    string q = "update TableForPlayer1 set Word='" + textBox1.Text.ToString() + "' where ID=" + listBoxP1.SelectedItem.ToString();
            //    dosomething(q);
            //    textBox1.Text = "";
            //}
        }
        private void listBoxValue2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnAddWord_Copy_Click(object sender, RoutedEventArgs e)
        {
            //if (listBoxP1.SelectedIndex != -1)
            //{
            //    string q = "delete from TableForPlayer1 where ID=" + listBoxP1.SelectedItem.ToString();
            //    dosomething(q);
            //   // updataTable();
            //}
        }

        private void listBoxP1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ListBox l = sender as ListBox;
            //if (l.SelectedIndex != -1)
            //{
            //    listBoxP1.SelectedIndex = l.SelectedIndex;
            //    listBoxValue1.SelectedIndex = l.SelectedIndex;
            //    textBox1.Text = listBoxP1.SelectedItem.ToString();
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Word word = new Word(textBox.Text, textBox.Text.Length, 1);
            word.insert();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Word word = new Word(textBox.Text, textBox.Text.Length, 2);
            word.insert();
        }
        //private void loaddata()
        //{
        //    listBox1.Items.Clear();
        //    listBox2.Items.Clear();
        //    try
        //    {
        //        string q = "select * from table1";
        //        cmd.CommandText = q;
        //        cn.Open();
        //        dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                listBox1.Items.Add(dr[0].ToString());
        //                listBox2.Items.Add(dr[1].ToString());
        //            }
        //        }
        //        dr.Close();
        //        cn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        cn.Close();
        //        MessageBox.Show(e.Message.ToString());
        //    }
        //}

        //private void ListBox2_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    ListBox l = sender as ListBox;
        //    if (l.SelectedIndex != -1)
        //    {
        //        listBox1.SelectedIndex = l.SelectedIndex;
        //        listBox2.SelectedIndex = l.SelectedIndex;
        //        textBox2.Text = listBox2.SelectedItem.ToString();
        //    }
        //}

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    if (textBox1.Text != "")
        //    {
        //        string q = "insert into table1 (Name) values ('" + textBox1.Text.ToString() + "')";
        //        dosomething(q);
        //        textBox1.Text = null;
        //    }
        //}


        //private void dosomething(String q)
        //{
        //    try
        //    {
        //        cn.Open();
        //        cmd.CommandText = q;
        //        cmd.ExecuteNonQuery();
        //        cn.Close();
        //        loaddata();
        //    }
        //    catch (Exception e)
        //    {
        //        cn.Close();
        //        MessageBox.Show(e.Message.ToString());
        //    }
        //}


        //private void button2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (listBox1.SelectedIndex != -1)
        //    {
        //        string q = "delete from table1 where id=" + listBox1.SelectedItem.ToString();
        //        dosomething(q);
        //    }
        //}

        //private void button3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (textBox2.Text != "" & listBox1.SelectedIndex != -1)
        //    {
        //        string q = "update table1 set Name='" + textBox2.Text.ToString() + "' where id=" + listBox1.SelectedItem.ToString();
        //        dosomething(q);
        //        textBox2.Text = "";
        //    }
        //}
        
    }
}

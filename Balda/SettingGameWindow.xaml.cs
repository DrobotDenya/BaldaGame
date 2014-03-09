﻿using System;
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
    /// Логика взаимодействия для SettingGameWindow.xaml
    /// </summary>
    public partial class SettingGameWindow : Window
    {
        public SettingGameWindow()
        {
            InitializeComponent();
            listboxDifficulty.Items.Add("Easy");
            listboxDifficulty.Items.Add("Normal");
            listboxDifficulty.Items.Add("Hard");
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void listboxDifficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void checkBoxPalyers_Checked(object sender, RoutedEventArgs e)
        {
           
                textBoxPlayerTwoName.IsEnabled = true;
            listboxDifficulty.IsEnabled = false;
        }

        private void checkBoxPalyers_Unchecked(object sender, RoutedEventArgs e)
        {
            textBoxPlayerTwoName.IsEnabled = false;
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
        }
    }
}
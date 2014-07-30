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
using Balda.Data;

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
            ListboxDifficulty.Items.Add("Easy");
            ListboxDifficulty.Items.Add("Normal");
            ListboxDifficulty.Items.Add("Hard");
            ListboxDifficulty.SelectedIndex = Settings.setting.getBotComplexity();
            CheckBoxPalyers.IsChecked = !Settings.setting.getIsBot();
            if (CheckBoxPalyers.IsChecked.Value)
            {
                TextBoxPlayerTwoName.Text = Settings.setting.getNamePlayer();
           }
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void listboxDifficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void checkBoxPalyers_Checked(object sender, RoutedEventArgs e)
        {
           
            TextBoxPlayerTwoName.IsEnabled = true;
            ListboxDifficulty.IsEnabled = false;
        }

        private void checkBoxPalyers_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPlayerTwoName.IsEnabled = false;
            TextBoxPlayerTwoName.Text = "Player2";
            ListboxDifficulty.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.setting.setBotComplexity(ListboxDifficulty.SelectedIndex);
            Settings.setting.setIsBot(!CheckBoxPalyers.IsChecked.Value);
            Settings.setting.setPlayerName(TextBoxPlayerTwoName.Text);
            this.Close();

            
        }

    }
}

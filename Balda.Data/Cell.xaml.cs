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
using System.Windows.Navigation;

namespace Balda.Data
{
    public partial class Cell : UserControl
    {
       //// public event MouseButtonEventHandler MouseDownn;

        public bool isSelected = false;

        public Cell()
        {
            InitializeComponent();
        }

        public void setText(string s)
        {
            mainLabel.Content = s;
        }

        public string getText()
        {
            return (string)mainLabel.Content;
        }

        public void cell_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}

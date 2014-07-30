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
using System.Windows.Navigation;

namespace Balda.Data
{
    public partial class Cell : UserControl
    {
       //// public event MouseButtonEventHandler MouseDownn;

        public bool IsSelected = false;

        public Cell()
        {
            InitializeComponent();
        }

        public void SetText(string s)
        {
            mainLabel.Content = s;
        }

        public string GetText()
        {
            return (string)mainLabel.Content;
        }

        public void CellMouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}

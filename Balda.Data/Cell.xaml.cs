using System.Windows.Controls;
using System.Windows.Input;

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

        public string Text()
        {
            return (string)mainLabel.Content;
        }

        public void CellMouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}

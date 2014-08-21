using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Balda
{
    /// <summary>
    /// Interaction logic for Star.xaml
    /// </summary>
    public partial class Star : UserControl
    {
        public Star()
        {
            InitializeComponent();
        }

        public void UserControlMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(Message);
        }
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Massege", typeof(string), typeof(Star));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
    }
}

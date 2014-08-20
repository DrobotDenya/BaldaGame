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
        public double Height
        {
            get { return Height; }
            set { Height = value; }
        }
        public double Width
        {
            get { return Width; }
            set { Width = value; }
        }

        public void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
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

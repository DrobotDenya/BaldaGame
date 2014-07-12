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
using System.Drawing;
using Balda.Data;


namespace Balda
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        
        public GameWindow()
        {
            InitializeComponent();           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            drawNet();
        }
      
        private void drawNet()
        {
            Line line;

            int heigth = (int)board.Height / 5;
            int width = (int)board.Width / 5;
            
            for (int i = 0; i <= 5; i++)
            {
                //Draw horizontal line
                line = new Line();
                line.Stroke = Brushes.Black;
                line.Width = board.Width;
                line.X1 = 0;
                line.Y1 = heigth * i;
                line.X2 = board.Width;
                line.Y2 = heigth * i;
                board.Children.Add(line);

                //Draw vertical line
                line = new Line();
                line.Stroke = Brushes.Black;
                line.Width = board.Width;
                line.X1 = width * i;
                line.Y1 = 0;
                line.X2 = width * i;
                line.Y2 = board.Height;
                board.Children.Add(line);
            }
           
            }
    }
}

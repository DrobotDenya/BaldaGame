using System.Collections.ObjectModel;
using System.Windows;
using Balda.Data;

namespace Balda.View
{
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        public void DrawBoardCell(Cell[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    Board.Children.Add(array[i][j]);
                }
            }
        }

        public void DrawKeys(Collection<Cell> list)
        {
            foreach (var cell in list)
            {
                Keyboard.Children.Add(cell);
            }
        }

        public void SetNamePlayers(string name1, string name2)
        {
            TitleP1.Text = name1;
            TitleP2.Text = name2;
        }

        public void UpdateListP1(Collection<string> listP1)
        {
            ListBoxP1.Items.Clear();
            foreach (string word in listP1)
            {
                ListBoxP1.Items.Add(word);
            }
        }

        public void UpdateListP2(Collection<string> listP2)
        {
            ListBoxP2.Items.Clear();
            foreach (string word in listP2)
            {
                ListBoxP2.Items.Add(word);
            }
        }

        public void UpdateValueP1(int value)
        {
            Value1.Content = value;
        }

        public void UpdateValueP2(int value)
        {
            Value2.Content = value;
        }
    }
}

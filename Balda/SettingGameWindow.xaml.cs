using System.Windows;
using System.Windows.Controls;
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
            ListboxDifficulty.SelectedIndex = Settings.Setting.GetBotComplexity();
            CheckBoxPalyers.IsChecked = !Settings.Setting.GetIsBot();
            if (CheckBoxPalyers.IsChecked.Value)
            {
                TextBoxPlayerTwoName.Text = Settings.Setting.GetNamePlayer();
            }

        }

        private void ListboxDifficultySelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBoxPalyersChecked(object sender, RoutedEventArgs e)
        {
            TextBoxPlayerTwoName.IsEnabled = true;
            ListboxDifficulty.IsEnabled = false;
        }

        private void CheckBoxPalyersUnchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPlayerTwoName.IsEnabled = false;
            TextBoxPlayerTwoName.Text = "Player2";
            ListboxDifficulty.IsEnabled = true;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Settings.Setting.SetBotComplexity(ListboxDifficulty.SelectedIndex);
            Settings.Setting.SetIsBot(!CheckBoxPalyers.IsChecked.Value);
            Settings.Setting.SetPlayerName(TextBoxPlayerTwoName.Text);
            this.Close();


        }

    }
}

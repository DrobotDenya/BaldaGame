using System;
using System.Windows.Input;
using Balda.View;

namespace Balda.ViewModel
{
    public class MenuViewModel
    {
        private MenuWindow _menuWindow;
        public MenuViewModel(MenuWindow menu)
        {
            _menuWindow = menu;
            _menuWindow.DataContext = this;
            StartBtnCommand = new Command(new Action<object>(StartGameClick));
            SettingsBtnCommand = new Command(new Action<object>(SettingClick));
            HighScoreBtnCommand = new Command(new Action<object>(HighScoreClick));
            CreateLocalGameCommand = new Command(new Action<object>(CreateLocalGameClick));
            FindGameCommand = new Command(new Action<object>(FindGameClick));
        }

        #region ButtonCommand
        /// <summary>
        /// Открытие окна игры
        /// </summary>
        private void StartGameClick(object obj)
        {
            GameWindow gameWindow = new GameWindow();
            GameViewModel viewModel = new GameViewModel(gameWindow, false);
            gameWindow.DataContext = viewModel;
            gameWindow.Show();
        }
        /// <summary>
        /// открытие окна настроек
        /// </summary>
        private void SettingClick(object obj)
        {
            SettingGameWindow settingWindow = new SettingGameWindow();
            SettingViewModel viewModel = new SettingViewModel(settingWindow);
            settingWindow.DataContext = viewModel;
            settingWindow.Show();
        }

        private void CreateLocalGameClick(object obj)
        {
            GameWindow gameWindow = new GameWindow();
            GameViewModel viewModel = new GameViewModel(gameWindow, true);
            gameWindow.DataContext = viewModel;
            gameWindow.Show();
        }

        private void FindGameClick(object obj)
        {
            FindGameWindow gameWindow = new FindGameWindow();
            gameWindow.Show();
        }

        private void HighScoreClick(object obj)
        {

        }

        public ICommand StartBtnCommand { get; set; }

        public ICommand SettingsBtnCommand { get; set; }

        public ICommand HighScoreBtnCommand { get; set; }

        public ICommand CreateLocalGameCommand { get; set; }

        public ICommand FindGameCommand { get; set; }
        #endregion
    }
}

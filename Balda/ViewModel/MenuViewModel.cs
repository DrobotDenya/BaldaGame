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
        }

        #region ButtonCommand
        /// <summary>
        /// Открытие окна игры
        /// </summary>
        private void StartGameClick(object obj)
        {
            GameWindow gameWindow = new GameWindow();
            GameViewModel viewModel = new GameViewModel(gameWindow);
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

        private void HighScoreClick(object obj)
        {

        }

        public ICommand StartBtnCommand { get; set; }

        public ICommand SettingsBtnCommand { get; set; }

        public ICommand HighScoreBtnCommand { get; set; }
        #endregion
    }
}

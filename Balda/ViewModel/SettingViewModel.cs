﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Balda.View;
using Balda.Data;

namespace Balda.ViewModel
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private readonly SettingGameWindow _settingWindow;
        private readonly string[] _difficulty = { "Easy", "Normal", "Hard" };
        public SettingViewModel(SettingGameWindow window)
        {
            BackbtnCommand = new Command(new Action<object>(BackClick));
            CheckBoxCommand = new Command(new Action<object>(CheckBoxChecked));
            _settingWindow = window;
            SetListBox();
            _isEnabled = false;
        }
        /// <summary>
        /// Заполнение лист бокса
        /// </summary>
        private void SetListBox()
        {
            foreach (var s in _difficulty)
            {
                _settingWindow.ListboxDifficulty.Items.Add(s);
            }
        }
        /// <summary>
        /// Значение CheckBox-а
        /// </summary>
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        /// <summary>
        /// On/Off 
        /// </summary>
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        #region Command
        /// <summary>
        /// Сохранение настроек и откритие окна меню
        /// </summary>
        private void BackClick(object obj)
        {
            Settings.Setting.SetBotComplexity(_settingWindow.ListboxDifficulty.SelectedIndex);
            Settings.Setting.SetIsBot(!IsChecked);
            Settings.Setting.SetPlayerName(_settingWindow.TextBoxPlayerTwoName.Text);
            _settingWindow.Close();
        }

        private void CheckBoxChecked(object obj)
        {
            IsEnabled = IsChecked;
        }

        public ICommand BackbtnCommand { get; set; }

        public ICommand CheckBoxCommand { get; set; }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

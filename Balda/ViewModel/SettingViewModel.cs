using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private void SetListBox()
        {
            foreach (var s in _difficulty)
            {
                _settingWindow.ListboxDifficulty.Items.Add(s);
            }
        }

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

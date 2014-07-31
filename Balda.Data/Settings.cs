namespace Balda.Data
{
    public class Settings
    {
        private static Settings _settings;
        private int _botComplexity = 1;
        private bool _isBot = true;
        private string _playerName;

        private Settings()
        {
        }

        public static Settings Setting
        {
            get
            {
                if(_settings == null)
                {
                    _settings = new Settings();
                }
                return _settings;
            }
        }

        public void SetBotComplexity(int complexity)
        {
            _botComplexity = complexity;
        }

        public void SetIsBot(bool b)
        {
            _isBot = b;
        }

        public void SetPlayerName(string name)
        {
            _playerName = name;
        }

        public int GetBotComplexity()
        {
            return _botComplexity;
        }

        public bool GetIsBot()
        {
            return _isBot;
        }

        public string GetNamePlayer()
        {
            return _playerName;
        }
    }
}

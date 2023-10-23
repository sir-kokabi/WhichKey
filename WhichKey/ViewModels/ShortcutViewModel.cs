namespace WhichKey.ViewModels
{
    public class ShortcutViewModel:ViewModelBase
    {
        private string _appName;
        private string _shortcut;
        private string _description;

        public ShortcutViewModel(string appName, string shortcut, string description)
        {
            _appName = appName;
            _shortcut = shortcut;
            _description = description;
        }

        public string AppName
        {
            get => _appName;
            private set
            {
                if (value == _appName) return;
                _appName = value;
                OnPropertyChanged(nameof(AppName));
            }
        }

        public string Shortcut
        {
            get => _shortcut;
            set
            {
                if (Equals(value, _shortcut)) return;
                _shortcut = value;
                OnPropertyChanged(nameof(Shortcut));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }
}

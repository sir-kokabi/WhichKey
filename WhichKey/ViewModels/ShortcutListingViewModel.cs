using System.ComponentModel;
using System.IO;
using System.Windows.Data;

namespace WhichKey.ViewModels
{
    public class ShortcutListingViewModel : ViewModelBase
    {
        public ICollectionView ShortcutsCollectionView { get; }

        private string _searchText = string.Empty;
        private string _appName;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                ShortcutsCollectionView.Refresh();
            }
        }

        public string AppName
        {
            get => _appName;
            set
            {
                if (value == _appName) return;
                _appName = value;
                OnPropertyChanged(nameof(AppName));
            }
        }

        public ShortcutListingViewModel(string appName)
        {
            AppName = appName;

            List<ShortcutViewModel> shortcutViewModels = new();


            foreach (var shortcutViewModel in GetShortcutViewModels(AppName))
            {
                shortcutViewModels.Add(shortcutViewModel);
            }

            ShortcutsCollectionView = CollectionViewSource.GetDefaultView(shortcutViewModels.Where(vm => vm.AppName == appName));

            ShortcutsCollectionView.Filter = FilterShortcuts;
        }

        private bool FilterShortcuts(object obj)
        {
            if (obj is ShortcutViewModel shortcutViewModel)
            {
                return shortcutViewModel.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) ||
                    shortcutViewModel.Shortcut.Contains(SearchText.ToUpper());
            }

            return false;
        }

        private static IEnumerable<ShortcutViewModel> GetShortcutViewModels(string appName)
        {
            var dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shortcuts"); 
            var filePath = Path.Combine(dataFolderPath, appName + ".txt");

            if (!File.Exists(filePath))
            {
                return Enumerable.Empty<ShortcutViewModel>();
            }

            var content = File.ReadAllText(filePath);

            var entries = content.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None);

            var shortcutViewModels = new List<ShortcutViewModel>();

            foreach (var entry in entries)
            {
                var parts = entry.Split(Environment.NewLine);
                var shortcut = parts[0].Trim();
                var desc = parts[1].Trim();
                shortcutViewModels.Add(new ShortcutViewModel(appName, shortcut, desc));
            }

            return shortcutViewModels;

        }
    }
}

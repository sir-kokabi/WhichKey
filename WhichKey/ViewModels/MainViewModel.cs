namespace WhichKey.ViewModels
{
    public class MainViewModel
    {
        public ShortcutListingViewModel ShortcutListingViewModel { get; }

        public MainViewModel(string appName)
        {
            ShortcutListingViewModel = new ShortcutListingViewModel(appName);
        }

    }
}

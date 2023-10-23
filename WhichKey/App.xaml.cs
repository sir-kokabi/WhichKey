using NHotkey;
using NHotkey.Wpf;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WhichKey.ViewModels;


namespace WhichKey
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public Window _mainWindow = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
            _mainWindow.PreviewKeyDown += _mainWindow_PreviewKeyDown;
            _mainWindow.StateChanged+= MainWindow_OnStateChanged;
            HotkeyManager.Current.AddOrReplace("ShowWindow", Key.None, ModifierKeys.Control | ModifierKeys.Alt, ShowWindow);
            base.OnStartup(e);
        }

        private void MainWindow_OnStateChanged(object? sender, EventArgs e)
        {

            if (_mainWindow.WindowState == WindowState.Minimized)
            {
                _mainWindow.Hide();
            }
        }

        private void _mainWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _mainWindow.WindowState = WindowState.Minimized;
            }
        }

        private void ShowWindow(object? sender, HotkeyEventArgs e)
        {
            var appName = GetActiveProcessFileName();
            _mainWindow.DataContext = new MainViewModel(appName.ToLower());
            _mainWindow.Show();
            _mainWindow.Activate();
            _mainWindow.WindowState=WindowState.Normal;

            e.Handled = true;
            if (_mainWindow is MainWindow mainWindow)
            {
                if (mainWindow.ShortcutListingView.SearchTextBox != null)
                {
                    mainWindow.ShortcutListingView.SearchTextBox.Focus();
                    mainWindow.ShortcutListingView.SearchTextBox.SelectAll();
                }
            }
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var text = $"\n\n{DateTime.Now}\n{e.Exception.ToStringDemystified()}";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            File.AppendAllText(path, text);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        static string GetActiveProcessFileName()
        {
            var hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out var pid);
            var p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }
    }

}

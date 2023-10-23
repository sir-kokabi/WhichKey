using NHotkey;
using NHotkey.Wpf;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WhichKey.ViewModels;
using WhichKey.Views;


namespace WhichKey
{
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        

        private void ItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }

}
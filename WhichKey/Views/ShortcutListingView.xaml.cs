using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WhichKey.Views
{
    /// <summary>
    /// Interaction logic for ShortcutListingView.xaml
    /// </summary>
    public partial class ShortcutListingView : UserControl
    {
        public ShortcutListingView()
        {
            InitializeComponent();
        }

        private void SearchTextBox_OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SearchTextBox.Focus();
            Keyboard.Focus(SearchTextBox);
            e.Handled = true;
        }
    }
}

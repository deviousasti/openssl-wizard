using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace openssl_wizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var documentBytes = Encoding.UTF8.GetBytes(openssl_wizard.Properties.Resources.Help);
            using (var reader = new MemoryStream(documentBytes))
            {
                reader.Position = 0;
                HelpText.SelectAll();
                HelpText.Selection.Load(reader, DataFormats.Rtf);
            }
        }


        private void Hyperlink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Hyperlink link)
            {
                var path = link.NavigateUri.OriginalString;
                var page = (DataContext as Wizard)?.GetPageFor(path);
                var matching = tabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.DataContext == page);
                if (matching != null)
                {
                    tabControl.SelectedItem = matching;
                }
            }
        }


    }
}

using OpenSSL;
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
using System.Windows.Shapes;

namespace openssl_wizard
{

    public partial class Progress : Window
    {

        public Progress()
        {
            InitializeComponent();
        }

        public Progress(Task<Result> result) : this()
        {
            Run(result);
        }

        public async void Run(Task<Result> task)
        {
            await Task.Delay(500);
            var result = await task;
            
            if (result.ExitCode != 0)
                MessageBox.Show(task.Result.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);                   

            Dispatcher.Invoke(Close);
            Dispatcher.Invoke(() => Application.Current.MainWindow.Activate());
        }
    }
}

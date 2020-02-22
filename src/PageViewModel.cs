using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace openssl_wizard
{
    public class PageViewModel<TCommand> : INotifyPropertyChanged
        where TCommand : OpenSSL.OpenSslCommand, new()
    {
        public TCommand Configuration { get; } = new TCommand();

        public RelayCommand GenerateCommand { get; }

        public RelayCommand ExportCommand { get; }

        public RelayCommand CopyCommand { get; }

        public bool HasOutput => !String.IsNullOrEmpty(Configuration.OutputFile);

        public string GenerateDescription => Configuration?.Description;

        public PageViewModel()
        {
            GenerateCommand = new RelayCommand(ShowExecute, () => true);
            ExportCommand = new RelayCommand(ShowExport, () => true);
            CopyCommand = new RelayCommand(CopyToClipboard, () => true);
        }

        public void CopyToClipboard()
        {
            Clipboard.SetText($"openssl {Configuration.Arguments}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void ShowExecute()
        {
            var result = Configuration.Execute();
            var progress = new Progress(result)
            {
                Owner = Application.Current.MainWindow
            };

            progress.ShowDialog();
        }

        public void ShowExport()
        {

            if (!File.Exists(Configuration.OutputFile))
            {
                MessageBox.Show("The output file has not been generated yet.", "Export", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var filter = $"*{Path.GetExtension(Configuration.OutputFile)}";
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = Configuration.OutputFile,
                Filter = $"OpenSSL files ({filter})|{filter}|All files (*.*)|*.*" 
            };

            var result = dialog.ShowDialog(App.Current.MainWindow).GetValueOrDefault(false);
            if (!result)
                return;

            try
            {
                File.Copy(Configuration.OutputFile, dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Export", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

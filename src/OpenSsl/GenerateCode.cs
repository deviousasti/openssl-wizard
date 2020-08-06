using OpenSSL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OpenSSL
{
    public class GenerateCode : OpenSslCommand, INotifyPropertyChanged
    {
        public override string Arguments => throw new NotSupportedException();

        public override string CommandLine => OutputText; 

        public string InputCertificate { get; set; } = "client.key";
        
        public string Output { get; set; }

        public string OutputText { get; set; }

        public override string OutputFile => Output;

        public override string Description => "Generate Code";

        public event PropertyChangedEventHandler PropertyChanged;

        public override async Task<Result> Execute()
        {
            try
            {
                await Task.Yield();
                var contents = File.ReadAllLines(InputCertificate);
                var name = Path.GetFileName(InputCertificate).Replace(".", "_");
                var lines = String.Join("\n", contents.Select(line => $"    \"{line}\\n\""));
                var text =
                    $"// Generated from {InputCertificate}\nconst char* {name} = \n{lines};";

                OutputText = text;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OutputText)));
                return new Result("Generated", 0);
            }
            catch (Exception ex)
            {
                return new Result(ex.Message, ex.HResult);
            }
        }

    }
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSL
{
    public class Result
    {
        public string Message { get; }

        public int ExitCode { get; }

        public Result(string message, int exitCode)
        {
            Message = message;
            ExitCode = exitCode;
        }
    }
    public abstract class OpenSslCommand
    {
        public const string PEM = "PEM";
        public const string DER = "DER";
        public const string P7B = "PKCS7";
        public const string PFX = "PFX";

        public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;

        public abstract string Arguments { get; }

        public abstract string OutputFile { get; }

        public abstract string Description { get; }

        public static int[] BitSizes = new[] { 512, 1024, 2048, 3072, 4096 };

        public static string[] EncyrptionAlgorithms =>
            Parameters("aes128|aes192|aes256|aria128|aria192|aria256|camellia128|camellia192|camellia256|des|des3|idea");

        public static string[] DigestAlgorithms =>
            Parameters("sha256|blake2b512|blake2s256|gost|md4|md5|mdc2|rmd160|sha1|sha224|sha3-224|sha3-256|sha3-384|sha3-512|sha384|sha512|sha512-224|sha512-256|shake128|shake256|sm3");


        public Task<Result> Execute() => Execute(Arguments, WorkingDirectory);

        public static Task<Result> Execute(string arguments, string directory)
        {
            Trace.WriteLine($"openssl {arguments}");

            var startInfo = new ProcessStartInfo(@"openssl.exe", arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                WorkingDirectory = directory,
                CreateNoWindow = false
            };

            var builder = new StringBuilder();
            var tcs = new TaskCompletionSource<Result>();
            try
            {
                var process = Process.Start(startInfo);

                void onExit() => tcs.TrySetResult(new Result(builder.ToString().Replace("+", ""), process.ExitCode));
                void onData(object s, DataReceivedEventArgs e) { if (e.Data == null) onExit(); else builder.Append(e.Data); }

                process.Exited += (s, e) => onExit();
                process.ErrorDataReceived += onData;
                process.OutputDataReceived += onData;

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Win32Exception)
            {
                tcs.SetResult(new Result("Please verify that OpenSSL is present in the path, and is a valid executable.", -1));
            }
            catch (Exception ex)
            {
                tcs.SetResult(new Result(ex.Message, -1));
            }

            return tcs.Task;
        }

        public static string[] Parameters(string parameters)
        {
            return parameters
            .Split('|')
            .Select(s => s.Trim('-'))
            .Where(s => !string.IsNullOrEmpty(s))
            .ToArray();
        }

        public static string QualifyPassphrase(string direction, string passphrase, string keyEncyption)
        {
            return String.IsNullOrWhiteSpace(passphrase) ? null :
                (String.IsNullOrWhiteSpace(keyEncyption) ? "" : $"-{keyEncyption} ") +
                 $"-pass{direction} pass:\"{passphrase}\"";
        }

        public static string ReplaceExtension(string keyName, string extension) =>
            $"{Path.GetFileNameWithoutExtension(keyName)}.{extension.Trim('.')}";

        public static string Flag(string flag, string value)
        {
            return String.IsNullOrWhiteSpace(value) ? null : $"{flag} \"{value}\"";
        }
    }


    public abstract class Signing : OpenSslCommand
    {
        public string KeyEncyption { get; set; } = "";

        public string KeyName { get; set; } = "root.pem";

        public string InputKey { get; set; } = "rsa.key";

        public string InputPassphrase { get; set; } = "";

        public string PassphraseIn =>
            QualifyPassphrase("in", InputPassphrase, KeyEncyption);

        public string OutputPassphrase { get; set; } = "";

        public string PassphraseOut =>
            QualifyPassphrase("out", OutputPassphrase, KeyEncyption) ?? "-nodes";

        public string Digest { get; set; } = "sha256";

        public string[] Digests { get; } = DigestAlgorithms;

        public override string OutputFile => KeyName;
    }


}

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSL
{

    /// <summary>
    /// See
    /// https://www.openssl.org/docs/man1.0.2/man1/genrsa.html
    /// </summary>
    public class RSAKey : OpenSslCommand
    {
        public string KeyName { get; set; } = "rsa.key";

        public int KeySize { get; set; } = 2048;

        public string Passphrase { get; set; } = "password";

        public virtual string OutputPassphrase =>
            QualifyPassphrase("out", Passphrase, KeyEncyption);

        public string KeyEncyption { get; set; } = "aes256";

        public int[] KeySizes => BitSizes;

        public string[] Algorithms => EncyrptionAlgorithms;

        public override string OutputFile => KeyName;

        public override string Arguments => $"genrsa {OutputPassphrase} -out {OutputFile} {KeySize}";

        public override string Description => "Generate Key";
    }
}

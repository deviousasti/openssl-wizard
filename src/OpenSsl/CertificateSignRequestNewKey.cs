namespace OpenSSL
{
    public class CertificateSignRequestNewKey : CertificateSignRequest
    {
        public CertificateSignRequestNewKey()
        {
            KeyName = "server";
            OutputPassphrase = "password";
        }

        public int KeySize { get; set; } = 2048;

        public int[] KeySizes => BitSizes;

        public string[] Algorithms => EncyrptionAlgorithms;

        //openssl req -new -newkey rsa:2048 -nodes -keyout server.key -out server_csr.txt -subj
        public override string Arguments => $"req -new -newkey rsa:{KeySize} {PassphraseOut} -{Digest} -keyout {ReplaceExtension(OutputFile, ".key")} -out {OutputFile} " +
                                            $"-subj \"{Subject}\" {QualifiedExtensions}";

    }
}

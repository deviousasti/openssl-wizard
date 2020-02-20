namespace OpenSSL
{
    /// <summary>
    /// See
    /// https://www.openssl.org/docs/man1.0.2/man1/req.html
    /// </summary>
    public class CreateRootCert : RSAKey
    {
        public CreateRootCert()
        {
            KeyName = "root.crt";
            KeyEncyption = "nodes";
            Passphrase = "";
        }

        public string Digest { get; set; } = "sha256";

        public string[] Digests { get; } =
        Parameters("sha256|blake2b512|blake2s256|gost|md4|md5|mdc2|rmd160|sha1|sha224|sha3-224|sha3-256|sha3-384|sha3-512|sha384|sha512|sha512-224|sha512-256|shake128|shake256|sm3");


        public string Subject { get; set; } = "/CN=example.com";

        public string Extensions { get; set; } = "subjectAltName=DNS:example.com,DNS:example.net,IP:10.0.0.1";

        public int Validity { get; set; } = 365;


        public override string Arguments => $"req -x509 -newkey rsa:{KeySize} -{Digest} -days {Validity} " +
                                            $"{OutputPassphrase} -keyout {ReplaceExtension(OutputFile, ".key")} -out {OutputFile} -subj \"{Subject}\" " +
                                            $"-addext \"{Extensions}\"";

        public override string OutputPassphrase => base.OutputPassphrase ?? "-nodes";

        public override string OutputFile => $"{KeyName}";

        public override string Description => "Generate Certificate";
    }
}

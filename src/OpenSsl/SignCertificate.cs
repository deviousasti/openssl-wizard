namespace OpenSSL
{
    /// <summary>
    /// See
    /// https://www.openssl.org/docs/man1.0.2/man1/req.html
    /// </summary>
    public class SignCertificate : Signing
    {
        public string InputCsr { get; set; } = "server.csr";
        public string CertificateForCA { get; set; } = "root.crt";
        public string KeyForCA { get => InputKey; set => InputKey = value; }
        public string Extensions { get; set; } = "usr_cert";
        public int Validity { get; set; } = 365;

        public SignCertificate()
        {
            KeyEncyption = "";
            KeyName = "server.crt";
            KeyForCA = "root.key";
        }


        //openssl x509 -req -in example.org.csr -CA ca.crt -CAkey ca.key -CAcreateserial -out example.org.crt
        public override string Arguments => $"x509 -req -in {InputCsr} -days {Validity} -CA {CertificateForCA} -CAkey {InputKey} {PassphraseIn} -CAcreateserial " +
                                            $"-out {OutputFile} {Flag("-extensions", Extensions)}";

        public override string OutputFile => KeyName;

        public override string Description => "Sign Certificate";
    }
}

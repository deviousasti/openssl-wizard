namespace OpenSSL
{
    public class CertificateSignRequest : SelfSignedCert
    {
        //openssl req -new -key example.org.key -out example.org.csr
        public override string Arguments => $"req -new -key {InputKey} {PassphraseIn} -{Digest} -out {OutputFile} {PassphraseOut} " +
                                            $"-subj \"{Subject}\" {QualifiedExtensions}";

        public override string OutputFile => ReplaceExtension(KeyName, ".csr");

        public override string Description => "Generate CSR";

    }
}

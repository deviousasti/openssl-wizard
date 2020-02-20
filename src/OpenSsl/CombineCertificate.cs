namespace OpenSSL
{
    public class CombineCertificate : Signing
    {
        public string InputCertificate { get; set; } = "server.crt";

        public CombineCertificate()
        {
            InputKey = "server.key";
            KeyName = "server.pfx";
        }

        public string OutFormat { get; set; } = PFX;
        public string[] OutFormats => new[] { PFX, PEM };

        //-nodes means "don't encrypt private key" but in a PKCS#12 file, the certificates are encrypted as well, so even with -nodes you'd need an export password.
        public new string PassphraseOut => $"-passout pass:{OutputPassphrase}";

        public override string Arguments => $"pkcs12 -inkey {InputKey} {PassphraseIn} -in {InputCertificate} -export {PassphraseOut} -out {OutputFile}";

        public override string Description => "Combine";
    }
}

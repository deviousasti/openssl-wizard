namespace OpenSSL
{
    public class ConvertCertificate : OpenSslCommand
    {


        public string[] InFormats => new[] { PEM, DER, P7B, PFX};

        public string[] OutFormats => new[] { PEM, DER };

        public string InFormat { get; set; } = PEM;

        public string OutFormat { get; set; } = PEM;

        public string InCertificate { get; set; } = "server.crt";
        
        public string OutCertificate { get; set; } = "server.pem";

        public override string Arguments {
            get
            {
                if(InFormat == P7B)
                    return $"pkcs7 -print_certs -in {InCertificate} -out {OutCertificate} -outform {OutFormat}";

                if (InFormat == PFX)
                    return $"pkcs12 -in {InCertificate} -out {OutCertificate} -outform {OutFormat} -nodes";

                return $"x509 -inform {InFormat} -in {InCertificate} -out {OutCertificate} -outform {OutFormat}";
            }
        }

        public override string OutputFile => OutCertificate;

        public override string Description => "Convert Format";
    }
}

using OpenSSL;

namespace openssl_wizard
{
    public class Wizard
    {
        public PageViewModel<OpenSslVersion> Home { get; } = new PageViewModel<OpenSslVersion>();

        public PageViewModel<RSAKey> RSAKeyGen { get; } = new PageViewModel<RSAKey>();

        public PageViewModel<ECDSAKey> ECDSAKeyGen { get; } = new PageViewModel<ECDSAKey>();

        public PageViewModel<SelfSignedCert> SelfSignedCertGen { get; } = new PageViewModel<SelfSignedCert>();

        public PageViewModel<CreateRootCert> RootCertGen { get; } = new PageViewModel<CreateRootCert>();
        
        public PageViewModel<CertificateSignRequest> CsrGen { get; } = new PageViewModel<CertificateSignRequest>();
        
        public PageViewModel<CertificateSignRequestNewKey> CsrGenNew { get; } = new PageViewModel<CertificateSignRequestNewKey>();
        
        public PageViewModel<SignCertificate> SignCert { get; } = new PageViewModel<SignCertificate>();
        
        public PageViewModel<ConvertCertificate> ConvertCert { get; } = new PageViewModel<ConvertCertificate>();
        
        public PageViewModel<CombineCertificate> CombineCert { get; } = new PageViewModel<CombineCertificate>();
    }
}

using OpenSSL;
using System.Collections.Generic;

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
        
        public PageViewModel<GenerateCode> GenerateCode { get; } = new PageViewModel<GenerateCode>();

        protected IDictionary<string, object> Pages => new Dictionary<string, object>
        {
            {"home", Home },
            {"create_rsa", RSAKeyGen },
            {"create_ecdsa", ECDSAKeyGen },
            {"root_ca", RootCertGen },
            {"self_sign", SelfSignedCertGen },
            {"create_csr", CsrGen },
            {"create_csr_key", CsrGenNew },
            {"sign", SignCert },
            {"convert", ConvertCert },
            {"combine", CombineCert },
            {"generate", GenerateCode },
        };

        public object GetPageFor(string key)
        {
            Pages.TryGetValue(key, out object value);
            return value;
        }
    }
}

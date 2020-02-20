using System;
using System.Linq;

namespace OpenSSL
{
    /// <summary>
    /// See
    /// https://www.openssl.org/docs/man1.0.2/man1/req.html
    /// </summary>
    public class SelfSignedCert : Signing
    {
        public string Subject { get; set; } = "/CN=example.com";

        public string Extensions { get; set; } = "subjectAltName=DNS:example.com,DNS:example.net,IP:10.0.0.1";

        public string QualifiedExtensions =>
            String.Join(" ", Extensions.Split('\n').Select(e => Flag("-addext", e)).Where(e => e != null));

        public int Validity { get; set; } = 365;

        public override string Arguments => $"req -x509 -new {""} -key {InputKey} {PassphraseIn} -{Digest} -days {Validity} " +
                                            $"-out {OutputFile} {PassphraseOut} -subj \"{Subject}\" {QualifiedExtensions}";

        public override string Description => "Sign Certificate";
    }
}

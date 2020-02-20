namespace OpenSSL
{
    public class OpenSslVersion : OpenSslCommand
    {
        public override string Arguments => "version";

        public override string OutputFile => default;

        public override string Description => "Get Version";
    }
}

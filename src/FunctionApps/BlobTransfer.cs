using System.Diagnostics.Eventing.Reader;
using System.Formats.Asn1;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using heimdall.Configs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Renci.SshNet;

namespace heimdall.FunctionApps
{
    public class BlobTransfer
    {
        private readonly ILogger<BlobTransfer> _logger;
        private readonly ApplicationConfig _config;
        public BlobTransfer(ILogger<BlobTransfer> logger, ApplicationConfig config)
        {
            _logger = logger;
            _config = config;
        }

        [Function(nameof(BlobTransfer))]
        public async Task Run([BlobTrigger("%TARGET_BLOB_PATH%/{name}", Connection = "STORAGE_CONNECTION_STRING")] Stream stream, string name)
        {
            var uploadFileName = string.IsNullOrEmpty(_config.FileName) ? name : _config.FileName;
            if(_config.AuthenticationMethod == Enums.AuthenticationMethod.PrivateKey)
            {
                if(_config.PrivateKey is null)
                {
                    throw new ArgumentNullException("PrivateKey is required");
                }

                var privateKeyByteArray = Encoding.UTF8.GetBytes(_config.PrivateKey);
                PrivateKeyFile? privateKey = null;
                using (var ms = new MemoryStream(privateKeyByteArray))
                {
                    privateKey = new PrivateKeyFile(ms, _config.PrivateKeyPassphrase);
                }

                var authenticationMethod = new PrivateKeyAuthenticationMethod(_config.SftpUsername, privateKey);
                var sftpConnectionInfo = new ConnectionInfo(_config.SftpEndpoint, _config.SftpUsername, authenticationMethod);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                sftpConnectionInfo.Encoding = Encoding.GetEncoding(_config.SftpFileEncode);
                using (var client = new SftpClient(sftpConnectionInfo))
                {
                    client.Connect();
                    client.UploadFile(stream, $"/{_config.UploadPath.Trim('/')}/{uploadFileName}");
                }
            }
            else if(_config.AuthenticationMethod == Enums.AuthenticationMethod.None)
            {
                throw new NotImplementedException();

            }
            else if(_config.AuthenticationMethod == Enums.AuthenticationMethod.Password)
            {
                throw new NotImplementedException();
            }
        }
    }
}

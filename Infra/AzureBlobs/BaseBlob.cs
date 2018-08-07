using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Infra.AzureBlobs
{
    public class BaseBlob
    {
        public CloudBlobContainer socios { get; set; }

        public BaseBlob()
        {
            var storageAccount = Configuracao.ConnectionStringAzureStorage;
            var blobClient = storageAccount.CreateCloudBlobClient();

            socios = blobClient.GetContainerReference("socios");
            socios.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            socios.CreateIfNotExists();
        }
    }
}

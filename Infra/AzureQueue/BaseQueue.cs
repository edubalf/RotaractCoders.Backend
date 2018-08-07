using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Infra.AzureQueue
{
    public class BaseQueue
    {
        public CloudQueue FilaClube { get; set; }
        public CloudQueue FilaSocio { get; set; }
        public CloudQueue FilaProjeto { get; set; }

        public BaseQueue()
        {
            CloudStorageAccount storageAccount = Configuracao.ConnectionStringAzureStorage;
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            FilaClube = queueClient.GetQueueReference("clube");
            FilaClube.CreateIfNotExists();

            FilaSocio = queueClient.GetQueueReference("socio");
            FilaSocio.CreateIfNotExists();

            FilaProjeto = queueClient.GetQueueReference("projeto");
            FilaProjeto.CreateIfNotExists();
        }
    }
}

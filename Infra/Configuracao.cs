using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    //TODO: Sei que é gambiarra pesada... não me julge, vou fazer certinho depois
    public static class Configuracao
    {
        //desenvolvimento
        //public static CloudStorageAccount ConnectionStringAzureStorage => CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rotaract4430dev;AccountKey=tfki/mfR/+/wAmKgkwVMPfqHxutnTFQWK6vdYu4v6jYwcAQ7aMx5jwKLhSayDxP1GWHHy2ZHhgqUpTeu7+5hew==;EndpointSuffix=core.windows.net");

        //produção
        public static CloudStorageAccount ConnectionStringAzureStorage => CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rotaract;AccountKey=0wV5I1IC9qM5ZWF6PYIGQtnnZm5p1H53FtrerOhHEYP2JZYZGN2Wk9+Bq4+06AvFidzGh0Zg/M0zjklRPF0iqg==;EndpointSuffix=core.windows.net");
    }
}

using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using LuckyDex.Api.Models.AppSettings;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDex.Api.Repositories
{
    public class BlobStorageTrainerRepository : ITrainerRepository
    {
        private readonly Lazy<CloudBlobContainer> _container;

        public BlobStorageTrainerRepository(BlobStorageSettings settings)
        {
            _container = new Lazy<CloudBlobContainer>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var blobClient = account.CreateCloudBlobClient();
                
                var container = blobClient.GetContainerReference(settings.TrainerContainerName);
                container.CreateIfNotExistsAsync().Wait();

                return container;
            });
        }

        public async Task<Trainer> GetAsync(string name)
        {
            var container = _container.Value;

            var blob = container.GetBlockBlobReference(name);
            if (!await blob.ExistsAsync()) return null;

            var bytes = new byte[blob.Properties.Length];

            await blob.DownloadToByteArrayAsync(bytes, 0);

            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<Trainer>(json);
        }

        public async Task PutAsync(string name, Trainer dex)
        {
            var container = _container.Value;
            var blob = container.GetBlockBlobReference(name);
            
            var json = JsonConvert.SerializeObject(dex);

            var bytes = Encoding.UTF8.GetBytes(json);

            await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
        }
    }
}

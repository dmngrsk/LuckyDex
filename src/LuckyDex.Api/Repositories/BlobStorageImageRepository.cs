using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using LuckyDex.Api.Models.AppSettings;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace LuckyDex.Api.Repositories
{
    public class BlobStorageImageRepository : IImageRepository
    {
        private readonly Lazy<CloudBlobContainer> _container;

        public BlobStorageImageRepository(BlobStorageSettings settings)
        {
            _container = new Lazy<CloudBlobContainer>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var blobClient = account.CreateCloudBlobClient();

                var container = blobClient.GetContainerReference(settings.ImageContainerName);
                container.CreateIfNotExistsAsync().Wait();

                return container;
            });
        }

        public async Task<Image> GetAsync(int id)
        {
            var container = _container.Value;

            var blob = container.GetBlockBlobReference(GetFileName(id));
            if (!await blob.ExistsAsync()) return null;

            var bytes = new byte[blob.Properties.Length];

            await blob.DownloadToByteArrayAsync(bytes, 0);

            return new Image { Bytes = bytes };
        }

        public async Task PutAsync(int id, Image image)
        {
            var container = _container.Value;
            var blob = container.GetBlockBlobReference(GetFileName(id));

            await blob.UploadFromByteArrayAsync(image.Bytes, 0, image.Bytes.Length);
        }

        private static string GetFileName(int id)
        {
            return $"{id:000}.png";
        }
    }
}

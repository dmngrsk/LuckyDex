namespace LuckyDex.Api.Models.AppSettings
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; }
        public string TrainerContainerName { get; set; }
        public string ImageContainerName { get; set; }
    }
}

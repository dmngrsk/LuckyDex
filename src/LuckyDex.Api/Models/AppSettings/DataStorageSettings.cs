namespace LuckyDex.Api.Models.AppSettings
{
    public class DataStorageSettings
    {
        public BlobStorageSettings BlobStorage { get; set; }
        public TableStorageSettings TableStorage { get; set; }
    }
}

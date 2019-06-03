namespace LuckyDex.Api.Models.AppSettings
{
    public class TableStorageSettings
    {
        public string ConnectionString { get; set; }
        public string TrainerTableName { get; set; }
        public string DexEntriesTableName { get; set; }
    }
}

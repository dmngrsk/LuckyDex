using Microsoft.Azure.Cosmos.Table;

namespace LuckyDex.Api.Models.TableStorage
{
    public class TrainerEntity : TableEntity
    {
        public string Comment { get; set; }

        public TrainerEntity()
        {
        }

        public TrainerEntity(string name, string comment)
        {
            RowKey = name;
            Comment = comment;

            PartitionKey = "x";
            ETag = "*";
        }
    }
}

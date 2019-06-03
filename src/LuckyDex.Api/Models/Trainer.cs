namespace LuckyDex.Api.Models
{
    public class Trainer
    {
        public string Name { get; set; }
        public string Comment { get; set; }

        public static Trainer Default(string name)
        {
            return new Trainer { Name = name };
        }
    }
}

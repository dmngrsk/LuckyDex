namespace LuckyDex.Api.Models
{
    public class Routing
    {
        public string Name { get; set; }
        public string Route { get; set; }

        public static Routing Default(string name)
        {
            return new Routing { Name = name };
        }
    }
}

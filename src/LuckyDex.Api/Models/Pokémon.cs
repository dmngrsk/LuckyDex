namespace LuckyDex.Api.Models
{
    public class Pokémon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTradeable { get; set; }
        public bool IsLowestForm { get; set; }
        public bool IsLegendary { get; set; }
    }
}

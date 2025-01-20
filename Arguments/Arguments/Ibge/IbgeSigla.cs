namespace Arguments.Entities.Ibge
{
    public class OutputGetByCodeIbge
    {
        public int id { get; set; }
        public string? sigla { get; set; }
        public string? nome { get; set; }
        public OutputGetByCodeRegionIbge? regiao { get; set; }
    }
}

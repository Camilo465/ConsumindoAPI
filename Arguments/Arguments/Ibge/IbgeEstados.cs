namespace Arguments.Entities.Ibge
{
    public class OutPutGetAll
    {
        public int Id { get; set; }
        public string? Sigla { get; set; }
        public string? Nome { get; set; }
        public OutputGetByCodeRegionIbge? Regiao { get; set; }
    }
}

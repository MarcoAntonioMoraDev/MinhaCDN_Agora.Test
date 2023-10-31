namespace CandidateTesting.MarcoAntonioMoraPalacios.Domain.Contracts
{
    public class MinhaCdnInput
    {
        public int SizeResponse { get; set; }
        public int StatusCode { get; set; }
        public string StatusCache { get; set; } = string.Empty;
        public string PathRequest { get; set; } = string.Empty;
        public decimal TimeSpent { get; set; }
    }
}

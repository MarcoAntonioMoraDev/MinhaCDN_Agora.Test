namespace CandidateTesting.MarcoAntonioMoraPalacios.Domain.Entities
{
    public class AgoraInput
    {
        public string Provider { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string UriPath { get; set; } = string.Empty;
        public int TimeSpent { get; set; }
        public int SizeResponse { get; set; }
        public string StatusCache { get; set; } = string.Empty;
    }
}

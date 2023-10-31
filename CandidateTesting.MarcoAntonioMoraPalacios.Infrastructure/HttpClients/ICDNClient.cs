namespace CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.HttpClients
{
    public interface ICDNClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}

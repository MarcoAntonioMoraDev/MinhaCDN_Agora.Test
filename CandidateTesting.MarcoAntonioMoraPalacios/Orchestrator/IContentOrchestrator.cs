namespace CandidateTesting.MarcoAntonioMoraPalacios.Application.Orchestrator
{
    public interface IContentOrchestrator
    {
        Task<string> StartAsync(string sourceUrl, string destinationPath);
    }
}

using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generators;
using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.Generaties;
using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.HttpClients;

namespace CandidateTesting.MarcoAntonioMoraPalacios.Application.Orchestrator
{
    public class ContentOrchestrator : IContentOrchestrator
    {
        private readonly ICDNClient _cdnClient;
        private readonly ILogGenerator _logGenerate;
        private readonly IFileGenerator _fileGenerate;

        public ContentOrchestrator(
            ICDNClient cdnClient,
            ILogGenerator logGenerate,
            IFileGenerator fileGenerate)
        {
            _cdnClient =
                cdnClient
                ?? throw new ArgumentNullException(nameof(cdnClient));

            _logGenerate =
                logGenerate
                ?? throw new ArgumentNullException(nameof(logGenerate));

            _fileGenerate =
                fileGenerate
                ?? throw new ArgumentNullException(nameof(fileGenerate));
        }

        public async Task<string> StartAsync(string sourceUrl, string destinationPath)
        {
            Console.WriteLine($"[{DateTime.Now}] - Querying url: {sourceUrl}");
            var cdnContent = await _cdnClient.GetAsync(sourceUrl);

            var logContent = cdnContent.Content.ReadAsStream();

            if (cdnContent.IsSuccessStatusCode)
            {
                var fileContent = _logGenerate.Create(logContent);

                var fileCreated = _fileGenerate.Create(destinationPath, fileContent);

                return fileCreated;
            }

            Console.WriteLine($"[{DateTime.Now}] - Was not possible query the following url: {sourceUrl}");
            return "";
        }
    }
}

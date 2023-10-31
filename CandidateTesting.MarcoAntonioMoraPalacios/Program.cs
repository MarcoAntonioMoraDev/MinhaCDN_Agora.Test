using CandidateTesting.MarcoAntonioMoraPalacios.Application.Orchestrator;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generaties;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generators;
using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.Generaties;
using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using System.Web;

namespace CandidateTesting.MarcoAntonioMoraPalacios
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"[{DateTime.Now}] - Configuring Services...");
            var services = new ServiceCollection();

            services.AddScoped(_ => new HttpClient());
            services.AddScoped<ICDNClient, CDNClient>();
            services.AddScoped<ILogGenerator, LogGenerator>();
            services.AddScoped<IFileGenerator, FileGenerator>();
            services.AddScoped<IContentOrchestrator, ContentOrchestrator>();

            var serviceProvider = services.BuildServiceProvider();

            var logFormatterOrchestrator =
                serviceProvider.GetRequiredService<IContentOrchestrator>();

            if (args.Length == 2)
            {
                var url = GetUrl(args[0]);
                var destination = GetFileDestination(args[1]);

                if (IsDataCorrectlyProvided(url, destination))
                {
                    Console.WriteLine(
                        $"[{DateTime.Now}] - Obtaining data");

                    GetDataAsync(logFormatterOrchestrator, url, destination).Wait();

                    Console.WriteLine($"[{DateTime.Now}] - Content saved in '{destination}' ");
                }
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now}] - Incorrect parameters, follow the sequence: convert.exe url destination"); 
                return;
            }
        }

        private static async Task GetDataAsync(
            IContentOrchestrator logFormatterOrchestrator,
            string sourceUrl,
            string destinationPath)
        {
            Console.WriteLine($"[{DateTime.Now}] - Invoking Orchestrator...");
            await logFormatterOrchestrator.StartAsync(sourceUrl, destinationPath);
        }

        private static string GetUrl(string url)
        {
            if (ValidateUrl(url))
            {
                return url;
            }

            Console.WriteLine($"[{DateTime.Now}] - ERROR: Please, provide a valid source url");
            return "";
        }

        private static bool ValidateUrl(string url)
        {
            try
            {
                string encodedUrl = HttpUtility.UrlPathEncode(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetFileDestination(string destination)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), destination.Replace("./", ""));

            return path;
        }

        private static bool IsDataCorrectlyProvided(string url, string destination)
        {
            return !(string.IsNullOrEmpty(url) || string.IsNullOrEmpty(destination));
        }
    }

}


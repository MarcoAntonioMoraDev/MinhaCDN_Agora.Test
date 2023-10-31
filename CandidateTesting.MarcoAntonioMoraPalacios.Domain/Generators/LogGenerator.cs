using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Entities;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generators;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Mappers;
using System.Text;

namespace CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generaties
{
    public class LogGenerator : ILogGenerator
    {
        public LogGenerator() { }

        public string Create(Stream LogContent)
        {
            var minhaCDNContent = LogContent.GetFromResponseStream();
            Console.WriteLine($"[{DateTime.Now}] - Mapped Minha CDN Successfully");

            var agoraInput = minhaCDNContent.MapFromMinhaCDN();
            Console.WriteLine($"[{DateTime.Now}] - Mapped to Agora Successfully");

            var fileContent = CreateFile(agoraInput);
            Console.WriteLine($"[{DateTime.Now}] - Conversion finished.");
            Console.WriteLine(
                $"[{DateTime.Now}] - Conversion Data: \r\n" +
                $"--------------------------------------------------- \r\n " +
                $"{fileContent} \r\n" +
                $"---------------------------------------------------");

            return fileContent;
        }

        private static string CreateFile(AgoraInputs agoraInput)
        {
            var contentBuilder = new StringBuilder();
            
            contentBuilder.AppendLine($"#Date: {agoraInput.Date}");
            contentBuilder.AppendLine(
                "#Fields: " +
                "provider " +
                "http-method " +
                "status-code " +
                "uri-path " +
                "time-spent " +
                "size-response " +
                "status-cache");

            foreach (var input in agoraInput.Inputs)
            {
                var line = BuildContentLine(input);

                contentBuilder.AppendLine(line);
            }

            return contentBuilder.ToString();
        }


        private static string BuildContentLine(AgoraInput input)
        {
            var line =
                    $"\"{input.Provider}\" " +
                    $"{input.HttpMethod} " +
                    $"{input.StatusCode} " +
                    $"{input.UriPath} " +
                    $"{input.TimeSpent} " +
                    $"{input.SizeResponse} " +
                    $"{input.StatusCache}";

            return line;
        }

    }
}

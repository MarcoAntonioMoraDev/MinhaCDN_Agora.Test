using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Contracts;
using System.Globalization;

namespace CandidateTesting.MarcoAntonioMoraPalacios.Domain.Mappers
{
    public static class MinhaCDNMapper
    {
        public static MinhaCdnInputs GetFromResponseStream(this Stream stream)
        {
            using var reader = new StreamReader(stream);

            var stringContent = reader.ReadToEnd();

            Console.WriteLine(
                $"[{DateTime.Now}] - Response: \r\n" +
                $"{stringContent} \r\n");

            var contentSplitted = stringContent.Split("\r\n");

            var minhaCDNDataInputs = new MinhaCdnInputs();

            foreach (var log in contentSplitted)
            {
                var content = GetContent(log);

                if(content == null)
                {
                    continue;
                }

                minhaCDNDataInputs.Inputs.Add(content);
            }

            return minhaCDNDataInputs;
        }

        private static MinhaCdnInput? GetContent(string contentLine)
        {
            var fields = contentLine.Split('|');

            if (fields.Length == 5)
            {
                var entry = new MinhaCdnInput
                {
                    SizeResponse = GetSizeResponse(fields[0]),
                    StatusCode = GetStatusCode(fields[1]),
                    StatusCache = GetStatusCache(fields[2]),
                    PathRequest = GetPathRequest(fields[3]),
                    TimeSpent = GetTimeSpent(fields[4])

                };

                return entry;
            }

            return null;
        }

        private static int GetStatusCode(string statusCode)
        {
            return int.Parse(statusCode);
        }
        private static decimal GetTimeSpent(string timeSpent)
        {
            var cultureInfo = new CultureInfo("en-US");
            return decimal.Parse(timeSpent, cultureInfo);
        }

        private static int GetSizeResponse(string sizeResponse)
        {
            return int.Parse(sizeResponse);
        }

        private static string GetStatusCache(string statusCache)
        {
            return statusCache;
        }

        private static string GetPathRequest(string pathRequest)
        {
            return pathRequest;
        }
    }
}
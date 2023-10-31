using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Contracts;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Entities;

namespace CandidateTesting.MarcoAntonioMoraPalacios.Domain.Mappers
{
    public static class AgoraMapper
    {
        public static AgoraInputs MapFromMinhaCDN(this MinhaCdnInputs minhaCDNDataInputs)
        {
            var agoraInput = new AgoraInputs
            {
                Version = 1.0m,
                Date = DateTime.Now
            };

            foreach (var minhaCDNDataInput in minhaCDNDataInputs.Inputs)
            {
                var agoraDataInput = new AgoraInput
                {
                    StatusCache = GetStatusCache(minhaCDNDataInput.StatusCache),
                    Provider = "MINHA CDN",
                    SizeResponse = minhaCDNDataInput.SizeResponse,
                    StatusCode = minhaCDNDataInput.StatusCode,
                    TimeSpent = GetTimeSpent(minhaCDNDataInput.TimeSpent),
                    HttpMethod = GetMethod(minhaCDNDataInput.PathRequest),
                    UriPath = GetRequestedPath(minhaCDNDataInput.PathRequest)
                };

                agoraInput.Inputs.Add(agoraDataInput);
            }

            return agoraInput;

        }

        private static string GetStatusCache(string statusCache)
        {
            return statusCache switch
            {
                "INVALIDATE" => "REFRESH_HIT",
                _ => statusCache,
            };
        }

        private static int GetTimeSpent(decimal timeSpent)
        {
            return (int)Math.Round(timeSpent);
        }

        private static string GetMethod(string pathRequest)
        {
            var httpMethod = pathRequest.Replace("\"", "");
            var splittedPath = httpMethod.Split(' ');

            httpMethod = splittedPath[0];

            return httpMethod;
        }

        private static string GetRequestedPath(string pathRequest)
        {
            var httpMethod = pathRequest.Replace("\"", "");
            var splittedPath = httpMethod.Split(' ');

            httpMethod = splittedPath[1];

            return httpMethod;
        }
    }
}

using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Contracts;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Mappers;
using FluentAssertions;

namespace CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.Domain.Mappers
{
    public class AgoraMapperTests
    {
        [Fact]
        public void MapFromMinhaCDN_Should_Map_To_AgoraCDN_Correctly()
        {
            // Preparação
            var minhaCDNDataInputs = new MinhaCdnInputs
            {
                Inputs = new System.Collections.Generic.List<MinhaCdnInput>
                {
                    new MinhaCdnInput
                    {
                        SizeResponse = 312,
                        StatusCode = 200,
                        StatusCache = "HIT",
                        PathRequest = "\"GET /robots.txt HTTP/1.1\"",
                        TimeSpent = 100.2m
                    }
                }
            };

            // Ação
            var result = AgoraMapper.MapFromMinhaCDN(minhaCDNDataInputs);

            // Assert
            result.Should().NotBeNull();
            result.Version.Should().Be(1.0m);
            result.Date.Should().BeBefore(DateTime.Now.AddSeconds(1));
            result.Inputs.Should().HaveCount(1);
            result.Inputs[0].SizeResponse.Should().Be(312);
            result.Inputs[0].StatusCode.Should().Be(200);
            result.Inputs[0].StatusCache.Should().Be("HIT");
            result.Inputs[0].Provider.Should().Be("MINHA CDN");
            result.Inputs[0].TimeSpent.Should().Be(100);
            result.Inputs[0].HttpMethod.Should().Be("GET");
            result.Inputs[0].UriPath.Should().Be("/robots.txt");
        }
    }
}

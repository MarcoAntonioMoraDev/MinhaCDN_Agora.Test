using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Mappers;
using FluentAssertions;

namespace CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.Domain.Mappers
{
    public class MinhaCDNMapperTests
    {
        [Fact]
        public void GetFromResponseStream_Should_Parse_Content_Correctly()
        {
            // Preparação
            var content = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));

            // Ação
            var result = MinhaCDNMapper.GetFromResponseStream(stream);

            // Assert
            result.Should().NotBeNull();
            result.Inputs.Should().NotBeEmpty();
            result.Inputs[0].SizeResponse.Should().Be(312);
            result.Inputs[0].StatusCode.Should().Be(200);
            result.Inputs[0].StatusCache.Should().Be("HIT");
            result.Inputs[0].PathRequest.Should().Be("\"GET /robots.txt HTTP/1.1\"");
            result.Inputs[0].TimeSpent.Should().Be(100.2m);
        }

    }
}

using AutoFixture.Idioms;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Contracts;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Entities;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generaties;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generators;
using CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.AutoFixture.Attributes;
using FluentAssertions;
using System.Text;

namespace CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.Domain.Generaties
{
    public class LogFactoryTests
    {
        [Theory, AutoNSubstituteData]
        public void LogFactory_Should_Guard_Its_Clauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(LogGenerator).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void LogFactory_Should_Implement_ILogFactory(LogGenerator sut)
        {
            sut.Should().BeAssignableTo<ILogGenerator>();
        }

        [Fact]
        public void Create_Should_Return_Log_Formatted_As_Agora_Format()
        {
            var stream = SetupContentStream("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2");
         
            var expected = "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT";

            var sut = new LogGenerator();

            var result = sut.Create(stream);

            var actual = result.Split("\r\n")[2];

            actual.Should().Be(expected);
        }

        private static Stream SetupContentStream(string content)
        {
            var contentBytes = Encoding.UTF8.GetBytes(content);

            var stream = new MemoryStream(contentBytes);

            return stream;
        }

        private static MinhaCdnInputs SetupMinhaCDNDataInputs()
        {
            return new MinhaCdnInputs
            {
                Inputs = new List<MinhaCdnInput> {
                new MinhaCdnInput {
                    SizeResponse = 312,
                    StatusCode = 200,
                    StatusCache = "HIT",
                    PathRequest = "\"GET /robots.txt HTTP/1.1\"",
                    TimeSpent = 100.2m
                }
            }
            };
        }

        private static AgoraInputs SetupAgoraInput()
        {
            return new AgoraInputs
            {
                Date = DateTime.Now,
                Version = 1.0m,
                Inputs = new List<AgoraInput>
                {
                    new AgoraInput
                    {
                    SizeResponse = 312,
                    StatusCode = 200,
                    StatusCache = "HIT",
                    Provider = "MINHA CDN",
                    HttpMethod = "GET",
                    TimeSpent = 100,
                    UriPath = "/robots.txt"

                    }
                }

            };

        }
    }
}

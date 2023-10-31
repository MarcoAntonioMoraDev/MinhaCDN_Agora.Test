using AutoFixture.Idioms;
using CandidateTesting.MarcoAntonioMoraPalacios.Application.Orchestrator;
using CandidateTesting.MarcoAntonioMoraPalacios.Domain.Generators;
using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.Generaties;
using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.HttpClients;
using CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.AutoFixture.Attributes;
using NSubstitute;

namespace CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.Application.Orchestrators
{
    public class LogFormattedOrchestratorTest
    {
        [Theory, AutoNSubstituteData]
        public void LogFormatterOrchestrator_Should_Guard_Its_Clauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ContentOrchestrator).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void LogFormatterOrchestrator_Should_Implement_ILogFormatterOrchestrator(
            ContentOrchestrator sut)
        {
            Assert.IsAssignableFrom<IContentOrchestrator>(sut);
        }

        [Theory, AutoNSubstituteData]
        public async Task StartAsync_Should_Invoke_Factories_If_cdnContent_Is_Successfull(
            ICDNClient cdnClient,
            ILogGenerator logGenerator,
            IFileGenerator fileGenerate,
            HttpResponseMessage responseMessage,
            string sourceUrl,
            string destinationPath,
            string logGenerateResult)
        {
            responseMessage.StatusCode = System.Net.HttpStatusCode.OK;

            cdnClient.GetAsync(sourceUrl).Returns(responseMessage);

            logGenerator.Create(responseMessage.Content.ReadAsStream()).Returns(logGenerateResult);

            var sut = new ContentOrchestrator(cdnClient, logGenerator, fileGenerate);

            await sut.StartAsync(sourceUrl, destinationPath);

            fileGenerate.Received(1).Create(destinationPath, logGenerateResult);
        }

        [Theory, AutoNSubstituteData]
        public async Task StartAsync_Should_Not_Invoke_Factories_When_cdnContent_Is_Not_Successfull(
            ICDNClient cdnClient,
            ILogGenerator logGenerate,
            IFileGenerator fileGenerate,
            HttpResponseMessage responseMessage,
            string sourceUrl,
            string destinationPath)
        {
            responseMessage.StatusCode = System.Net.HttpStatusCode.NotFound;

            cdnClient.GetAsync(sourceUrl).Returns(responseMessage);

            var sut = new ContentOrchestrator(cdnClient, logGenerate, fileGenerate);

            await sut.StartAsync(sourceUrl, destinationPath);

            fileGenerate.Received(0).Create(Arg.Any<string>(), Arg.Any<string>());
            logGenerate.Received(0).Create(Arg.Any<Stream>());
        }
    }
}

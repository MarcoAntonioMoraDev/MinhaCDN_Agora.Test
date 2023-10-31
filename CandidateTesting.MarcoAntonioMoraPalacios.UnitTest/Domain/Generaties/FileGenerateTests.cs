using CandidateTesting.MarcoAntonioMoraPalacios.Infrastructure.Generaties;
using CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.AutoFixture.Attributes;
using FluentAssertions;

namespace CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.Domain.Generaties
{
    public class FileGenerateTests
    {
        [Theory, AutoNSubstituteData]
        public void FileFactory_Should_Implement_IFileGenerate(FileGenerator sut)
        {
            sut.Should().BeAssignableTo<IFileGenerator>();
        }

        [Theory, AutoNSubstituteData]
        public void Create_Should_Write_File_With_Content(string content)
        {
            var fileGenerate = new FileGenerator();
            var path = $"{Guid.NewGuid()}.txt";

            var filePath = fileGenerate.Create(path, content);

            File.Exists(filePath).Should().BeTrue();

            var fileContent = File.ReadAllText(filePath);
            fileContent.Should().Be(content);
        }

        [Theory, AutoNSubstituteData]
        public void Create_Should_Create_Directory_If_Not_Exists(string directory, string content)
        {
            var fileGenerate = new FileGenerator();
            var path = Path.Combine(directory, "test.txt");

            var filePath = fileGenerate.Create(path, content);

            File.Exists(filePath).Should().BeTrue();
            Directory.Exists(directory).Should().BeTrue();
        }
    }
}

namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;
    using System.IO;

    [TestFixture]
    public class CliRunnerTests
    {
        [Test]
        public void CliRunnerBuildJavaScriptTest()
        {
            // Arrange
            var runner = new CliRunner(new CliOptions(new[] { "-i:x", "-o:y" }));

            // Act
            var result = runner.BuildJavaScript("app", string.Empty);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void CliRunnerBuildCompleteJavaScriptTest()
        {
            // Arrange
            var parser = new AssemblyParser("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests.DTOs", "en");
            var metadata = parser.Parse();
            var runner = new CliRunner(new CliOptions(new[] { "-i:x", "-o:y" }));

            // Act
            var result = runner.BuildJavaScript("app", metadata.ToString());

            // Assert
            var appValdrJsFile = File.OpenText("../../app/app.valdr.js");
            var fileContent = appValdrJsFile.ReadToEnd();
            Assert.That(result.Replace("\r", string.Empty), Is.EqualTo(fileContent.Replace("\r", string.Empty)));
        }
    }
}
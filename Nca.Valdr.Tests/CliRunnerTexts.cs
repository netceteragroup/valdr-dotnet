namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;

    [TestFixture]
    public class CliRunnerTests
    {
        [TestCase]
        public void CliRunnerBuildJavaScriptTest()
        {
            var runner = new CliRunner(new CliOptions(new [] { "-i:x", "-o:y" }));
            var result = runner.BuildJavaScript("app", "");
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }
}
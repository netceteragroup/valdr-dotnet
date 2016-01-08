namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CliRunnerTests
    {
        [TestCase]
        public void CliRunnerBuildJavaScriptTest()
        {
            var result = CliRunner.BuildJavaScript("app", "");
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }
}
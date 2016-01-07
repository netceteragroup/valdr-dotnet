namespace Nca.Valdr.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ProgramTests
    {
        [TestCase]
        public void MainWithoutParameterTest()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Program.Main(null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: args"));
        }

        [TestCase]
        public void MainInvalidParameterTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Program.Main(new[] { "-x:wrong"}));
            Assert.That(ex.Message, Is.EqualTo("Invalid parameter."));
        }

        [TestCase]
        public void MainParameterMissingTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Program.Main(new[] { "-a:app" }));
            Assert.That(ex.Message, Is.EqualTo("Required parameter missing."));
        }

        [TestCase]
        public void BuildJavaScriptTest()
        {
            var result = Program.BuildJavaScript("app", "");
            Assert.That(result, Is.Not.Null);
        }
    }
}
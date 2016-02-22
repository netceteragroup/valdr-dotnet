namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CliOptionsTests
    {
        private CliOptions _options;

        [TestCase]
        public void CliOptionsWithoutParameterTest()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _options = new CliOptions(null));
            Assert.That(ex.Message.Substring(0, 21), Is.EqualTo("Value cannot be null."));
            Assert.That(_options, Is.Null);
        }

        [TestCase]
        public void CliOptionsInvalidParameterTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => _options = new CliOptions(new[] { "-x:wrong" }));
            Assert.That(ex.Message, Is.EqualTo("Invalid parameter."));
            Assert.That(_options, Is.Null);
        }

        [TestCase]
        public void CliOptionsParameterMissingTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => _options = new CliOptions(new[] { "-a:app" }));
            Assert.That(ex.Message, Is.EqualTo("Required parameter missing."));
            Assert.That(_options, Is.Null);
        }
    }
}
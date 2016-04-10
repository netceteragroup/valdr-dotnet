namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;

    [TestFixture]
    public class CliWriterTests
    {
        [Test]
        public void CliWriterWriteTest()
        {
            using (var writer = new CliWriter())
            {
                Assert.DoesNotThrow(() => writer.Write("Write test"));
            }
        }

        [Test]
        public void CliWriterWriteLineTest()
        {
            using (var writer = new CliWriter())
            {
                Assert.DoesNotThrow(() => writer.WriteLine("WriteLine test"));
            }
        }
    }
}
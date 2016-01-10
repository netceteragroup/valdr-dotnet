namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;

    [TestFixture]
    public class CliWriterTests
    {
        [TestCase]
        public void CliWriterWriteTest()
        {
            using (var writer = new CliWriter())
            {
                Assert.DoesNotThrow(() => writer.Write("Write test"));
            }
        }

        [TestCase]
        public void CliWriterWriteLineTest()
        {
            using (var writer = new CliWriter())
            {
                Assert.DoesNotThrow(() => writer.WriteLine("WriteLine test"));
            }
        }
    }
}
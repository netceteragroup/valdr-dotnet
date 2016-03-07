namespace Nca.Valdr.Tests
{
    using Console;
    using NUnit.Framework;

    [TestFixture]
    public class AssemblyParserTests
    {
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests.DTOs", null)]
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests", null)]
        [TestCase("Nca.Valdr.Tests.dll", null, null)]
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests.DTOs", "en")]
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests", "en")]
        [TestCase("Nca.Valdr.Tests.dll", null, "en")]
        public void AssemblyParserParseEnTest(string assemblyFile, string targetNamespace, string culture)
        {
            // Arrange
            var parser = new AssemblyParser(assemblyFile, targetNamespace, culture);

            // Act
            var result = parser.Parse();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "Number of children not as expected.");
            Assert.That((string)result.SelectToken("address.city.required.message"), Is.EqualTo("City is required."));
            Assert.That((int)result.SelectToken("address.zipCode.size.min"), Is.EqualTo(4));
            Assert.That((int)result.SelectToken("person.age.max.value"), Is.EqualTo(99));
            Assert.That((string)result.SelectToken("person.url.url.message"), Is.EqualTo("Must be a valid URL."));
        }

        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests.DTOs", "de")]
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests", "de")]
        [TestCase("Nca.Valdr.Tests.dll", null, "de")]
        public void AssemblyParserParseDeTest(string assemblyFile, string targetNamespace, string culture)
        {
            // Arrange
            var parser = new AssemblyParser(assemblyFile, targetNamespace, culture);

            // Act
            var result = parser.Parse();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "Number of children not as expected.");
            Assert.That((string)result.SelectToken("address.city.required.message"), Is.EqualTo("Ort ist obligatorisch."));
            Assert.That((int)result.SelectToken("address.zipCode.size.min"), Is.EqualTo(4));
            Assert.That((int)result.SelectToken("person.age.max.value"), Is.EqualTo(99));
            Assert.That((string)result.SelectToken("person.url.url.message"), Is.EqualTo("Must be a valid URL."));
        }
    }
}
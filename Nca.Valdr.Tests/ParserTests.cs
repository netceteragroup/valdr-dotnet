namespace Nca.Valdr.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ParserTests
    {
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests.DTOs")]
        [TestCase("Nca.Valdr.Tests.dll", "Nca.Valdr.Tests")]
        [TestCase("Nca.Valdr.Tests.dll", "")]
        public void ParseTest(string assemblyFile, string targetNamespace)
        {
            // Arrange

            // Act
            var result = Parser.Parse(assemblyFile, targetNamespace);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "Number of children not as expected.");
            Assert.That((string)result.SelectToken("address.city.required.message"), Is.EqualTo("City is required."));
            Assert.That((int)result.SelectToken("address.zipCode.size.min"), Is.EqualTo(4));
            Assert.That((int)result.SelectToken("person.age.max.value"), Is.EqualTo(99));
            Assert.That((string)result.SelectToken("person.url.url.message"), Is.EqualTo("Must be a valid URL."));
        }
    }
}
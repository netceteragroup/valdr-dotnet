namespace Nca.Valdr.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class ParserTests
    {
        [TestCase("Nca.Valdr.Tests.DTOs", null)]
        [TestCase("Nca.Valdr.Tests", null)]
        [TestCase(null, null)]
        [TestCase("Nca.Valdr.Tests.DTOs", "en")]
        [TestCase("Nca.Valdr.Tests", "en")]
        [TestCase(null, "en")]
        public void ParserParseEnTest(string targetNamespace, string culture)
        {
            // Arrange
            var parser = new Parser();

            // Act
            var specificCulture = !string.IsNullOrEmpty(culture) ? CultureInfo.CreateSpecificCulture(culture) : null;

            var result = parser.Parse(specificCulture, targetNamespace, new ValdrTypeAttributeDescriptor(typeof(ValdrTypeAttribute), nameof(ValdrTypeAttribute.Name)), nameof(ValdrMemberAttribute), Assembly.GetAssembly(typeof(ParserTests)));

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "Number of children not as expected.");
            Assert.That((string)result.SelectToken("address.city.required.message"), Is.EqualTo("City is required."));
            Assert.That((int)result.SelectToken("address.zipCode.size.min"), Is.EqualTo(4));
            Assert.That((int)result.SelectToken("person.age.max.value"), Is.EqualTo(99));
            Assert.That((string)result.SelectToken("person.url.url.message"), Is.EqualTo("Must be a valid URL."));
        }

        [TestCase("Nca.Valdr.Tests.DTOs", "de")]
        [TestCase("Nca.Valdr.Tests", "de")]
        [TestCase(null, "de")]
        public void ParserParseDeTest(string targetNamespace, string culture)
        {
            // Arrange
            var parser = new Parser();

            // Act
            var specificCulture = !string.IsNullOrEmpty(culture) ? CultureInfo.CreateSpecificCulture(culture) : null;

            var result = parser.Parse(specificCulture, targetNamespace, new ValdrTypeAttributeDescriptor(typeof(ValdrTypeAttribute), nameof(ValdrTypeAttribute.Name)), nameof(ValdrMemberAttribute), Assembly.GetAssembly(typeof(ParserTests)));

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "Number of children not as expected.");
            Assert.That((string)result.SelectToken("address.city.required.message"), Is.EqualTo("Ort ist obligatorisch."));
            Assert.That((int)result.SelectToken("address.zipCode.size.min"), Is.EqualTo(4));
            Assert.That((int)result.SelectToken("person.age.max.value"), Is.EqualTo(99));
            Assert.That((string)result.SelectToken("person.url.url.message"), Is.EqualTo("Must be a valid URL."));
        }

        [Test]
        public void ParserParseWithoutParameterTest()
        {
            // Arrange
            var parser = new Parser();
            JObject result = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => result = parser.Parse(null, null, null, null));

            // Assert
            Assert.That(ex.Message.Substring(0, 21), Is.EqualTo("Value cannot be null."));
            Assert.That(result, Is.Null);
        }
    }
}
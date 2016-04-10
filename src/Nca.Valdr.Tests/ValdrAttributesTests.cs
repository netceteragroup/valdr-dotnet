namespace Nca.Valdr.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ValdrAttributesTests
    {
        [Test]
        public void ValdrTypeAttributeDescriptorWithoutParameterTest()
        {
            // Arrange
            ValdrTypeAttributeDescriptor type = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => type = new ValdrTypeAttributeDescriptor(null, null));

            // Assert
            Assert.That(ex.Message.Substring(0, 21), Is.EqualTo("Value cannot be null."));
            Assert.That(type, Is.Null);
        }

        [Test]
        public void ValdrTypeAttributeDescriptorTest()
        {
            // Arrange and Act
            var type = new ValdrTypeAttributeDescriptor(typeof(ValdrTypeAttribute), nameof(ValdrTypeAttribute.Name));

            // Assert
            Assert.That(type.TypeName, Is.EqualTo(typeof(ValdrTypeAttribute).Name), "TypeName not as expected.");
            Assert.That(type.ValdrTypePropertyName, Is.EqualTo(nameof(ValdrTypeAttribute.Name)), "ValdrTypePropertyName not as expected.");
        }

        [Test]
        public void ValdrMemberAttributeNameTest()
        {
            // Arrange and Act
            var member = new ValdrMemberAttribute { Name = "test" };

            // Assert
            Assert.That(member.Name, Is.EqualTo("test"));
        }
    }
}
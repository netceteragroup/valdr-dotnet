namespace Nca.Valdr.Tests
{
    using DTOs;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void PersonValidatorInvalidTest()
        {
            // Arrange
            var person = new PersonDto
            {
                ValidTo = DateTime.MinValue,
                Birthday = DateTime.MaxValue
            };
            var context = new ValidationContext(person, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(person, context, results, validateAllProperties: true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(results[0].ErrorMessage, Is.EqualTo("First name is required."));
            Assert.That(results[1].ErrorMessage, Is.EqualTo("Last name is required."));
            Assert.That(results[2].ErrorMessage, Is.EqualTo("validTo must be in the future"));
            Assert.That(results[3].ErrorMessage, Is.EqualTo("Birthday must be in the past."));
        }

        [Test]
        public void PersonValidatorValidTest()
        {
            // Arrange
            var person = new PersonDto
            {
                FirstName = "Test",
                LastName = "Test"
            };
            var context = new ValidationContext(person, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(person, context, results, validateAllProperties: true);

            // Assert
            Assert.That(isValid, Is.True);
        }
    }
}
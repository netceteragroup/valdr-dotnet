namespace Nca.Valdr.Tests.DTOs
{
    using Resources.Localization;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Person's data transfer object
    /// </summary>
    [DataContract(Name = "person")]
    public class PersonDto
    {
        /// <summary>
        /// First name
        /// </summary>
        [DataMember(Name = "firstName")]
        [Required(ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_RequiredField")]
        [StringLength(30, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_MaximumLength", MinimumLength = 1)]
        [Display(ResourceType = typeof(Texts), Name = "Person_FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "lastName")]
        [Required(ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_RequiredField")]
        [StringLength(31, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_MaximumLength", MinimumLength = 4)]
        [Display(ResourceType = typeof(Texts), Name = "Person_LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Age of person
        /// </summary>
        [DataMember(Name = "age")]
        [Range(0, 99)]
        public int Age { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [DataMember(Name = "email")]
        [Display(Name = "email address")]
        [EmailAddress(ErrorMessage = "Must be a valid E-Mail address.")]
        public string Email { get; set; }

        /// <summary>
        /// Home page
        /// </summary>
        [DataMember(Name = "password")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$", ErrorMessage = "Must be a valid password.")]
        public string Password { get; set; }

        [DataMember(Name = "url")]
        [Url(ErrorMessage = "Must be a valid URL.")]
        public string Url { get; set; }
    }
}

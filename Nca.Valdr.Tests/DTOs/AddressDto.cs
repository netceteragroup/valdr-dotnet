namespace Nca.Valdr.Tests.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Address data transfer object
    /// </summary>
    [DataContract(Name = "address")]
    public class AddressDto
    {
        [DataMember(Name = "city")]
        [Required(ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_RequiredField")]
        public string City { get; set; }

        [DataMember(Name = "zipCode")]
        [Required(ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_RequiredField")]
        [StringLength(6, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_MaximumLength", MinimumLength = 4)]
        [Display(ResourceType = typeof(Texts), Name = "Address_ZipCode")]
        public string ZipCode { get; set; }
    }
}

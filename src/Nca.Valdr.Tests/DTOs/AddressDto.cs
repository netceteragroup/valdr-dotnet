namespace Nca.Valdr.Tests.DTOs
{
    using Resources.Localization;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Address data transfer object.
    /// </summary>
    [DataContract(Name = "address")]
    [ValdrType(Name = "address")]
    public class AddressDto
    {
        /// <summary>
        /// Street name.
        /// </summary>
        [DataMember(Name = "street")]
        [ValdrMember(Name = "street")]
        [Display(ResourceType = typeof(Texts), Name = "Address_Street")]
        public string Street { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        [DataMember(Name = "city")]
        [ValdrMember(Name = "city")]
        [Required(ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_RequiredField")]
        [Display(ResourceType = typeof(Texts), Name = "Address_City")]
        public string City { get; set; }

        /// <summary>
        /// Zip code
        /// </summary>
        [DataMember(Name = "zipCode")]
        [ValdrMember(Name = "zipCode")]
        [Required(ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_RequiredField")]
        [StringLength(6, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "Generic_MaximumLength", MinimumLength = 4)]
        [Display(ResourceType = typeof(Texts), Name = "Address_ZipCode")]
        public string ZipCode { get; set; }
    }
}

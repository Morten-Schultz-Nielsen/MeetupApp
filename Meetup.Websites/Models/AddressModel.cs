using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
    public class AddressModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Land")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 2)]
        [RegularExpression(Address.CountryPattern, ErrorMessage = "Feltet \"{0}\" indeholder et ugyldigt navn")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Vej navn")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 2)]
        [RegularExpression(Address.StreetPattern, ErrorMessage = "Feltet \"{0}\" indeholder en ugyldig vej")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Vej nummer")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være mindst {1} bogstaver langt.")]
        [RegularExpression(Address.StreetNumberPattern, ErrorMessage = "Feltet \"{0}\" indeholder et ugyldigt vej nummer")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "By")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 2)]
        [RegularExpression(Address.CityPattern, ErrorMessage = "Feltet \"{0}\" indeholder et ugyldigt navn")]
        public string City { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Postnummer")]
        [StringLength(4, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 4)]
        [RegularExpression("[1-9]{1}[0-9]{3}", ErrorMessage = "Feltet \"{0}\" kan kun indehole tal mellem 1000 og 9999")]
        public string CityZipCode { get; set; }
    }
}
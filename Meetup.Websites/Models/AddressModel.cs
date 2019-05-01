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
        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Country")]
        [StringLength(30, ErrorMessage = "The field \"{0}\" must be {2}-{1} characters long.", MinimumLength = 2)]
        [RegularExpression(Address.CountryPattern, ErrorMessage = "The field \"{0}\" contains an invalid name")]
        public string Country { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Street name")]
        [StringLength(30, ErrorMessage = "The field \"{0}\" must be {2}-{1} characters long.", MinimumLength = 2)]
        [RegularExpression(Address.StreetPattern, ErrorMessage = "The field \"{0}\" contains an invalid name")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Street number")]
        [StringLength(30, ErrorMessage = "The field \"{0}\" must be less than {1} characters long.")]
        [RegularExpression(Address.StreetNumberPattern, ErrorMessage = "The field \"{0}\" contains an invalid street number")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "City")]
        [StringLength(30, ErrorMessage = "The field \"{0}\" must be {2}-{1} characters long.", MinimumLength = 2)]
        [RegularExpression(Address.CityPattern, ErrorMessage = "The field \"{0}\" contains an invalid name")]
        public string City { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "City zipcode")]
        [StringLength(4, ErrorMessage = "The field \"{0}\" must be {1} characters long.", MinimumLength = 4)]
        [RegularExpression("[1-9]{1}[0-9]{3}", ErrorMessage = "The field \"{0}\" can only contain numbers between 1000 to 9999")]
        public string CityZipCode { get; set; }
    }
}
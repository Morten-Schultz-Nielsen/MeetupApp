using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Helpers;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Kode")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Husk denne browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }

        [Display(Name = "Husk mig?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Fornavn")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 2)]
        [RegularExpression(User.NamePattern, ErrorMessage = "Feltet \"{0}\" indeholder et ugyldigt navn.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Efternavn")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 2)]
        [RegularExpression(User.NamePattern, ErrorMessage = "Feltet \"{0}\" indeholder et ugyldigt navn.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Beskrivelse")]
        [StringLength(int.MaxValue, ErrorMessage = "Feltet \"{0}\" skal være mindst {1} bogstaver langt.", MinimumLength = 10)]
        [RegularExpression(User.DescriptionPattern, ErrorMessage = "Feltet \"{0}\" indeholder en ugyldig beskrivelse.")]
        public string Description { get; set; }

        public AddressModel Address { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Profil billed")]
        public HttpPostedFileBase Picture { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [StringLength(100, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Gentag kode")]
        [Compare("Password", ErrorMessage = "Koden og den gentagene kode er ikke ens.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [StringLength(100, ErrorMessage = "Feltet {0} skal være {2}-{1} bogstaver langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Gentag kode")]
        [Compare("Password", ErrorMessage = "Koden og den gentagene kode er ikke ens.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

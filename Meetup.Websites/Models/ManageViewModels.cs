using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Meetup.Websites.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [StringLength(100, ErrorMessage = "Feltet {0} skal være mindst {2} bogstaver langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Ny kode")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Gentag kode")]
        [Compare("NewPassword", ErrorMessage = "Den nye kode og den gentagene kode er ikke ens.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nuværende kode")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [StringLength(100, ErrorMessage = "Feltet {0} skal være mindst {2} bogstaver langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Ny kode")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Gentag ny kode")]
        [Compare("NewPassword", ErrorMessage = "Den nye kode og den gentagene kode er ikke ens.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Telefon nummer")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Kode")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefon nummer")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}
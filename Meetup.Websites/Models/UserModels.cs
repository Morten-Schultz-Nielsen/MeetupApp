using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
    public class UserListModel
    {
        public List<User> Users { get; set; }

        [Display(Name = "Name")]
        public string SearchString { get; set; }

        public int PageNumber { get; set; }

        public int MaxPages { get; set; }

        public int FirstShownPage { get; set; }

        public int LastShownPage { get; set; }
    }

    public class OrganizationModel
    {
        [Required]
        [Display(Name = "Organization name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Hiring date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Ending date")]
        public DateTime? EndDate { get; set; }

        public string State { get; set; }

        public int Id { get; set; }
    }

    public class OrganizationInputModel
    {
        public string Name { get; set; }

    }

    public class UserEditorModel
    {
        public List<OrganizationModel> Organizations { get; set; }

        public List<Business> UnselectedBusinesses { get; set; }

        public List<Business> SelectedBusinesses { get; set; }

        public List<Interest> UnselectedInterests { get; set; }

        public List<Interest> SelectedInterests { get; set; }

        public string ChosenInterests { get; set; }

        public string ChosenBusinesses { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Description")]
        [StringLength(int.MaxValue, ErrorMessage = "The field \"{0}\" must be atleast {2} characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        [Display(Name = "Profile picture")]
        public HttpPostedFileBase Picture { get; set; }

        public AddressModel Address { get; set; }
    }

    public class UserProfileModel
    {
        public User User { get; set; }

        public bool IsYou { get; set; }
    }
}
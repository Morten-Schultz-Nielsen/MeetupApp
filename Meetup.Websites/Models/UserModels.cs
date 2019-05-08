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

        [Display(Name = "Navn")]
        public string SearchString { get; set; }

        public int PageNumber { get; set; }

        public int MaxPages { get; set; }

        public int FirstShownPage { get; set; }

        public int LastShownPage { get; set; }
    }

    public class OrganizationModel
    {
        [Display(Name = "Organisation")]
        public string Name { get; set; }

        [Display(Name = "Ansættelsesdato")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Stop dato")]
        public DateTime? EndDate { get; set; }

        public string State { get; set; }

        public int Id { get; set; }
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

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Beskrivelse")]
        [RegularExpression(User.DescriptionPattern, ErrorMessage = "Feltet \"{0}\" er en ugyldig beskrivelse.")]
        public string Description { get; set; }

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
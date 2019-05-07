using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
    public class WishesViewModel
    {
        public List<Wish> WishList { get; set; }

        public Event TheEvent { get; set; }
    }

    public class WishViewModel
    {
        public Wish TheWish { get; set; }

        public Event TheEvent { get; set; }
    }

    public class WishOrganizationModel
    {
        [Display(Name = "Organisation")]
        public string Name { get; set; }

        [Display(Name = "Arbejdsår")]
        public int? WorkYears { get; set; }
    }

    public class WishEditCreateModel
    {
        public List<Organization> Organizations { get; set; }

        public WishOrganizationModel OrganizationWish { get; set; }

        public int SelectedOrganizationIndex { get; set; }

        public bool EditingWish { get; set; }

        public Wish WishInformation { get; set; }

        public int WishId { get; set; }

        public Event EventInformation { get; set; }

        public int EventId { get; set; }

        public List<SelectListItem> UsersInEvent { get; set; }

        public List<Business> UnchosenBusinesses { get; set; }

        public List<Interest> UnchosenInterests { get; set; }

        public List<Business> ChosenBusinessesList { get; set; }

        public List<Interest> ChosenInterestsList { get; set; }

        [Display(Name = "Navn")]
        public string ChosenName { get; set; }

        public string ChosenInterests { get; set; }

        public string ChosenBusinesses { get; set; }
    }
}
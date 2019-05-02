using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
    public class UserIndexModel
    {
        public List<Event> NextEvents { get; set; }

        public List<Event> NewEvents { get; set; }

        public List<int> WishesPerEvent { get; set; }
    }
}
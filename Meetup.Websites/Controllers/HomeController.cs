using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meetup.Helper;
using Meetup.Entities;
using Meetup.Websites.Models;

namespace Meetup.Websites.Controllers
{
    public class HomeController: Controller
    {
        /// <summary>
        /// An action returning the home page
        /// </summary>
        /// <returns>Returns the home page</returns>
        public ActionResult Index()
        {
            //If user ins't logged in return normal home page
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return View();
            }
            //If user is logged in return information page
            MeetupModel model = new MeetupModel();
            UserIndexModel viewModel = new UserIndexModel();
            User user = model.Users.SingleOrDefault(u => u.Id == infoID);

            //Create list of upcoming events
            viewModel.NextEvents = new List<Event>();
            List<Invite> invites = user.Invites.ToList();
            foreach(Invite invite in invites)
            {
                if(invite.Event.BeginningTime >= DateTime.Now)
                {
                    viewModel.NextEvents.Add(invite.Event);
                }
            }
            viewModel.NextEvents.Sort(Event.Sort);
            viewModel.NextEvents = viewModel.NextEvents.Take(5).ToList();

            //Get wish count for the events
            viewModel.WishesPerEvent = new List<int>();
            for(int i = 0; i < viewModel.NextEvents.Count; i++)
            {
                viewModel.WishesPerEvent.Add(user.Wishes.Count(w => w.EventId == viewModel.NextEvents[i].Id));
            }

            //Create list of newly invited to events
            viewModel.NewEvents = new List<Event>();
            invites.Sort(Invite.Sort);
            invites = invites.Take(5).ToList();
            foreach(Invite invite in invites)
            {
                viewModel.NewEvents.Add(invite.Event);
            }
            return View("UserIndex", viewModel);
        }
    }
}
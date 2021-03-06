﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meetup.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Meetup.Websites.Models;
using System.Net;
using Newtonsoft.Json;
using Meetup.Helper;

namespace Meetup.Websites.Controllers
{
    public class UsersController : Controller
    {
        /// <summary>
        /// The amount of users each page will show.
        /// The number is best when its divisible by 4.
        /// </summary>
        const int usersPerPage = 12;

        /// <summary>
        /// The amount of (page buttons - 1) / 2 to show under the users.
        /// example: setting it to 3 = 7 buttons.
        /// </summary>
        const int pagesToShow = 3;

        /// <summary>
        /// An action returning a page showing all users
        /// </summary>
        /// <param name="viewModel">The view model containing search information</param>
        /// <returns>Returns a page with a list of all users (only returns users searched for if searched)</returns>
        public ActionResult Index(UserListModel viewModel)
        {
            MeetupModel model = new MeetupModel();
            if(viewModel is null)
            {
                viewModel = new UserListModel();
            }
            viewModel.Users = model.Users.ToList();

            //Get users
            if(viewModel.PageNumber <= 0)
            {
                viewModel.PageNumber = 1;
            }
            IEnumerable<User> users = viewModel.Users;
            if(!string.IsNullOrWhiteSpace(viewModel.SearchString))
            {
                //Search for a user's name
                users = users.Where(u => u.FullName.ToLower().Contains(viewModel.SearchString.ToLower())).ToList();
            }
            viewModel.Users = users.Skip((viewModel.PageNumber - 1) * usersPerPage).Take(usersPerPage).ToList();

            //Calculate page numbers
            viewModel.MaxPages = (int)Math.Ceiling(users.Count() / (double)usersPerPage);
            viewModel.FirstShownPage = Math.Max(1, viewModel.PageNumber - pagesToShow);
            viewModel.LastShownPage = Math.Min(viewModel.MaxPages, viewModel.PageNumber + pagesToShow) + 1;

            return View(viewModel);
        }

        /// <summary>
        /// An action returning a page showing a user's profile
        /// </summary>
        /// <param name="id">the id of the user whose profile to show</param>
        /// <returns>if user exists: a page with information about the user</returns>
        [ActionName("Profile")]
        public ActionResult UserProfile(int? id)
        {
            MeetupModel model = new MeetupModel();
            List<User> users = model.Users.ToList();
            UserProfileModel viewModel = new UserProfileModel();

            if(id is null)
            {
                return View("Index", users);
            }

            viewModel.User = users.SingleOrDefault(userInfo => userInfo.Id == id);
            if(viewModel.User is null)
            {
                return View("Index", users);
            }
            else
            {
                viewModel.IsYou = viewModel.User.Id == this.UserId();

                return View(viewModel);
            }
        }

        /// <summary>
        /// An action returning a page to a user can edit their profile
        /// </summary>
        /// <returns>If logged in: Returns a page where a user can edit their profile</returns>
        public ActionResult Edit()
        {
            MeetupModel model = new MeetupModel();
            List<User> users = model.Users.ToList();

            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }
            User editUser = users.SingleOrDefault(userInfo => userInfo.Id == infoID);

            UserEditorModel editModel = new UserEditorModel();

            //Get business list
            List<Business> selectedBusinesses = editUser.GetBusinesses().ToList();
            List<Business> unselectedBusinesses = model.Businesses.ToList().Except(selectedBusinesses).ToList();

            //Get interest list
            List<Interest> selectedInterests = editUser.GetInterests().ToList();
            List<Interest> unselectedInterests = model.Interests.ToList().Except(selectedInterests).ToList();

            editModel.UnselectedBusinesses = unselectedBusinesses;
            editModel.SelectedBusinesses = selectedBusinesses;
            editModel.UnselectedInterests = unselectedInterests;
            editModel.SelectedInterests = selectedInterests;
            editModel.Description = editUser.Description;
            editModel.FirstName = editUser.FirstName;
            editModel.LastName = editUser.LastName;

            editModel.Address = new AddressModel();
            editModel.Address.City = editUser.Address.CityName;
            editModel.Address.Country = editUser.Address.Country;
            editModel.Address.CityZipCode = editUser.Address.ZipCode.ToString();
            editModel.Address.StreetName = editUser.Address.StreetName;
            editModel.Address.StreetNumber = editUser.Address.StreetNumber;

            editModel.Organizations = new List<OrganizationModel>();
            foreach(UsersOrganizations organization in editUser.UsersOrganizations)
            {
                editModel.Organizations.Add(new OrganizationModel()
                {
                    Id = organization.Id,
                    Name = organization.Organization.Name,
                    EndDate = organization.EndDate,
                    StartDate = organization.StartDate,
                    State = "old"
                });
            }

            return View(editModel);
        }

        /// <summary>
        /// An action getting the post request when a user edits their profile
        /// </summary>
        /// <param name="viewModel">The posted information the user has edited</param>
        /// <returns>If success: returns the user's page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditorModel viewModel)
        {
            //Get user
            MeetupModel model = new MeetupModel();
            int? userId = this.UserId();
            if(userId == null)
            {
                return RedirectToAction("Index", "HomeController");
            }
            User editUser = model.Users.SingleOrDefault(userInfo => userInfo.Id == userId);

            //Get businesses list
            if(string.IsNullOrEmpty(viewModel.ChosenBusinesses))
            {
                viewModel.ChosenBusinesses = "";
            }
            string[] selectedBusnisses = viewModel.ChosenBusinesses.Split(',');
            model.UsersBusinesses.RemoveRange(editUser.UsersBusinesses.Where(b => !selectedBusnisses.Contains(b.Business.Name)));
            List<Business> businesses = model.Businesses.Where(b => selectedBusnisses.Contains(b.Name)).ToList();
            businesses = businesses.Where(b => !editUser.UsersBusinesses.Any(ub => ub.BusinessId == b.Id)).ToList();
            foreach(Business business in businesses)
            {
                editUser.UsersBusinesses.Add(new UsersBusiness(business,editUser));
            }

            //Get interests list
            if(string.IsNullOrEmpty(viewModel.ChosenInterests))
            {
                viewModel.ChosenInterests = "";
            }
            string[] selectedInterests = viewModel.ChosenInterests.Split(',');
            model.UsersInterests.RemoveRange(editUser.UsersInterests.Where(i => !selectedInterests.Contains(i.Interest.Name)));
            List<Interest> interests = model.Interests.Where(i => selectedInterests.Contains(i.Name)).ToList();
            interests = interests.Where(i => !editUser.UsersInterests.Any(ui => ui.InterestId == i.Id)).ToList();
            foreach(Interest interest in interests)
            {
                editUser.UsersInterests.Add(new UsersInterest(interest, editUser));
            }

            //Make sure model is valid
            if(!ModelState.IsValid)
            {
                return ReturnEdit(viewModel, editUser, model);
            }
            else
            {
                if(!(viewModel.Organizations is null))
                {
                    //Create organizations list
                    for(int i = 0; i < viewModel.Organizations.Count; i++)
                    {
                        OrganizationModel organization = viewModel.Organizations[i];
                        if(organization.State == "removed")
                        {
                            //Remove removed organizations
                            UsersOrganizations removeOrganization = editUser.UsersOrganizations.SingleOrDefault(o => o.Id == organization.Id);
                            if(!(removeOrganization is null))
                            {
                                model.UsersOrganizations.Remove(removeOrganization);
                            }
                        }
                        else if (organization.State == "old" || organization.State == "new")
                        {
                            //Make sure organization is in a valid state
                            if(string.IsNullOrWhiteSpace(organization.Name))
                            {
                                ModelState.AddModelError("Organizations[" + i + "].Name", "Feltet Organisation navn skal udfyldes.");
                                return ReturnEdit(viewModel, editUser, model);
                            }
                            if(organization.StartDate is null)
                            {
                                ModelState.AddModelError("Organizations[" + i + "].StartDate", "Feltet ansættelsesdato skal udfyldes.");
                                return ReturnEdit(viewModel, editUser, model);
                            }
                            if(organization.StartDate > DateTime.Now)
                            {
                                ModelState.AddModelError("Organizations[" + i + "].StartDate", "Ansættelsesdato må ikke være i fremtiden.");
                                return ReturnEdit(viewModel, editUser, model);
                            }
                            else if(!(organization.EndDate is null))
                            {
                                if(organization.StartDate.Value >= organization.EndDate)
                                {
                                    ModelState.AddModelError("Organizations[" + i + "].StartDate", "Ansættelsesdato må ikke være efter slut datoen.");
                                    return ReturnEdit(viewModel, editUser, model);
                                }
                                else if(organization.EndDate > DateTime.Now)
                                {
                                    ModelState.AddModelError("Organizations[" + i + "].EndDate", "Slut datoen kan ikke være i fremtiden.");
                                    return ReturnEdit(viewModel, editUser, model);
                                }
                            }

                            //Get the organization the user is editing or create a new one latter
                            UsersOrganizations editOrganization = null;
                            Organization newOrganization = null;
                            Organization oldOrganization = null;
                            if(organization.State == "old")
                            {
                                editOrganization = editUser.UsersOrganizations.SingleOrDefault(o => o.Id == organization.Id);
                                if(editOrganization is null)
                                {
                                    continue;
                                }
                                oldOrganization = editOrganization.Organization;
                            }
                            else if(organization.State != "new")
                            {
                                continue;
                            }

                            //Check new organization name
                            if(organization.State == "new" || organization.Name != oldOrganization.Name)
                            {
                                newOrganization = model.Organizations.SingleOrDefault(o => o.Name == organization.Name);
                                if(newOrganization is null)
                                {
                                    //Call api to check organization name
                                    try
                                    {
                                        if(Organization.NameExists(organization.Name))
                                        {
                                            newOrganization = new Organization(organization.Name);
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("Organizations[" + i + "].Name", "Organisationen \"" + organization.Name + "\" eksisterer ikke.");
                                            return ReturnEdit(viewModel, editUser, model);
                                        }
                                    }
                                    catch(WebException)
                                    {
                                        ModelState.AddModelError("Organizations[" + i + "].Name", "Fejlede i at bedømme organisation navnet. Prøv igen senere");
                                        return ReturnEdit(viewModel, editUser, model);
                                    }
                                }
                            }
                            //Create new organization object or fill old object
                            if(editOrganization is null)
                            {
                                editOrganization = new UsersOrganizations(newOrganization, editUser, organization.StartDate.Value);
                                model.UsersOrganizations.Add(editOrganization);
                            }
                            else
                            {
                                if(!(newOrganization is null))
                                {
                                    editOrganization.Organization = newOrganization;
                                }
                                editOrganization.StartDate = organization.StartDate.Value;
                            }
                            editOrganization.EndDate = organization.EndDate;
                        }
                    }
                }

                if(!(viewModel.Picture is null))
                {
                    //Update profile picture
                    if(!viewModel.Picture.ContentType.Contains("image"))
                    {
                        ModelState.AddModelError("Picture", "Feltet \"Profil Billed\" accepterer kun billeder.");
                        return ReturnEdit(viewModel, editUser, model);
                    }
                    editUser.PictureUri = "data:image/png;base64," + viewModel.Picture.PictureFileToString();
                }

                //Update everything else
                editUser.Description = viewModel.Description;
                editUser.Address.CityName = viewModel.Address.City;
                editUser.Address.Country = viewModel.Address.Country;
                editUser.Address.StreetName = viewModel.Address.StreetName;
                editUser.Address.StreetNumber = viewModel.Address.StreetNumber;
                editUser.Address.ZipCode = Convert.ToInt32(viewModel.Address.CityZipCode);
                editUser.FirstName = viewModel.FirstName;
                editUser.LastName = viewModel.LastName;

                model.SaveChanges();
                return RedirectToAction("Profile", "Users", new {editUser.Id});
            }
        }

        /// <summary>
        /// Returns the edit page. Used for when something is wrong in the view model
        /// </summary>
        /// <param name="viewModel">The model with the information to show on the editing page</param>
        /// <param name="editUser">the user to edit</param>
        /// <param name="model">the database access</param>
        /// <returns>The profile edit page</returns>
        private ActionResult ReturnEdit(UserEditorModel viewModel, User editUser, MeetupModel model)
        {
            //Get businesses lists
            viewModel.SelectedBusinesses = new List<Business>();
            List<Business> allBusinessesList = model.Businesses.ToList();
            List<UsersBusiness> businessesList = editUser.UsersBusinesses.ToList();
            foreach(UsersBusiness usersBusiness in businessesList)
            {
                viewModel.SelectedBusinesses.Add(usersBusiness.Business);
                allBusinessesList.Remove(usersBusiness.Business);
            }
            viewModel.UnselectedBusinesses = allBusinessesList;

            //Get interests lists
            viewModel.SelectedInterests = new List<Interest>();
            List<Interest> allInterestsList = model.Interests.ToList();
            List<UsersInterest> interestsList = editUser.UsersInterests.ToList();
            foreach(UsersInterest usersInterest in interestsList)
            {
                viewModel.SelectedInterests.Add(usersInterest.Interest);
                allInterestsList.Remove(usersInterest.Interest);
            }
            viewModel.UnselectedInterests = allInterestsList;

            if(viewModel.Organizations is null)
            {
                viewModel.Organizations = new List<OrganizationModel>();
            }

            //Remove removed organizations from the view model
            viewModel.Organizations = viewModel.Organizations.Where(o => o.State != "removed" && o.State != "new-removed").ToList();

            return View("Edit", viewModel);
        }
    }
}
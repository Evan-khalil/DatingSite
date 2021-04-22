using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    public class HomeController : Controller
    {
        private UserOperations userOperations;
        private Random rn;



        public HomeController()
        {
            DataContext db = new DataContext();
            userOperations = new UserOperations(db);
            rn = new Random();
        }
        public ActionResult Index1(int id)
        {
            return PartialView(id);
        }

        public ActionResult Index()
        {
            var users = new List<ApplicationUser>();
            if (User.Identity.IsAuthenticated)
            {
                users = userOperations.GetAllUsers(User.Identity.GetUserId());
            }
            else
            {

                users = userOperations.GetAll();
            }
            var randomUsers = new List<ApplicationUser>();
            for (int i = 0; i < users.Count; i++)
            {
                var user = users[rn.Next(users.Count)];

                if (!randomUsers.Exists(x => x == user))
                {
                    randomUsers.Add(user);
                }
                else
                {
                    i--;
                }
            }
            return View(randomUsers);
        }
    }
}
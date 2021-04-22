using DatingSite.Models;
using System.Linq;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    public class SearchController : Controller
    {
        private UserOperations userOperations;
        public SearchController()
        {
            DataContext context = new DataContext();
            userOperations = new UserOperations(context);
        }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        //Get result from search
        [HttpGet]
        public PartialViewResult Result(string id)
        {
            var users = userOperations.SearchedUsers(id);
            var model = users.Select(user => new SearchModel()
            {
                UserId = user.Id,
                Name = user.Firstname + " " + user.Lastname,
                Gender = user.Gender,
                Image = user.ProfileImage
            });
            return PartialView("SearchResults", model);
        }
    }
}
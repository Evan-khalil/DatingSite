using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace DatingSite.Controllers
{
    public class AjaxApiController : ApiController
    {
        PostOperations postOperations;
        UserOperations userOperations;
        RequestOperations requestOperations;
        public AjaxApiController()
        {
            DataContext context = new DataContext();
            postOperations = new PostOperations(context);
            userOperations = new UserOperations(context);
            requestOperations = new RequestOperations(context);
        }

        [HttpPost]
        public void AddPost(Posts post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reciever = userOperations.Get(post.RecieverId);
                    var _post = new Posts()
                    {
                        Description = post.Description,
                        Sender = userOperations.Get(User.Identity.GetUserId()),
                        Reciever = reciever,
                        Date = DateTime.Now
                    };
                    postOperations.Add(_post);
                    postOperations.Save();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
            }
        }

        [HttpDelete]
        public void DeletePost(int id)
        {
            try
            {
                var post = postOperations.Get(id);
                postOperations.Remove(id);
                postOperations.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

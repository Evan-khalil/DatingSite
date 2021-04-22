using DatingSite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private UserOperations userOperations;
        private PostOperations postOperations;
        private FriendsOperations friendOperations;
        private RequestOperations requestOperations;

        public ProfileController()
        {
            DataContext context = new DataContext();
            userOperations = new UserOperations(context);
            postOperations = new PostOperations(context);
            friendOperations = new FriendsOperations(context);
            requestOperations = new RequestOperations(context);
        }

        // GET: ProfilePage
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = userOperations.Get(userId);
            var posts = postOperations.GetPosts(userId);
            var friends = friendOperations.GetFriends(userId);
            var myFriends = new List<FriendModel>();
            foreach (var item in friends)
            {
                myFriends.Add(new FriendModel()
                {
                    user = item.TheUser,
                    otherUser = item.TheFriend
                });
            }
            var model = new ProfileModel
            {
                CurrentUser = user,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                ProfilpageImage = user.ProfileImage,
                Description = user.Description,
                Gender = user.Gender,
                Email = user.Email,
                Friends = myFriends
            };
            var postModel = ConvertPostToPostViewModelIdentity(posts, user.Id);
            model.Posts = postModel;
            return View(model);
        }

        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var user = userOperations.Get(userId);
            var model = new EditPorfileModel
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Description = user.Description,
                Gender = user.Gender,
                ProfilpageImage = user.ProfileImage
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditPorfileModel model, HttpPostedFileBase imageFile)
        {
            var userId = User.Identity.GetUserId();
            var user = userOperations.Get(userId);

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (imageFile != null)
            {
                byte[] imageByte;
                var fileName = Path.GetFileName(imageFile.FileName);
                var fileExtension = Path.GetExtension(fileName);
                int fileSize = imageFile.ContentLength;
                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png"
                    || fileExtension.ToLower() == ".jpeg")
                {
                    Stream stream = imageFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    imageByte = binaryReader.ReadBytes((int)stream.Length);
                    user.ProfileImage = imageByte;
                }
            }
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Description = model.Description;
            user.Email = model.Email;
            userOperations.Save();
            return RedirectToAction("Index");
        }

        //Returns a PartialView with the inlogged users friends
        public PartialViewResult MyFriends()
        {
            var friends = friendOperations.GetFriends(User.Identity.GetUserId());
            var favorite = friends.Where(friend => friend.Category == FriendCategory.favorite)
                .Select(item => new FriendModel2()
                {
                    Name = item.TheFriend.Firstname + " " + item.TheFriend.Lastname,
                    FriendId = item.Id,
                    UserId = item.FriendId,
                    Image = item.TheFriend.ProfileImage,
                });
            var standard = friends.Where(friend => friend.Category == FriendCategory.Normal)
                .Select(item => new FriendModel2()
                {
                    Name = item.TheFriend.Firstname + " " + item.TheFriend.Lastname,
                    FriendId = item.Id,
                    UserId = item.FriendId,
                    Image = item.TheFriend.ProfileImage,
                });
            var model = new CategoryModel()
            {
                FavoriteFriends = favorite.ToList(),
                StandardFriends = standard.ToList()
            };
            return PartialView("Friends", model);
        }

        //Returns a PartialView with the inlogged users requests
        public PartialViewResult MyFriendRequests()
        {
            var requests = requestOperations.GetUsers(User.Identity.GetUserId());
            if (requests.Count > 0)
            {
                var model = requests.Select(request => new RequestModel()
                {
                    requestId = request.Id,
                    UserId = request.RequestedBy.Id,
                    Name = request.RequestedBy.Firstname + " " + request.RequestedBy.Lastname,
                    Date = request.RequestDate,
                    TimeAgo = TimeConverter.TimeAgo(request.RequestDate),
                    Image = request.RequestedBy.ProfileImage
                });
                return PartialView("Requests", model);
            }
            return PartialView("NoRequests");
        }

        //Returns a PartialView with the inlogged users posts
        public PartialViewResult WallPage(string id)
        {
            var posts = postOperations.GetPosts(id);
            var model = ConvertPostToPostViewModelIdentity(posts, id);
            return PartialView("Posts", model);
        }

        [HttpPost]
        public void AcceptRequest(int id)
        {
            try
            {
                var req = requestOperations.Get(id);
                var friend1 = new Friends()
                {
                    TheUser = req.RequestedTo,
                    TheFriend = req.RequestedBy,
                    Category = FriendCategory.Normal,
                };
                var friend2 = new Friends()
                {
                    TheUser = req.RequestedBy,
                    TheFriend = req.RequestedTo,
                    Category = FriendCategory.Normal,
                };
                friendOperations.Add(friend1);
                friendOperations.Add(friend2);
                requestOperations.Remove(req.Id);
                friendOperations.Save();
                requestOperations.Save();
            }
            catch (Exception ex)
            {
            }
        }

        [HttpPost]
        public void SendRequest(string id)
        {
            if (!friendOperations.Friend(id, User.Identity.GetUserId()))
            {
                var user = userOperations.Get(id);
                var identity = userOperations.Get(User.Identity.GetUserId());
                var request = new Requests()
                {
                    RequestDate = DateTime.Now,
                    RequestedTo = user,
                    RequestedBy = identity,
                };
                if (request.RequestedBy != null && request.RequestedTo != null)
                {
                    requestOperations.Add(request);
                    requestOperations.Save();

                }
            }

        }

        [HttpDelete]
        public void DeclineRequest(int id)
        {
            try
            {
                var req = requestOperations.Get(id);
                if (req != null)
                {
                    requestOperations.Remove(req.Id);
                    requestOperations.Save();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Mark and unmark friends with favorite/standard tag
        [HttpPut]
        public void FavoriteTag(int id)
        {
            var friend = friendOperations.Get(id);
            if (friend.Category == FriendCategory.Normal)
            {
                friend.Category = FriendCategory.favorite;
            }
            else
            {
                friend.Category = FriendCategory.Normal;
            }
            friendOperations.Save();
        }

        //Other profiles
        public ActionResult UserProfile(string id)
        {
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }
            var user = userOperations.Get(id);
            var posts = postOperations.GetPosts(id);
            var identity = userOperations.Get(User.Identity.GetUserId());
            var model = new UserProfileModel
            {
                UserId = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Description = user.Description,
                Image = user.ProfileImage,
                IsAlreadyFriend = friendOperations.Friend(id, User.Identity.GetUserId())
            };
            if (model.IsAlreadyFriend)
            {
                var friend = friendOperations.GetAll().First(x => x.FriendId == user.Id && x.UserId == User.Identity.GetUserId());
                model.RelationStatusId = friend.Id;
                model.RelationStatusToIdentity = FriendshipIdentity.IsFriend;
            }
            else
            {
                model.HasSendRequest = requestOperations.AlreadySent(id, User.Identity.GetUserId());
                if (model.HasSendRequest)
                {
                    var request = requestOperations.GetAll().First(x => x.RequestedBy_Id == User.Identity.GetUserId() && x.RequestedTo_Id == model.UserId);
                    model.RelationStatusId = request.Id;
                    model.RelationStatusToIdentity = FriendshipIdentity.HasSendRequest;
                }
                else
                {
                    model.HasRecievedRequest = requestOperations.Recieved(id, User.Identity.GetUserId());
                    if (model.HasRecievedRequest)
                    {
                        var request = requestOperations.GetAll().First(x => x.RequestedTo_Id == User.Identity.GetUserId() && x.RequestedBy_Id == model.UserId);
                        model.RelationStatusId = request.Id;
                        model.RelationStatusToIdentity = FriendshipIdentity.HasRecievedRequest;
                    }
                    else
                    {
                        model.RelationStatusToIdentity = FriendshipIdentity.IsNotFriend;
                    }
                }
            }
            model.Posts = ConvertPostToPostViewModelIdentity(posts, id);
            model.Posts.IsFriendOrNot = model.IsAlreadyFriend;
            return View(model);
        }


        public PartialViewResult UserProfileRelationStatusToIdentity(string id)
        {
            var model = new FriendshipStatus();
            var user = userOperations.Get(id);
            model.UserId = user.Id;
            var IsAlreadyFriend = friendOperations.Friend(id, User.Identity.GetUserId());
            if (IsAlreadyFriend)
            {
                var friend = friendOperations.GetAll().First(x => x.FriendId == user.Id && x.UserId == User.Identity.GetUserId());
                model.RelationStatusId = friend.Id;
                model.RelationStatusToIdentity = FriendshipIdentity.IsFriend;
            }
            else
            {
                var HasSendRequest = requestOperations.AlreadySent(id, User.Identity.GetUserId());
                if (HasSendRequest)
                {
                    var request = requestOperations.GetAll().First(x => x.RequestedBy_Id == User.Identity.GetUserId() && x.RequestedTo_Id == model.UserId);
                    model.RelationStatusId = request.Id;
                    model.RelationStatusToIdentity = FriendshipIdentity.HasSendRequest;
                }
                else
                {
                    var HasRecievedRequest = requestOperations.Recieved(id, User.Identity.GetUserId());
                    if (HasRecievedRequest)
                    {
                        var request = requestOperations.GetAll().First(x => x.RequestedTo_Id == User.Identity.GetUserId() && x.RequestedBy_Id == model.UserId);
                        model.RelationStatusId = request.Id;
                        model.RelationStatusToIdentity = FriendshipIdentity.HasRecievedRequest;
                    }
                    else
                    {
                        model.RelationStatusToIdentity = FriendshipIdentity.IsNotFriend;
                    }
                }
            }
            return PartialView("UserRelationStatusToIdentity", model);
        }

        //Creates an object that contains a List<Post> and properties about the relationship between the inlogged user and the userprofile
        public PostsModelId ConvertPostToPostViewModelIdentity(List<Posts> posts, string id)
        {
            var postsModel = posts.Select(post => new PostModel()
            {
                Id = post.Id,
                Text = post.Description,
                TimeAgo = TimeConverter.TimeAgo(post.Date),
                Sender = post.Sender
            });
            var model = new PostsModelId
            {
                Posts = postsModel.ToList()
            };
            if (User.Identity.GetUserId() == id) model.IsIdentity = true;
            else model.IsIdentity = false;
            model.identityId = User.Identity.GetUserId();
            return model;
        }



        [HttpGet]
        public JsonResult IfUserHasAnyRequests()
        {
            var requests = requestOperations.GetUsers(User.Identity.GetUserId());
            var success = true;
            if (requests.Any()) success = true;
            else success = false;

            return Json(success, JsonRequestBehavior.AllowGet);
        }

    }
}
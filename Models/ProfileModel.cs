
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatingSite.Models
{
    public class ProfileModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }
        public byte[] ProfilpageImage { get; set; }
        public PostsModelId Posts { get; set; }
        public List<FriendModel> Friends { get; set; }
    }

    public class FriendModel
    {
        public ApplicationUser user { get; set; }
        public ApplicationUser otherUser { get; set; }
    }

    public class EditPorfileModel
    {
        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage = "Your description can have max 300 characters")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public byte[] ProfilpageImage { get; set; }
    }

    public class FriendModel2
    {
        public int FriendId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public FriendCategory Category { get; set; }
    }


    public class CategoryModel
    {
        public List<FriendModel2> FavoriteFriends { get; set; }
        public List<FriendModel2> StandardFriends { get; set; }
    }


    public class RequestModel
    {
        public int requestId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TimeAgo { get; set; }
        public byte[] Image { get; set; }
    }

    public class PostModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string TimeAgo { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }

    public class PostsModelId
    {
        public string identityId { get; set; }
        public bool IsIdentity { get; set; } = false;
        public bool IsFriendOrNot { get; set; } = true;
        public List<PostModel> Posts { get; set; }
    }
}
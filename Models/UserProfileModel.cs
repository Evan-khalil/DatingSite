using System.Collections.Generic;

namespace DatingSite.Models
{
    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public PostsModelId Posts { get; set; }
        public bool IsAlreadyFriend { get; set; } = false;
        public bool HasSendRequest { get; set; } = false;
        public bool HasRecievedRequest { get; set; } = false;
        public FriendshipIdentity RelationStatusToIdentity { get; set; }
        public int RelationStatusId { get; set; }
    }

    public enum FriendshipIdentity
    {
        IsNotFriend,
        IsFriend,
        HasSendRequest,
        HasRecievedRequest
    }

    public class SharePostModel
    {
        public string UserId { get; set; }
        public List<Posts> Posts { get; set; }
    }

    public class FriendshipStatus
    {
        public string UserId { get; set; }
        public FriendshipIdentity RelationStatusToIdentity { get; set; }
        public int RelationStatusId { get; set; }
    }
}
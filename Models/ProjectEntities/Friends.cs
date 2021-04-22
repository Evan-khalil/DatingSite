
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingSite
{
    public class Friends : MainEntity<int>
    {
        public int Id { get; set; }

        [ForeignKey("TheUser")]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser TheUser { get; set; }
        [ForeignKey("TheFriend")]
        public virtual string FriendId { get; set; }
        public virtual ApplicationUser TheFriend { get; set; }

        public FriendCategory Category { get; set; }
    }

    public enum FriendCategory
    {
        Normal,
        favorite
    }
}

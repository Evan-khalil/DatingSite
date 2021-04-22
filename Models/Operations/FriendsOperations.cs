using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatingSite
{
    public class FriendsOperations : Operations<Friends, int>
    {
        public FriendsOperations(DataContext context) : base(context)
        {
        }

        public List<Friends> GetFriends(string id)
        {
            return Items.Include(x => x.TheFriend)
                .Where(x => x.UserId == id).ToList();
        }

        public bool Friend(string userId, string id)
        {
            var query = Items.Where(x => x.UserId == id && x.FriendId == userId).ToList();
            if (query.Count > 0) return true;
            else return false;
        }

    }
}

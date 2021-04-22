using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatingSite
{
    public class PostOperations : Operations<Posts, int>
    {
        public PostOperations(DataContext db) : base(db)
        {
        }

        public List<Posts> GetPosts(string id)
        {
            return Items.Include(p => p.Sender).ToList().Where(post => post.RecieverId == id).OrderByDescending(post => post.Date).ToList();
        }

        public List<Posts> GetSharedPosts(string id)
        {
            return Items.Include(p => p.Reciever).ToList().Where(post => post.SenderId == id).OrderByDescending(post => post.Date).ToList();
        }
    }
}

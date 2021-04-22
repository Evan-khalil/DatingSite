
using System.Collections.Generic;
using System.Linq;

namespace DatingSite
{
    public class UserOperations : Operations<ApplicationUser, string>
    {
        public UserOperations(DataContext context) : base(context)
        {
        }

        public List<ApplicationUser> GetAllUsers(string id)
        {
            return Items.Where(user => user.Id != id).ToList();
        }

        //public List<ApplicationUser> GetAllWithOppositeGenderWithoutIdentity(Gender gender, string id){
        //    return Items.Where(user => user.Gender != gender && user.Id != id).ToList();
        //}

        public List<ApplicationUser> SearchedUsers(string text)
        {
            return Items.Where(user => user.Firstname.Contains(text)
                                || user.Lastname.Contains(text)).ToList();
        }

        public bool IfUserEmailExist(string email)
        {
            var items = Items.Where(user => user.Email == email);
            return items.Any();
        }

    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DatingSite
{
    public class ApplicationUser : IdentityUser, MainEntity<string>
    {
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Your firstname must be atleast 2 characters")]
        public string Firstname { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Your lastname must be atleast 2 characters")]
        public string Lastname { get; set; }
        [StringLength(300, ErrorMessage = "Your description can have max 300 characters")]
        public string Description { get; set; }
        public string BirthDate { get; set; }
        public byte[] ProfileImage { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Friends> Friends { get; set; }
        public virtual ICollection<Requests> Requests { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("Firstname", this.Firstname.ToString()));
            userIdentity.AddClaim(new Claim("Lastname", this.Lastname.ToString()));
            userIdentity.AddClaim(new Claim("Gender", this.Gender.ToString()));
            return userIdentity;
        }
    }

    public static class Extention
    {
        public static string GetFirstname(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Firstname");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetLastname(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Lastname");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetGender(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Gender");
            return (claim != null) ? claim.Value : string.Empty;
        }

    }


    public enum Gender
    {
        Man,
        Woman
    }

}
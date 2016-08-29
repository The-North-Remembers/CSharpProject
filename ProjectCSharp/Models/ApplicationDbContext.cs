using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectCSharp.Controllers;

namespace ProjectCSharp.Models
{
   
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ProjectCSharp.Models.Post> Posts { get; set; }
       // public System.Data.Entity.DbSet<ProjectCSharp.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
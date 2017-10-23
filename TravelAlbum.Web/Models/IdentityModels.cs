using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TravelAlbum.Web.Models
{

    // public class TravelAlbumEfDbContext : IdentityDbContext<ApplicationUser>
    // {
    //     public TravelAlbumEfDbContext()
    //         : base("DefaultConnection", throwIfV1Schema: false)
    //     {
    //     }
    // 
    //     public static TravelAlbumEfDbContext Create()
    //     {
    //         return new TravelAlbumEfDbContext();
    //     }
    // }
}
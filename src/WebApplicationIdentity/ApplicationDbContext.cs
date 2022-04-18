using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationIdentity.Entities;

namespace WebApplicationIdentity
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,long>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

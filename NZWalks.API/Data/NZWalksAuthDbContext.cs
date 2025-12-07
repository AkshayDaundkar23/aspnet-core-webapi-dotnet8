using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext 
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "c1f688bc-2c73-4d4f-9649-0d6df1f4bb34";
            var writerRoleId = "00d0667d-7e4f-4610-b78b-8ce03ffbda25";

            var roles = new List<IdentityRole>
            {
                new IdentityRole{ Id = readerRoleId, ConcurrencyStamp = readerRoleId, Name = "Reader", NormalizedName = "Reader".ToUpper()},
                new IdentityRole{ Id = writerRoleId, ConcurrencyStamp = writerRoleId, Name = "Writer", NormalizedName = "Writer".ToUpper()},
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

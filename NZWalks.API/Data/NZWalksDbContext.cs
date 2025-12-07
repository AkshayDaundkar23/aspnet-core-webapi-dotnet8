using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for dificulties
            // Easy, Medium, Hard

            var dificulties = new List<Difficulty>()
            {
                new Difficulty() { Id = Guid.Parse("56767633-3468-43af-92b8-7e04ffb2998f"), Name = "Easy" },
                new Difficulty() { Id = Guid.Parse("da76105e-3023-4d57-b647-19e121be308f"), Name = "Medium" },
                new Difficulty() { Id = Guid.Parse("093c07df-965f-4fb6-9100-8267090daa6b"), Name = "Hard" }
            };

            modelBuilder.Entity<Difficulty>().HasData(dificulties);     // seed dificulties to database

            // seed data for regions

            var regions = new List<Region>()
            {
                new Region()
                { 
                    Id = Guid.Parse("44fff2a4-40e2-4570-836e-25064d25e9d2"), 
                    Name = "Auckland", 
                    Code = "AKL", 
                    RegionImageUrl= "https://via.placeholder.com/150"
                },
                new Region()
                {
                    Id = Guid.Parse("88930b7e-fb8c-440f-950f-073ec90d8f97"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl= "https://via.placeholder.com/300"
                },
                new Region()
                {
                    Id = Guid.Parse("cd03fda5-385b-4426-afd3-9633cd4bdf7b"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageUrl= "https://via.placeholder.com/600x400"
                },
                new Region()
                {
                    Id = Guid.Parse("4aa50c3e-3f2c-4776-bd1b-ae6578e0ef56"),
                    Name = "Canterbury",
                    Code = "CAN",
                    RegionImageUrl= "https://via.placeholder.com/800x600"
                },
                new Region()
                {
                    Id = Guid.Parse("9265e976-76b4-4a4e-9abe-ab8b9f1ac9e6"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageUrl= "https://picsum.photos/300/200"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }        
    }
}

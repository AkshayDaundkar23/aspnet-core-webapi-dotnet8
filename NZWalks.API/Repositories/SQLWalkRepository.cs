using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllWalkAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            // Build the base query from Walks table
            // Include Difficulty and Region to load related data (Eager Loading)
            // AsQueryable() allows us to add conditions dynamically

            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Apply Filtering (if parameters are provided)

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))        // If filtering is requested on the "Name" column
                {
                    // Apply partial string search using Contains()
                    // This will translate to SQL LIKE '%value%'

                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Execute the query and return result
            // ToListAsync() executes the SQL query asynchronously

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var existingWalkDomain = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalkDomain == null)
            {
                return null;
            }

            _dbContext.Walks.Remove(existingWalkDomain);
            await _dbContext.SaveChangesAsync();
            return existingWalkDomain;
        }
    }
}

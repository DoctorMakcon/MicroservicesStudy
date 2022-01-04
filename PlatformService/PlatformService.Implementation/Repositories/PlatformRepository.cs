using Microsoft.EntityFrameworkCore;
using PlatformService.Database.Contexts;
using PlatformService.Database.Models;
using PlatformService.Implementation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Implementation.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlaform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            var platforms = await _context.Platforms.ToListAsync();

            return platforms;
        }

        public Task<Platform> GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}

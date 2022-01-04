using PlatformService.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Implementation.Interfaces
{
    public interface IPlatformRepository
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<Platform>> GetAllPlatforms();

        Task<Platform> GetPlatformById(int id);

        void CreatePlaform(Platform platform);
    }
}

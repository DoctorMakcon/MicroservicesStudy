using PlatformService.Implementation.Dtos;
using System.Threading.Tasks;

namespace PlatformService.Implementation.Interfaces
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommandService(PlatformReadDto platform);
    }
}

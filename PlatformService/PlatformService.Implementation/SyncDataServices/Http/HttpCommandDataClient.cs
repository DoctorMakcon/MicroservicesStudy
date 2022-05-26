using Microsoft.Extensions.Configuration;
using PlatformService.Implementation.Dtos;
using PlatformService.Implementation.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.Implementation.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommandService(PlatformReadDto platform)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(platform), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandServiceUrl"]}/api/c/platforms/", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("---> Sync POST to CommandService was OK");
            }
            else
            {
                Console.WriteLine("---> Sync POST to CommandService was NOT OK");
            }
        }
    }
}

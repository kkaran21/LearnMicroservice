using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration= configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpcontent = new StringContent(
                JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8,
                "application/json"
            );
            
            var response = await _httpClient.PostAsync(_configuration.GetValue<string>("CommandServiceEndpoint"),httpcontent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("successfully posted to command service");
            }
            else
            {

                Console.WriteLine("post to command service failed");
            }
        }
    }
}
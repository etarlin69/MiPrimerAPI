using System.Text.Json;
using MiPrimerAPI.Models;
using Microsoft.Extensions.Configuration;
using Azure.Core;

namespace MiPrimerAPI.Services
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ExternalApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["ReqRes:ApiKey"] ?? throw new Exception("API key not found in configuration.");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://reqres.in/api/users?page=1");
                request.Headers.Add("x-api-key", "reqres - free - v1");

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"⚠️ Error al consumir API externa: {response.StatusCode}");
                    return new List<User>();
                }

                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine("🟡 JSON recibido:");
                Console.WriteLine(content);

                var users = JsonSerializer.Deserialize<UserResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return users?.Data ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔴 EXCEPCIÓN:");
                Console.WriteLine(ex.Message);
                return new List<User>();
            }
        }
    }
}

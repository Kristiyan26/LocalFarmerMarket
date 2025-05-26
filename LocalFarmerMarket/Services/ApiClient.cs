
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using LocalFarmerMarket.Core.Models.ResponseDTOs;

namespace LocalFarmerMarket.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5142/"); 
        }

        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ProductListResponse> GetAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(responseContent);

            // Extract product list from nested `$values`
            var productsElement = document.RootElement.GetProperty("products").GetProperty("$values");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // ✅ Allows matching lowercase JSON properties to C# PascalCase
            };

            var productsList = JsonSerializer.Deserialize<List<Product>>(productsElement.ToString(),options);

            return new ProductListResponse
            {
                TotalProducts = document.RootElement.GetProperty("totalProducts").GetInt32(),
                CurrentPage = document.RootElement.GetProperty("currentPage").GetInt32(),
                ItemsPerPage = document.RootElement.GetProperty("itemsPerPage").GetInt32(),
                Products = productsList // ✅ Corrected extraction
            };
        }

        public async Task<TResponse> PostAsync<TResponse>(string endpoint, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
 
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                // throw или върни грешка по избор
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PutAsync<TResponse>(string endpoint, object data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }
    }
}

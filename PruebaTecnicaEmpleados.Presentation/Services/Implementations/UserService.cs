using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PruebaTecnicaEmpleados.Presentation.Models;
using PruebaTecnicaEmpleados.Presentation.Services.Interface;

namespace PruebaTecnicaEmpleados.Presentation.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44302/api/users";

        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(user);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al agregar usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync("/api/users/" + id);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {

            try
            {
                    HttpResponseMessage response = await _httpClient.GetAsync("/api/users/"+id);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(jsonResponse);
             }
            catch (HttpRequestException ex)
            {
                    Console.WriteLine($"Error al obtener usuario por ID: {ex.Message}");
                    return null;
             }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(""); 
                response.EnsureSuccessStatusCode(); 
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(jsonResponse);
            }
            catch (HttpRequestException ex)
            {
            
                Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(user);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"/api/users/{user.Id}", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
                return false;
            }
        }
    }
}
using EasyBuy_Frontend_Admin.Dtos.Auth;
using EasyBuy_Frontend_Admin.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Services.AuthSvc
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7084"); 
        }

        public async Task<bool> Register(SignUpDTO signUpDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/User", signUpDTO);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while registering: " + ex.Message);
                return false;
            }
        }

        // Phương thức đăng nhập
        public async Task<string?> Login(SignInDTO signInDTO)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Auth/Login", signInDTO);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    if (responseData != null && responseData.TryGetValue("token", out string? token))
                    {
                        return token;
                    }
                }

                Debug.WriteLine("Đăng nhập không thành công. Trạng thái: " + response.IsSuccessStatusCode);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while logging in: " + ex.Message);
                return null;
            }
        }

    }
}

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace zhgut2app.Services
{
    public class ConnectivityService
    {
        private static readonly HttpClient client = new HttpClient();
        public const string baseUrl = "https://w29dq7t4-8000.euw.devtunnels.ms/";
        //public const static string baseUrl = "http://127.0.0.1:8000";

        public bool isLoggedIn { get; set; } = false;

        public string refreshKey = String.Empty;
        private string accessKey = String.Empty;

        public async Task<string> LogIn(string username, string password)
        {
            string refresh = string.Empty;

            var userData = new { username, password };
            try
            {
                var authRes = await client.PostAsJsonAsync($"{baseUrl}/auth/jwt/create/", userData);
                if (!authRes.IsSuccessStatusCode) return "nokey";

                var authJson = await authRes.Content.ReadFromJsonAsync<JsonElement>();
                string access = authJson.GetProperty("access").GetString();
                refresh = authJson.GetProperty("refresh").GetString();

            }
            catch (Exception ex)
            {
                return $"nokey {ex.Message}";
            }
            if (refresh == null)
            {
                return "nokey refresh is null";
            }

            isLoggedIn = true;
            return refresh;
        }

        public async Task<string?> Register(string email, string username, string password)
        {
            string output = "success";

            var registerData = new { email, username, password };

            try
            {
                var authRes = await client.PostAsJsonAsync($"{baseUrl}/auth/users/", registerData);

                if (authRes.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return output;
                }
                return authRes.Content.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> GetAccessToken(string refreshToken)
        {
            var refreshData = new { refresh = refreshToken };

            try
            {
                var authRes = await client.PostAsJsonAsync($"{baseUrl}/auth/jwt/refresh/", refreshData);
                if (!authRes.IsSuccessStatusCode) return string.Empty;

                var authJson = await authRes.Content.ReadFromJsonAsync<JsonElement>();
                string? access = authJson.GetProperty("access").GetString();

                if (access == null || access == string.Empty) return string.Empty;

                return access;

            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<string> GetYourself(string accessToken)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var authRes = await client.GetAsync($"{baseUrl}/auth/users/me/");
                if (!authRes.IsSuccessStatusCode) return string.Empty;

                var authJson = await authRes.Content.ReadFromJsonAsync<JsonElement>();
                string? me = authJson.GetProperty("username").GetString();

                if (me == null || me == string.Empty) return string.Empty;

                return me;

            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<string> GetHomeClientStatus(string accessToken) {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var authRes = await client.GetAsync($"{baseUrl}/smarthome/home_is_online/");
                if (!authRes.IsSuccessStatusCode) return "offline";

                return await authRes.Content.ReadAsStringAsync();

            }
            catch
            {
                return "offline";
            }
        }
    }
}

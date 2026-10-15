using Alexander_Yurtaev.AvaloniaAuth.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Data;

public class DataProvider : IDataProvider
{
    private readonly IHttpClientFactory _clientFactory;

    public DataProvider(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<RegisterResponse?> RegisterAsync(string email, string name, string password, CancellationToken ct)
    {
        var client = _clientFactory.CreateClient("fakeapifordevs");
        HttpRequestMessage request = new(method: HttpMethod.Post, requestUri: "/api/auth/register?page=1&per_page=5");

        var payload = new RegisterRequest(email, name, password);
        string json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
        if (response.Content.Headers.ContentLength == 0)
        {
            Console.WriteLine("Пришли пустые данные");
            throw new InvalidOperationException("Пришли пустые данные");
        }

        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>(ct);
        if (result is null)
        {
            Console.WriteLine($"{nameof(RegisterResponse)} is null");
        } else if (!string.IsNullOrEmpty(result.Error))
        {
            Console.WriteLine(result.Error);
        }
        return result;
    }

    public async Task<LoginResponse?> LoginAsync(string email, string password, CancellationToken ct)
    {
        var client = _clientFactory.CreateClient("fakeapifordevs");
        HttpRequestMessage request = new(method: HttpMethod.Post, requestUri: "/api/auth/login?page=1&per_page=5");

        var payload = new LoginRequest(email, password);
        string json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
        if (response.Content.Headers.ContentLength == 0)
        {
            Console.WriteLine("Пришли пустые данные");
            throw new InvalidOperationException("Пришли пустые данные");
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>(ct);
        if (result is null)
        {
            Console.WriteLine($"{nameof(LoginResponse)} is null");
        } else if (!string.IsNullOrEmpty(result.Error))
        {
            Console.WriteLine(result.Error);
            return null;
        }
        return result;
    }

    public async Task<UserDetails?> GetUserAsync(string accessToken, CancellationToken ct)
    {
        var client = _clientFactory.CreateClient("fakeapifordevs");
        HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: "/api/auth/me?page=1&per_page=5");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<MeResponse>(ct);
        if (result is null)
        {
            Console.WriteLine($"{nameof(MeResponse)} is null");
            return null;
        }

        if (!string.IsNullOrEmpty(result.Error))
        {
            Console.WriteLine(result.Error);
            return null;
        }
        return new UserDetails(result.Id, result.Email, result.Name, result.Avatar, result.Role);
    }

    public async Task<bool?> LogoutAsync(string refreshToken, CancellationToken ct)
    {
        var client = _clientFactory.CreateClient("fakeapifordevs");
        HttpRequestMessage request = new(method: HttpMethod.Post, requestUri: "/api/auth/logout?page=1&per_page=5");

        var payload = new LogoutRequest(refreshToken);
        string json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<LogoutResponse>(ct);
        if (result is null)
        {
            Console.WriteLine($"{nameof(LogoutResponse)} is null");
            return false;
        }

        if (!result.Success)
        {
            Console.WriteLine(result.Message);
            return false;
        }

        if (!string.IsNullOrEmpty(result.Error))
        {
            Console.WriteLine(result.Error);
            return false;
        }
        return true;
    }
}

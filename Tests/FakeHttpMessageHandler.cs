using Alexander_Yurtaev.AvaloniaAuth.Data;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Tests;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage message = new()
        {
            StatusCode = HttpStatusCode.NotFound
        };

        if (request.Method == HttpMethod.Post)
        {
            if (request.RequestUri?.AbsolutePath == "/api/auth/register")
            {
                await ProcessRegisterRequest(request, message);
            }
            else if (request.RequestUri?.AbsolutePath == "/api/auth/login")
            {
                await ProcessLoginRequest(request, message);
            }
            else if (request.RequestUri?.AbsolutePath == "/api/auth/logout")
            {
                await ProcessLogoutRequest(request, message);
            }
        }
        else if (request.Method == HttpMethod.Get)
        {
            if (request.RequestUri?.AbsolutePath == "/api/auth/me")
            {
                await ProcessMeRequest(request, message);
            }
        }

        return await Task.FromResult(message);
    }

    private async Task ProcessRegisterRequest(HttpRequestMessage request, HttpResponseMessage message)
    {
        string email = "";
        string name = "";

        if (request.Content is not null)
        {
            var bodyString = await request.Content.ReadAsStringAsync();
            var registerRequest = JsonSerializer.Deserialize<RegisterRequest>(bodyString);
            email = registerRequest!.Email;
            name = registerRequest!.Name;

        }
        else
        {
            message.StatusCode = HttpStatusCode.InternalServerError;
            return;
        }

        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(
$$"""
{
  "id": "usr_mv8jcu81",
  "email": "{{email}}",
  "name": "{{name}}",
  "role": "user",
  "status": "pending_verification",
  "created_at": "2026-10-14T20:01:43.681Z",
  "onboarding_checklist": {
    "email_verified": false,
    "profile_completed": false,
    "mfa_enabled": false
  }
}
""");
    }

    private async Task ProcessLoginRequest(HttpRequestMessage request, HttpResponseMessage message)
    {
        if (request.Content is not null)
        {
            var bodyString = await request.Content.ReadAsStringAsync();
            var loginRequest = JsonSerializer.Deserialize<LoginRequest>(bodyString);
        }
        else
        {
            message.StatusCode = HttpStatusCode.InternalServerError;
            return;
        }

        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(
"""
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c3JfMTIwMDAwIiwiZW1haWwiOiJoYXlkZW4uemFtYW4uNDE0QGZha2VhcGlmb3JkZXZzLmNvbSIsIm5hbWUiOiJIYXlkZW4gWmFtYW4iLCJyb2xlIjoiYWRtaW4iLCJ0eXAiOiJhY2Nlc3MiLCJpYXQiOjE3OTIwMDgzOTQsImV4cCI6MTc5MjAwOTI5NH0.TIdtLFwa1nyg7qKE4PQBfj4VAkU0e5gzNnBCercsY04",
  "refresh_token": "rt_mock_96ba5845d0454cf1894a8e8f2d52ee46",
  "token_type": "Bearer",
  "expires_in": 900,
  "user": {
    "id": "usr_120000",
    "email": "hayden.zaman.414@fakeapifordevs.com",
    "name": "Hayden Zaman",
    "role": "admin"
  }
}
""");
    }

    private async Task ProcessMeRequest(HttpRequestMessage request, HttpResponseMessage message)
    {
        if (request.Content is not null)
        {
            message.StatusCode = HttpStatusCode.InternalServerError;
            return;
        }

        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(
"""
{
    "id": "usr_120000",
    "email": "hayden.zaman.414@fakeapifordevs.com",
    "name": "Hayden Zaman",
    "role": "admin",
    "avatar": "https://avatar.vercel.sh/hayden.zaman.414%40fakeapifordevs.com",
    "created_at": "2025-01-01T00:00:00.000Z",
    "preferences": {
        "timezone": "America/New_York",
        "locale": "en-GB",
        "theme": "light"
    },
    "security": {
        "mfa_enabled": true,
        "last_login": "2025-01-02T00:00:00.000Z",
        "devices": [{
            "id": "dev_22",
            "name": "MacBook Pro",
            "last_used": "2025-01-02T00:00:00.000Z"
        }, {
            "id": "dev_23",
            "name": "Pixel 9",
            "last_used": "2025-01-01T12:00:00.000Z"
        }]
    }
}
""");
    }

    private async Task ProcessLogoutRequest(HttpRequestMessage request, HttpResponseMessage message)
    {
        if (request.Content is not null)
        {
            var bodyString = await request.Content.ReadAsStringAsync();
            var logoutRequest = JsonSerializer.Deserialize<LogoutRequest>(bodyString);
        }
        else
        {
            message.StatusCode = HttpStatusCode.InternalServerError;
            return;
        }

        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(
"""
{
  "success": true,
  "message": "Session revoked."
}
""");
    }
}
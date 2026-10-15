using System.Text.Json.Serialization;

namespace Alexander_Yurtaev.AvaloniaAuth.Data;

public record LogoutRequest
{
    public LogoutRequest(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;
}
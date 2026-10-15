using System.Text.Json.Serialization;

namespace Alexander_Yurtaev.AvaloniaAuth.Data;

public record RegisterRequest
{
    public RegisterRequest(string email, string name, string password)
    {
        Email = email;
        Name = name;
        Password = password;
    }

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
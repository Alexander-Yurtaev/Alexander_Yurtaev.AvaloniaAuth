using System.Text.Json.Serialization;

namespace Alexander_Yurtaev.AvaloniaAuth.Data;

public abstract record ResponseBase
{
    [JsonPropertyName("success")]
    public bool Success { get; set; } = true;

    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

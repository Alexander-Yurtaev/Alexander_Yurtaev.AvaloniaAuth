namespace Alexander_Yurtaev.AvaloniaAuth.Models;

public record AuthorizedModel
{
    public AuthorizedModel(string message = "")
    {
        Message = message;
    }

    public string Title => "Вы вошли как";
    public string Message { get; private set; }
}
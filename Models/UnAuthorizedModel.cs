namespace Alexander_Yurtaev.AvaloniaAuth.Models;

public record UnAuthorizedModel
{
    public string Title => "Вы не авторизованы";
    public string Message => "Пожалуйста, авторизуйтесь или зарегистрируйтесь,\r\nчтобы просмотреть данные пользователя.";
}
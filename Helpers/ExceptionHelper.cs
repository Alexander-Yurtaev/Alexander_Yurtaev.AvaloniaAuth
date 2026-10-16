using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace Alexander_Yurtaev.AvaloniaAuth.Helpers;

public static class ExceptionHelper
{
    public static string GetErrorMessage(Exception ex)
    {
        var baseEx = ex.GetBaseException();
        return baseEx switch
        {
            SocketException => "Ошибка подключения",
            HttpRequestException re when re.StatusCode==HttpStatusCode.TooManyRequests => "Превышен лимит запросов",
            HttpRequestException re when re.StatusCode==HttpStatusCode.InternalServerError => "Произошла ошибка на сервере",
            HttpRequestException re when re.StatusCode==HttpStatusCode.Unauthorized => "Требуется авторизация",
            HttpRequestException re when re.StatusCode==HttpStatusCode.Forbidden => "Нет прав доступа",
            HttpRequestException re when re.StatusCode==HttpStatusCode.NotFound => "Ресурс отсутствует",
            HttpRequestException re when re.StatusCode==HttpStatusCode.BadRequest => "Некорректный запрос",
            HttpRequestException re when re.StatusCode==HttpStatusCode.ServiceUnavailable => "Сервис на обслуживании",
            _ => baseEx.Message
        };
    }
}

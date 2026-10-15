# Alexander_Yurtaev.AvaloniaAuth

## Используемые NuGet-пакеты
*Пакеты для работы с Avalonia*
- Avalonia, Version="12.1.4"
- Avalonia.Desktop, Version="12.1.4"
- Avalonia.Themes.Fluent, Version="12.1.4"
- Avalonia.Fonts.Inter, Version="12.1.4"
- AvaloniaUI.DiagnosticsSupport, Version="2.2.3"

*Пакеты для работы с MVVM*
- CommunityToolkit.Mvvm, Version="8.4.2"

*Пакеты для работы с DI*
- Microsoft.Extensions.DependencyInjection, Version="10.0.13"

*Пакеты для работы с HttpClient*
- Microsoft.Extensions.Http, Version="10.0.13"

# Особенности работы приложения с данными
- необходимо зайти в файл App.axaml.cs
- найти флаг UseFakeHandler
- если установить UseFakeHandler=true, то данные будут браться из Tests/FakeHttpMessageHandler.cs (на случай, если сайт недоступен)
- если установить UseFakeHandler=false, то будут браться данные с сайта https://fakeapifordevs.vercel.app
- в файле Tests/FakeHttpMessageHandler.cs можно установить задержки, либо выдавать ошибки при эмуляции отдачи данных
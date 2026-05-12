# WebApi.Template

Шаблон решения для **ASP.NET Core Web API** в стиле **Clean Architecture** (C#, `.NET`, solution template).  
Репозиторий задуман как публичный pet‑project и публикуемый пакет‑шаблон для `dotnet new`.

## Требования

- .NET SDK (шаблон таргетит `net10.0`)
- PostgreSQL (EF Core провайдер по умолчанию — `Npgsql`)

## Установка шаблона

После публикации в NuGet:

```bash
dotnet new install WebApi.Template
```

Локальная установка из `.nupkg` (путь/версия — ваши):

```bash
dotnet new install .\nupkg\WebApi.Template.1.0.0.nupkg
```

Проверить, что шаблон виден:

```bash
dotnet new list tempsol
```

## Создать новое решение

`shortName` шаблона: `tempsol`

```bash
dotnet new tempsol -n MyCompany.MyService
cd .\MyCompany.MyService
```

## Запуск API

```bash
dotnet run --project .\WebApi.Template\WebApi.Template.csproj
```

В `Development` включается Swagger UI на корне (route prefix пустой).

## Конфигурация (appsettings)

Файлы:
- `WebApi.Template/appsettings.Development.json`
- `WebApi.Template/appsettings.Production.json`

Обязательные настройки:
- **Database**: `ConnectionStrings:DefaultConnection`
- **CORS**: `CORS:Url` (используется политикой `AllowFrontend`)
- **JWT**: `JwtSettings:Issuer`, `JwtSettings:Audience`, `JwtSettings:SecretKey`  
  `SecretKey` должен быть **минимум 32 символа**, иначе приложение упадёт при старте (JWT регистрируется в `AddInfrastructure`).

Пример (заполни реальными значениями):

```json
{
  "JwtSettings": {
    "Issuer": "my-issuer",
    "Audience": "my-audience",
    "SecretKey": "0123456789ABCDEF0123456789ABCDEF"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=postgres"
  },
  "CORS": {
    "Url": "http://localhost:3000"
  }
}
```

## Что внутри

- **`WebApi.Template`**: хост ASP.NET Core (`Program.cs`, контроллеры)
- **`WebApi.Template.Domain`**: доменная модель
- **`WebApi.Template.Application`**: use‑cases/контракты, `Mediator.Abstractions`, `ErrorOr`
- **`WebApi.Template.Infrastructure`**:
  - EF Core (`MyDbContext`, `EfUnitOfWork`)
  - JWT (`Microsoft.AspNetCore.Authentication.JwtBearer`)
  - Swagger (`Swashbuckle.AspNetCore`)
  - DI модули (`AddInfrastructure`, `AddData`, `AddJwt`, `AddCorsPolicies`, `AddSwagger`)
  - автоподключение портов/query‑objects через `Scrutor`

## Дефолтное поведение

- **Маршруты**: пример контроллера доступен по `GET /temp`
- **Auth**: в пайплайне уже подключены `UseAuthentication()` и `UseAuthorization()`
- **Миграции**: вызов `Database.MigrateAsync()` сейчас **закомментирован** (TODO в `Program.cs`)
  - Если раскомментировать `await context.Database.MigrateAsync();`, приложение будет **автоматически применять все неприменённые EF Core миграции** при старте.
  - Для pet‑проекта это удобно, но для production обычно делают отдельный шаг деплоя или включают автоприменение **по флагу/окружению**.
  - Важно: это сработает только если миграции **созданы** (есть `Migrations` в `WebApi.Template.Infrastructure`).

## Упаковка шаблона (.nupkg)

В репозитории есть:
- `WebApi.Template.nuspec` (package type `Template`)
- `.template.config/template.json` (`shortName: tempsol`)

Чтобы собрать `.nupkg`, удобнее использовать NuGet CLI:

```bash
nuget pack .\WebApi.Template.nuspec -OutputDirectory .\nupkg
```

Дальше — установка и создание проекта:

```bash
dotnet new install .\nupkg\WebApi.Template.1.0.0.nupkg
dotnet new tempsol -n MyCompany.MyService
```


# WebApi.Template

Шаблон решения для **ASP.NET Core Web API** в стиле **Clean Architecture** (C#, `.NET`, solution template).  
Репозиторий задуман как публичный pet‑project и публикуемый пакет‑шаблон для `dotnet new`.

## Требования

- .NET SDK (шаблон таргетит `net10.0`)
- PostgreSQL (EF Core провайдер по умолчанию — `Npgsql`)

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
dotnet run --project .\MyCompany.MyService\MyCompany.MyService.csproj
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

## Используемые NuGet пакеты

Ниже перечислены основные пакеты, которые уже подключены в шаблоне (с версиями из `.csproj`).

### WebApi.Template (API)

- **Microsoft.EntityFrameworkCore.Design (10.0.6)**: tooling для EF Core (миграции/скэффолдинг). В проекте помечен как `PrivateAssets=all`.

### WebApi.Template.Application

- **Mediator.Abstractions (3.0.2)**: абстракции Mediator (интерфейсы/контракты) для построения use-cases/обработчиков.
- **ErrorOr (2.0.1)**: удобный тип результата “успех/ошибка” для use-cases и доменной/прикладной логики.

### WebApi.Template.Infrastructure

- **Microsoft.EntityFrameworkCore (10.0.6)**: EF Core runtime.
- **Npgsql.EntityFrameworkCore.PostgreSQL (10.0.1)**: провайдер EF Core для PostgreSQL.
- **Microsoft.EntityFrameworkCore.Design (10.0.6)**: tooling EF Core (миграции). Помечен как `PrivateAssets=all`.
- **Microsoft.AspNetCore.Authentication.JwtBearer (10.0.6)**: JWT Bearer аутентификация.
- **Swashbuckle.AspNetCore (10.1.7)**: Swagger/OpenAPI генерация и UI.
- **Scrutor (7.0.0)**: assembly scanning для авто‑регистрации реализаций (порты/query objects).
- **Mediator.SourceGenerator (3.0.2)**: source generator для Mediator. Помечен как `PrivateAssets=all`.
- **Microsoft.Extensions.Configuration (10.0.6)** и **Microsoft.Extensions.DependencyInjection (10.0.6)**: базовые пакеты конфигурации и DI (на самом деле обычно уже приезжают транзитивно, но здесь зафиксированы явно).

## Дефолтное поведение

- **Маршруты**: пример контроллера доступен по `GET /temp`
- **Auth**: в пайплайне уже подключены `UseAuthentication()` и `UseAuthorization()`
- **Миграции**: вызов `Database.MigrateAsync()` сейчас **закомментирован** (TODO в `Program.cs`)
  - Если раскомментировать `await context.Database.MigrateAsync();`, приложение будет **автоматически применять все неприменённые EF Core миграции** при старте.
  - Для pet‑проекта это удобно, но для production обычно делают отдельный шаг деплоя или включают автоприменение **по флагу/окружению**.
  - Важно: это сработает только если миграции **созданы** (есть `Migrations` в `WebApi.Template.Infrastructure`).

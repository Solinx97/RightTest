# Техническое задание
Необходимо реализовать сервис со следующим функционалом на языке C# с
использованием .NET 8.
Требования
В базе данных Postgres должны быть таблицы
1) currency:
● id — первичный ключ
● name — название валюты
● rate — курс валюты к рублю
2) user
● id — первичный ключ
● name — имя пользователя
● Password - пароль
Пользователь интересуется только определенным набором валют (favorites)
Задачи:
1) Реализовать микросервис миграции БД
2) Реализовать фоновый сервис, который обращается по адресу
http://www.cbr.ru/scripts/XML_daily.asp и полученными данными заполняет таблицу
currency
3) Реализовать микросервис юзера. Нужный функционал – регистрация юзера, логин,
логаут
4) Реализовать микросервис финансов. Нужный функционал – получить курс валют
по юзеру.
И 3 и 4 должны быть реализованы через Clean Architecture и CQRS.
5) Реализовать авторизацию с помощью JWT.
6) Реализовать API Gateway для обоих микросервисов.
7) Проект с unit-тестами для сервисов юзера и финансов. Для фонового не надо.
Требования к присылаемым решениям
1. Готовые задания должны быть переданы ссылкой на гитхаб
2. Исходный код должен компилироваться средствами MSVS 2022.
3. В архиве не должно быть неиспользуемых исходных кодов, ресурсов или
промежуточных файлов сборки

# Как запустить
### Предварительная подготовка
1) Postgresql БД должна быть установлена локально или скачан Docker Image (например, my-postgres) и запущен необходимый Docker Container.

### Работа с Решением
1) Клонировать Репо
2) Создать .Net User Secrets или appsettings.json файл (формат может отличаться от User Secrets). Заполнить следующим образом:
    - для APIGateway
    {
        "ConnectionStrings:FinancesConnection": "your-connection-string-to-Postgresql-finances-db",
        "ConnectionStrings:UsersConnection": "your-connection-string-to-Postgresql-users-db",
        "JWT:Key": "your-jwt-secure-key",
        "Authentication:Issuer": "url-who-issue-jwt: https://localhost:7189/",
        "Authentication:Authority": "url-who-jwt-author: https://localhost:7189/",
        "Authentication:Client:Audiences": "finances-api,user-api",
        "Authentication:Client:Scopes": "api.read,api.write"
    }
    - для RightTest.FinancesAPI
    {
        "ConnectionStrings:DefaultConnection": "your-connection-string-to-Postgresql-finances-db,
        "JWT:Key": "your-jwt-secure-key",
        "Authentication:Issuer": "url-who-issue-jwt: https://localhost:7189/",
        "Authentication:Client:Scopes": "api.read,api.write"
    }
    - для RightTest.UsersAPI
    {
        "ConnectionStrings:DefaultConnection": "your-connection-string-to-Postgresql-users-db",
        "JWT:Key": "your-jwt-secure-key",
        "JWT:Issuer": "url-who-issue-jwt: https://localhost:7189/",
        "JWT:Audiences": "finances-api",
        "JWT:Scopes": "api.read,api.write",
        "JWT:ValidHours": "24"
    }
    - для RightTest.MigrationsAPI
    {
        "ConnectionStrings:FinancesConnection": "your-connection-string-to-Postgresql-finances-db",
        "ConnectionStrings:UsersConnection": "your-connection-string-to-Postgresql-users-db",
    }
    - для RightTest.UsersBL.IntegrationTests (TESTS)
    {
        "JWT:Key": "your-jwt-secure-key",
    }
3) Собрать Решение (Solution)
4) Запустить интересующие Проекты (или все проекты)
5) Выполнить запросы черзез Swagger или Requests app (например, Postman)

# Запуск тестов
Интеграционные тесты требуют наличие установленного и запущенного Docker (используется Testcontainers.PostgreSql).
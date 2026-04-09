[![License](https://img.shields.io/github/license/denis-peshkov/Cross.Identity)](LICENSE)
[![GitHub Release Date](https://img.shields.io/github/release-date/denis-peshkov/Cross.Identity?label=released)](https://github.com/denis-peshkov/Cross.Identity/releases)
[![NuGetVersion](https://img.shields.io/nuget/v/Cross.Identity.svg)](https://nuget.org/packages/Cross.Identity/)
[![NugetDownloads](https://img.shields.io/nuget/dt/Cross.Identity.svg)](https://nuget.org/packages/Cross.Identity/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Cross.Identity&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Cross.Identity)
[![issues](https://img.shields.io/github/issues/denis-peshkov/Cross.Identity)](https://github.com/denis-peshkov/Cross.Identity/issues)
[![.NET PR](https://github.com/denis-peshkov/Cross.Identity/actions/workflows/dotnet.yml/badge.svg?event=pull_request)](https://github.com/denis-peshkov/Cross.Identity/actions/workflows/dotnet.yml)

![Size](https://img.shields.io/github/repo-size/denis-peshkov/Cross.Identity)
[![GitHub contributors](https://img.shields.io/github/contributors/denis-peshkov/Cross.Identity)](https://github.com/denis-peshkov/Cross.Identity/contributors)
[![GitHub commits since latest release (by date)](https://img.shields.io/github/commits-since/denis-peshkov/Cross.Identity/latest?label=new+commits)](https://github.com/denis-peshkov/Cross.Identity/commits/master)
![Activity](https://img.shields.io/github/commit-activity/w/denis-peshkov/Cross.Identity)
![Activity](https://img.shields.io/github/commit-activity/m/denis-peshkov/Cross.Identity)
![Activity](https://img.shields.io/github/commit-activity/y/denis-peshkov/Cross.Identity)

# Cross.Identity

Библиотека идентификации и аутентификации для .NET: настраиваемые сценарии (регистрация, вход, восстановление пароля, выдача и обновление токенов), JWT, Argon2, верификация по email/SMS, процессный движок с JSON-описанием потоков.

## Возможности

- **Process Engine** — выполнение сценариев (flow) по JSON-дефинициям с последовательными шагами (steps).
- **Потоки** — регистрация, вход по паролю/коду, forgot password, token, refresh token, получение пользователя, запрос и проверка кодов (email/SMS).
- **JWT** — выпуск и валидация access/refresh токенов, настраиваемые claims и время жизни.
- **Безопасность** — хеширование паролей (Argon2), одноразовые коды, нормализация телефонов.
- **Каналы** — email и SMS (отправка кодов через Cross.Notification).
- **Формы** — декларативное описание полей и правил валидации (equal, requiredIf, atLeastOneRequired и др.).


## Unit-тесты

Используется соглашение именования **Given_When_Then**:

- **Given** — контекст/предусловия.
- **When** — действие.
- **Then** — ожидаемый результат.

Пример: `Given_ExistingUser_When_RequestCode_Then_SendsCodeAndReturnsLastCode`.

Тесты потоков и шагов расположены в `Cross.Identity.UnitTests/Identity/` (FlowTests, StepTests, StepFactoryTests).

## Дополнительно

- [LICENSE.md](LICENSE.md) — лицензия.

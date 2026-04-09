[![License](https://img.shields.io/github/license/denis-peshkov/Cross.PepperVault)](LICENSE)
[![GitHub Release Date](https://img.shields.io/github/release-date/denis-peshkov/Cross.PepperVault?label=released)](https://github.com/denis-peshkov/Cross.PepperVault/releases)
[![NuGetVersion](https://img.shields.io/nuget/v/Cross.PepperVault.svg)](https://nuget.org/packages/Cross.PepperVault/)
[![NugetDownloads](https://img.shields.io/nuget/dt/Cross.PepperVault.svg)](https://nuget.org/packages/Cross.PepperVault/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Cross.PepperVault&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Cross.PepperVault)
[![issues](https://img.shields.io/github/issues/denis-peshkov/Cross.PepperVault)](https://github.com/denis-peshkov/Cross.PepperVault/issues)
[![.NET PR](https://github.com/denis-peshkov/Cross.PepperVault/actions/workflows/dotnet.yml/badge.svg?event=pull_request)](https://github.com/denis-peshkov/Cross.PepperVault/actions/workflows/dotnet.yml)

![Size](https://img.shields.io/github/repo-size/denis-peshkov/Cross.PepperVault)
[![GitHub contributors](https://img.shields.io/github/contributors/denis-peshkov/Cross.PepperVault)](https://github.com/denis-peshkov/Cross.PepperVault/contributors)
[![GitHub commits since latest release (by date)](https://img.shields.io/github/commits-since/denis-peshkov/Cross.PepperVault/latest?label=new+commits)](https://github.com/denis-peshkov/Cross.PepperVault/commits/master)
![Activity](https://img.shields.io/github/commit-activity/w/denis-peshkov/Cross.PepperVault)
![Activity](https://img.shields.io/github/commit-activity/m/denis-peshkov/Cross.PepperVault)
![Activity](https://img.shields.io/github/commit-activity/y/denis-peshkov/Cross.PepperVault)

# Cross.PepperVault

A set of **.NET** libraries for **configurable loading of pepper** values (secret strings used to harden password hashes and similar use cases), with support for **multiple versions**, a **TTL cache**, **reload**, and **FluentValidation** of options at startup. The core defines the `IPepperVaultProvider` contract and shared infrastructure; data sources live in separate NuGet packages.

## Features

- **Pepper versions** — version dictionary, active current version, rotation without application downtime.
- **Cache and TTL** — providers built on `PepperProviderBase` cache loads and refresh on timeout and when options change via `IOptionsMonitor`.
- **Configuration** — `Pepper` section, binding via `AddPepperOptions`, `ValidateOnStart()`, and composite FluentValidation validators.
- **Providers** — environment, file, Azure Key Vault, AWS Secrets Manager, GCP Secret Manager, HashiCorp Vault, and more; reference only what you need.
- **Extensibility** — plug in your own source: inherit `PepperProviderBase` and register `IPepperVaultProvider` in DI.

## Target frameworks

All projects target **netstandard2.1** and **net6.0** through **net10.0** (see `.csproj`).

## Packages

### Registry

Per-package NuGet download and latest-version badges. **Issues:** the core package row shows the repo-wide open-issue count; each provider row shows the count for a GitHub search scoped to this repository and the full NuGet package id (there are no per-package labels in this repo).

| Package | Downloads | NuGet latest | Issues |
| ------- | --------- | ------------ | ------ |
| **Cross.PepperVault** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault)](https://www.nuget.org/packages/Cross.PepperVault/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault)](https://www.nuget.org/packages/Cross.PepperVault/) | [![issues](https://img.shields.io/github/issues/denis-peshkov/Cross.PepperVault)](https://github.com/denis-peshkov/Cross.PepperVault/issues) |
| **Cross.PepperVault.Env** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.Env)](https://www.nuget.org/packages/Cross.PepperVault.Env/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.Env)](https://www.nuget.org/packages/Cross.PepperVault.Env/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.Env)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.Env) |
| **Cross.PepperVault.EnvJson** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.EnvJson)](https://www.nuget.org/packages/Cross.PepperVault.EnvJson/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.EnvJson)](https://www.nuget.org/packages/Cross.PepperVault.EnvJson/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.EnvJson)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.EnvJson) |
| **Cross.PepperVault.FileJson** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.FileJson)](https://www.nuget.org/packages/Cross.PepperVault.FileJson/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.FileJson)](https://www.nuget.org/packages/Cross.PepperVault.FileJson/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.FileJson)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.FileJson) |
| **Cross.PepperVault.AzureKvJson** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.AzureKvJson)](https://www.nuget.org/packages/Cross.PepperVault.AzureKvJson/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.AzureKvJson)](https://www.nuget.org/packages/Cross.PepperVault.AzureKvJson/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.AzureKvJson)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.AzureKvJson) |
| **Cross.PepperVault.AzureKvVersioned** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.AzureKvVersioned)](https://www.nuget.org/packages/Cross.PepperVault.AzureKvVersioned/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.AzureKvVersioned)](https://www.nuget.org/packages/Cross.PepperVault.AzureKvVersioned/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.AzureKvVersioned)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.AzureKvVersioned) |
| **Cross.PepperVault.AwsSecretsJson** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.AwsSecretsJson)](https://www.nuget.org/packages/Cross.PepperVault.AwsSecretsJson/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.AwsSecretsJson)](https://www.nuget.org/packages/Cross.PepperVault.AwsSecretsJson/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.AwsSecretsJson)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.AwsSecretsJson) |
| **Cross.PepperVault.GcpSecretManagerJson** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.GcpSecretManagerJson)](https://www.nuget.org/packages/Cross.PepperVault.GcpSecretManagerJson/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.GcpSecretManagerJson)](https://www.nuget.org/packages/Cross.PepperVault.GcpSecretManagerJson/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.GcpSecretManagerJson)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.GcpSecretManagerJson) |
| **Cross.PepperVault.HcvKv2Json** | [![NuGet](https://img.shields.io/nuget/dt/Cross.PepperVault.HcvKv2Json)](https://www.nuget.org/packages/Cross.PepperVault.HcvKv2Json/) | [![NuGet](https://img.shields.io/nuget/v/Cross.PepperVault.HcvKv2Json)](https://www.nuget.org/packages/Cross.PepperVault.HcvKv2Json/) | [![issues](https://img.shields.io/github/issues-search?query=repo%3Adenis-peshkov%2FCross.PepperVault+is%3Aissue+Cross.PepperVault.HcvKv2Json)](https://github.com/denis-peshkov/Cross.PepperVault/issues?q=is%3Aissue+Cross.PepperVault.HcvKv2Json) |

### `Cross.PepperVault`

Core package: shared **options model**, **validation wiring**, and the **provider abstraction**. It exposes `IPepperVaultProvider` (current version, version dictionary, `TryGet`, `ReloadAsync`) and the caching base class `PepperProviderBase`, which uses a TTL from `IOptionsMonitor<TimeSpan>` (typically from `PepperOptions<T>.Ttl` through `OptionsMonitorProxyForTtl<T>`), a semaphore around reloads, and refresh when data was never loaded or the TTL elapsed.

`PepperOptions<TProviderOptions>` binds under the configuration section **`Pepper`**: `Provider` (discriminator string), `Ttl`, and `ProviderOptions` (provider-specific payload). **`AddPepperOptions<TProviderOptions, TProviderOptionsValidator>`** registers **`PepperBaseValidator<T>`** plus your provider validator, **`CompositeFluentOptionsValidatorAdapter`** as `IValidateOptions<>`, binds **`Pepper`**, and enables **`ValidateOnStart()`** so invalid configuration fails at startup. Reference this package in every app that uses any provider.

### `Cross.PepperVault.Env`

**Configuration-bound** provider: peppers are supplied directly from configuration (appsettings, env vars, etc.) under `Pepper:ProviderOptions`. **`EnvPepperProvider`** reads **`Current`** and **`Peppers`** on each load; there are no outbound secret-store calls. `EnvProviderOptions` holds **`Current`** (active `short` version) and **`Peppers`** (`Dictionary<short, string>`). Runtime validation ensures the map is non-empty and that **`Current`** maps to a non-whitespace pepper.

Use a conventional **`Pepper.Provider`** value such as **`Env`** (not fixed by `EnvProviderOptionsValidator`; `PepperBaseValidator` still requires `Provider` to be non-empty). Register **`AddPepperOptions<EnvProviderOptions, EnvProviderOptionsValidator>`** and **`EnvPepperProvider`** as `IPepperVaultProvider`.

### `Cross.PepperVault.EnvJson`

Reads one JSON document from a **process environment variable**. Default variable name is **`AUTH_PEPPERS_JSON`**; override with **`ProviderOptions.JsonKey`**. The JSON root must contain **`current`** (number) and **`peppers`** (object: version keys as strings, values as pepper strings). **`current`** must exist in **`peppers`**.

`EnvJsonProviderOptionsValidator` requires a non-empty **`JsonKey`**. There is **no** required literal for **`Pepper.Provider`** in this package—choose a clear string in your config and docs. Use **`AddPepperOptions<EnvJsonProviderOptions, EnvJsonProviderOptionsValidator>`** and **`EnvJsonPepperProvider`**.

### Shared JSON shape (EnvJson, FileJson, single-secret cloud providers, Vault KV JSON)

```json
{
  "current": 3,
  "peppers": {
    "1": "pepper-for-v1",
    "2": "pepper-for-v2",
    "3": "pepper-for-v3"
  }
}
```

Property names **`current`** and **`peppers`** are **lowercase** in the payload, as expected by the built-in parsers.

### `Cross.PepperVault.FileJson`

Loads the same JSON from a **local file**. **`ProviderOptions.Path`** must be an **absolute** path (FluentValidation enforces this). Set **`Pepper.Provider`** to **`FileJson`** (case-insensitive). The file is read on each provider reload. Register **`AddPepperOptions<FileJsonProviderOptions, FileJsonProviderOptionsValidator>`** and **`FileJsonPepperProvider`**.

### `Cross.PepperVault.AzureKvJson`

Fetches **one Key Vault secret** whose string value **is** the JSON blob (`current` + `peppers`). **`ProviderOptions`**: **`VaultUri`** (HTTPS absolute URI to `https://{vault}.vault.azure.net/`), **`SecretName`**, optional **`AzureCredential`** — omit → **`DefaultAzureCredential`**; **`managed:`** / **`managed:{clientId}`** → managed identity.

**`Pepper.Provider`** must be **`AzureKvJson`**. Uses **`SecretClient`**. Register **`AddPepperOptions<AzureKvJsonProviderOptions, AzureKvJsonProviderOptionsValidator>`** and **`AzureKvJsonPepperProvider`**.

### `Cross.PepperVault.AzureKvVersioned`

Uses **multiple versions** of the same secret **name**. Each **enabled** version should carry a tag (default name **`pepperVersion`**, configurable via **`TagName`**) with the pepper **version number**. The provider builds **version → secret value** and sets **`current`** to the **maximum** tagged version among enabled entries. Untagged or unreadable versions are skipped.

**`Pepper.Provider`** must be **`AzureKvVersions`**. Register **`AddPepperOptions<AzureKvVersionsProviderOptions, AzureKvVersionsProviderOptionsValidator>`** and **`AzureKvVersionedPepperProvider`**.

### `Cross.PepperVault.AwsSecretsJson`

Loads **one AWS Secrets Manager** secret (`SecretId` as name or ARN); payload must be the shared JSON. Register **`IAmazonSecretsManager`** in DI yourself (region/credentials per your deployment).

**`Pepper.Provider`** must be **`AwsJson`** (NuGet package id remains **`Cross.PepperVault.AwsSecretsJson`**). Register **`AddPepperOptions<AwsSecretsJsonProviderOptions, AwsSecretsJsonProviderOptionsValidator>`** and **`AwsSecretsJsonPepperProvider`**.

### `Cross.PepperVault.GcpSecretManagerJson`

Reads **`SecretName`** formatted as **`projects/{projectId}/secrets/{secretId}`**, always version **`latest`**. Inject **`SecretManagerServiceClient`** with appropriate GCP credentials.

**`Pepper.Provider`** must be **`GcpJson`**. Register **`AddPepperOptions<GcpSecretManagerJsonProviderOptions, GcpSecretManagerJsonProviderOptionsValidator>`** and **`GcpSecretManagerJsonPepperProvider`**.

### `Cross.PepperVault.HcvKv2Json`

**HashiCorp Vault KV v2** via **VaultSharp** (token authentication). **`ProviderOptions`**: **`VaultUri`**, **`Token`**, **`Mount`** (engine mount), **`Path`** (secret path). Secret **data** is serialized to JSON and parsed as a root object with **`current`** and **`peppers`**.

**`Pepper.Provider`** must be **`HcvKv2Json`**. Register **`AddPepperOptions<HcvKv2JsonProviderOptions, HcvKv2JsonProviderOptionsValidator>`** and **`HcvKv2JsonPepperProvider`**.

### Target frameworks and version lines

> **Target frameworks:** libraries target **netstandard2.1** and **.NET 6** through **.NET 10**. Keep **Cross.PepperVault** and your provider packages on **compatible NuGet versions** (same release line); pin versions in production.

### Package Manager Console

```powershell
# Package Manager Console (Visual Studio) — install core + any providers you need
Install-Package Cross.PepperVault
Install-Package Cross.PepperVault.Env
Install-Package Cross.PepperVault.EnvJson
Install-Package Cross.PepperVault.FileJson
Install-Package Cross.PepperVault.AzureKvJson
Install-Package Cross.PepperVault.AzureKvVersioned
Install-Package Cross.PepperVault.AwsSecretsJson
Install-Package Cross.PepperVault.GcpSecretManagerJson
Install-Package Cross.PepperVault.HcvKv2Json
```

Register **`AddPepperOptions`** (with your provider options and validator types) and **`IPepperVaultProvider`** in DI — see **Quick start** below.

## Quick start

1. Add NuGet packages (example: core + Env):

```bash
dotnet add package Cross.PepperVault
dotnet add package Cross.PepperVault.Env
```

2. Register options and the provider:

```csharp
services.AddPepperOptions<EnvProviderOptions, EnvProviderOptionsValidator>(configuration);
services.AddSingleton<IPepperVaultProvider, EnvPepperProvider>();
```

3. Sample configuration for **Env**:

```json
{
  "Pepper": {
    "Provider": "Env",
    "Ttl": "00:10:00",
    "ProviderOptions": {
      "Current": 1,
      "Peppers": {
        "1": "your-secret-pepper-v1",
        "2": "your-secret-pepper-v2"
      }
    }
  }
}
```

For each provider, set **`Pepper.Provider`** to the literal expected by that package’s validator (e.g. **`FileJson`**, **`AzureKvJson`**, **`AwsJson`**, **`GcpJson`**, **`AzureKvVersions`**, **`HcvKv2Json`**) or your chosen string where no literal is enforced (**`Env`**, **`EnvJson`**). Shape **`ProviderOptions`** to match that provider’s options type.

## Build

```bash
dotnet build Cross.PepperVault.slnx -c Release
```

Packaging: `config.nuspec` in each project directory (after a Release build).

## Unit tests

Tests follow the **Given_When_Then** naming convention:

- **Given** — context / preconditions.
- **When** — the action under test.
- **Then** — the expected outcome.

Example: `Given_EmptyProvider_When_Validate_Then_Fails`.

Tests live in the `Cross.PepperVault.Tests` project (`Options`, `Providers`, `Extensions`, and related folders).

## See also

- [LICENSE](LICENSE) — license.

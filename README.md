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

| Package | Purpose |
|---------|---------|
| **Cross.PepperVault** | Core: options, validation, `IPepperVaultProvider`, `PepperProviderBase`. |
| **Cross.PepperVault.Env** | Pepper from configuration (`Pepper:ProviderOptions`: current version and dictionary). |
| **Cross.PepperVault.EnvJson** | JSON from an environment variable. |
| **Cross.PepperVault.FileJson** | JSON from a file. |
| **Cross.PepperVault.AzureKvJson** | Azure Key Vault secret whose payload is versioned JSON. |
| **Cross.PepperVault.AzureKvVersioned** | Multiple secret versions in Key Vault via tags. |
| **Cross.PepperVault.AwsSecretsJson** | AWS Secrets Manager secret as JSON. |
| **Cross.PepperVault.GcpSecretManagerJson** | Google Cloud Secret Manager. |
| **Cross.PepperVault.HcvKv2Json** | HashiCorp Vault KV v2 via VaultSharp. |

## Quick start

1. Install the packages, for example: `Cross.PepperVault` + `Cross.PepperVault.Env`.
2. Register options and the provider:

```csharp
services.AddPepperOptions<EnvProviderOptions, EnvProviderOptionsValidator>(configuration);
services.AddSingleton<IPepperVaultProvider, EnvPepperProvider>();
```

3. Sample configuration for `Env`:

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

Provider names and `ProviderOptions` shape depend on the package you choose.

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

Example: `Given_ExistingUser_When_RequestCode_Then_SendsCodeAndReturnsLastCode`.

Flow and step tests live under `Cross.PepperVault.UnitTests/PepperVault/` (FlowTests, StepTests, StepFactoryTests).

## See also

- [LICENSE](LICENSE) — license.

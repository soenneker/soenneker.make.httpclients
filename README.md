[![](https://img.shields.io/nuget/v/soenneker.make.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.make.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.make.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.httpclients/)

# Soenneker.Make.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Make.HttpClients
```

## Quick start

```csharp
using Soenneker.Make.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddMakeOpenApiHttpClientAsSingleton();
```

Adds `MakeOpenApiHttpClient` as a singleton service.

## What you get

- `IMakeOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `MakeOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IMakeOpenApiHttpClient.Get(apiKey, cancellationToken)` | Gets a client for a specific API key using the configured base URL. | A task whose result is the requested http Client. |
| `IMakeOpenApiHttpClient.Get(apiKey, baseUrl, cancellationToken)` | Gets a client for a specific Make connection. | A task whose result is the requested http Client. |
| `MakeOpenApiHttpClientRegistrar.AddMakeOpenApiHttpClientAsSingleton(services)` | Adds `MakeOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `MakeOpenApiHttpClientRegistrar.AddMakeOpenApiHttpClientAsScoped(services)` | Adds `MakeOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.

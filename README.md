[![](https://img.shields.io/nuget/v/soenneker.make.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.make.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.make.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.make.httpclients/actions/workflows/codeql.yml)

# Soenneker.Make.HttpClients

Provides cached, authenticated `HttpClient` instances for the Make API.

## Install

```bash
dotnet add package Soenneker.Make.HttpClients
```

## Configuration

```json
{
  "Make": {
    "ApiKey": "your-api-key",
    "ClientBaseUrl": "https://us1.make.com/api/v2"
  }
}
```

`ClientBaseUrl` is optional and defaults to Make's `us1` API. Store the API key in a secret provider rather than committed configuration.

## Usage

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Make.HttpClients.Abstract;
using Soenneker.Make.HttpClients.Registrars;

services.AddMakeOpenApiHttpClientAsSingleton();

IMakeOpenApiHttpClient factory =
    serviceProvider.GetRequiredService<IMakeOpenApiHttpClient>();

HttpClient client = await factory.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync("users/me", cancellationToken);
response.EnsureSuccessStatusCode();
```

Use `Get(apiKey)` to select credentials per call, or `Get(apiKey, baseUrl)` for a different Make region. The same credential, base URL, and authentication configuration reuse the same cached client.

## Client reuse

- Do not dispose a returned `HttpClient`; its lifetime is owned by the registered wrapper/cache.
- Calls using the same API key, base URL, header name, and header template reuse one client.
- Singleton registration shares cached clients application-wide. Scoped registration owns a scoped cache and removes its clients when the scope ends.
- `Make:AuthHeaderName` and `Make:AuthHeaderValueTemplate` can override authentication for a compatible gateway. The template must contain `{token}` if the key should be sent.

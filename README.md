# spec-first-api

An exercise in creating an API in "spec-first" fashion. Hand-rolling a Swagger/OpenAPI specification, then generating both a server stub and client library.

Uses [NSwag](https://github.com/RicoSuter/NSwag) tooling.

# Getting Started

## Prerequisites
Install .NET 5.x SDK
* https://dotnet.microsoft.com/download/dotnet/5.0

```
# build solution, which will generate server/client code
dotnet build

# execute both api and client tests, to briefly prove the flow
dotnet test
```

# Components

## API

NSwag is configured to generate MVC controllers/models, with all the necessary attributes. This generated controller takes an injected interface with an identical contract. User code should implement these interfaces, and register the implementation with the dependency injection container.

In practice, spec changes will show up as implementations no longer implementing the expected interface, which would result in compile failures.

## Client

NSwag generates a series of `*Client` classes, as well as models. Library consumers can use these as-is, or customize by extending the generated class and implementing a number of partial methods/callbacks.

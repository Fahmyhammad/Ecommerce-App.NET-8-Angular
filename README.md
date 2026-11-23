# Ecommerce API

A .NET 8 backend for a product catalog and shopping basket, organized into API, Core and Infrastructure projects.

## What is implemented

- Product and category CRUD endpoints.
- Product search, category filtering, price sorting and pagination.
- Product image upload and deletion using local file storage.
- Redis-backed shopping baskets with a three-day expiry on updates.
- EF Core data access with SQL Server.
- DTO mapping with AutoMapper and API exploration with Swagger in Development.

## Repository scope

This repository currently contains the ASP.NET Core backend. Angular source is not included in the checked repository tree, despite the repository name. The API allows the local frontend origin `http://localhost:4200` through its CORS policy.

## Project structure

```text
Ecom.API/              Controllers, mapping, middleware and API configuration
Ecom.Core/             Entities, DTOs, interfaces and service contracts
Ecom.Infrastructure/   EF Core context, migrations, repositories and services
Ecom.sln               Solution file
```

## Stack

C# · .NET 8 · ASP.NET Core · Entity Framework Core · SQL Server · Redis · AutoMapper · Swagger

## Local development

The instructions below are based on the checked source. They have not yet been validated by a clean build and local run.

### Requirements

- .NET 8 SDK.
- A reachable SQL Server instance.
- A reachable Redis instance.
- EF Core CLI tooling compatible with the project's EF Core 8 packages, if you need to apply migrations.

### 1. Get the source

```bash
git clone https://github.com/Fahmyhammad/Ecommerce-App.NET-8-Angular.git
cd Ecommerce-App.NET-8-Angular
dotnet restore Ecom.sln
```

### 2. Configure local services

The application reads these configuration keys:

| Key | Purpose |
| --- | --- |
| `ConnectionStrings:EcomDB` | SQL Server connection string |
| `ConnectionStrings:redis` | Redis connection string |

Set them for your own environment. For example, in a Bash-compatible terminal, environment variables use the names `ConnectionStrings__EcomDB` and `ConnectionStrings__redis`.

Do not commit real connection credentials or email passwords. Keep them in local secrets or environment variables. Email-service code is also present; email delivery is not demonstrated by the catalog and basket endpoints listed here.

### 3. Apply the database migrations

With a compatible `dotnet-ef` tool installed:

```bash
dotnet ef database update --project Ecom.Infrastructure --startup-project Ecom.API
```

### 4. Start the API

```bash
dotnet run --project Ecom.API --launch-profile http
```

The checked HTTP launch profile uses `http://localhost:5242`. Swagger is available at `/swagger` in Development. The pipeline includes HTTPS redirection; use the HTTPS profile and a trusted development certificate if your local setup requires HTTPS.

## API examples

These requests use route names from the source and assume the API is running on the checked HTTP profile. They have not been executed as part of this documentation draft.

### Browse products

```http
GET http://localhost:5242/api/Products/get-all?pageNumber=1&pageSize=3
```

### Search and sort

```http
GET http://localhost:5242/api/Products/get-all?Search=laptop&categoryId=1&sort=PriceAce&pageNumber=1&pageSize=3
```

`PriceAce` sorts by ascending price and `PriceDce` by descending price. These are the exact values accepted by the current implementation. Category `1` is an example and must exist in your database.

The default page size is 3, with a default maximum of 6. The source also exposes `MaxPageSize` as a settable query-model property, so the default should not be read as an enforced hard limit.

### Other read endpoints

```http
GET /api/Products/get-by-Id/{id}
GET /api/Categories/get-all
GET /api/Categories/get-by-id/{id}
GET /api/Basket/get-basket-item/{id}
```

Explore create, update and delete request models in Swagger.

## Current scope and next steps

- No Angular application, automated test project or GitHub Actions workflow was found in the checked tree.
- This documentation does not claim a completed checkout, payment or deployed demo.
- A useful next step is a verified local setup followed by tests for product queries and basket behavior.

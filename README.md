# 🕐 Web API — Time Zone Service

A minimal ASP.NET Core 6 Web API for time zone management and date conversion, built as a technical assessment. Exposes three endpoints wrapping a `TimeManager` service that stores the current time zone in memory and converts dates between formats using the **IANA (Olson) time zone database** via NodaTime.

## 📋 Task Description

The original task required implementing a `TimeManager` class with three methods, wrapping it in a Web API, and publishing it to GitHub. The solution was evaluated on:
- Correctness of API behavior and correct HTTP verb selection
- Clean code, design patterns, and readability
- XML documentation on methods, no over-commenting
- Implementation speed

## ✨ Features

- 🌍 **Get current time** — returns the current date/time formatted for the active time zone
- 🔧 **Set time zone** — accepts an IANA (Olson) timezone string (e.g. `Asia/Yekaterinburg`) and updates the active zone
- 🔄 **Convert date** — parses a date string in one of three supported formats and converts it to the current time zone
- 📖 **Swagger UI** — interactive API documentation available in development mode
- 💉 **Dependency Injection** — `ITimeManager` interface registered as `Transient` in the DI container

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Language | C# |
| Framework | ASP.NET Core 6 Web API |
| Time Zone DB | NodaTime 3.1.6 (IANA/Olson → Windows TZ mapping) |
| Documentation | Swagger / OpenAPI (Swashbuckle) |
| DI | ASP.NET Core built-in DI container |

## 📡 API Reference

### `GET /api/Time`
Returns the current date and time in the active time zone.

**Response:** `"28.12.2022 15:42:10 +05:00"`

---

### `POST /api/Time`
Sets the active time zone using an IANA (Olson) timezone identifier.

| Parameter | Type | Example |
|---|---|---|
| `timeZone` | `string` (query) | `Asia/Yekaterinburg` |

**Response:** `true` if the time zone was changed, `false` if the identifier was not recognized.

---

### `POST /convert`
Parses a date string and converts it to the current time zone.

| Parameter | Type | Supported formats |
|---|---|---|
| `date` | `string` (query) | `dd/MM/yyyy HH-mm-ss` · `dd.MM.yyyy HH:mm:ss zzz` · `dd.MM.yyyy HH:mm` |

**Response:** Converted date as `"dd.MM.yyyy HH:mm:ss zzz"`, or an empty string if parsing fails.

---

## 🏗️ Architecture

```
Web API Janti/
├── Controllers/
│   └── TimeController.cs    # 3 endpoints: GET, POST (timezone), POST /convert
├── ITimeManager.cs          # Interface with XML-documented method contracts
├── TimeManager.cs           # Implementation: NodaTime IANA→Windows mapping,
│                            # multi-format parsing, static timezone state
└── Program.cs               # Minimal API setup, DI registration, Swagger config
```

**Key design choices:**

- `ITimeManager` interface decouples the controller from the implementation — the controller has zero business logic and is fully testable via mock injection.
- `TimeManager` uses **NodaTime's `TzdbDateTimeZoneSource`** to map IANA timezone IDs (e.g. `Europe/Moscow`) to Windows timezone IDs, bridging the gap between the cross-platform Olson database and .NET's `TimeZoneInfo`.
- `ConvertDate` accepts three date formats via `DateTime.ParseExact` with a format array, returning an empty string on any parse failure instead of throwing.
- `CurrentTimeZone` is a `static` field, so the timezone persists across requests for the lifetime of the application.

## 🚀 Getting Started

### Prerequisites

- .NET 6 SDK

### Run locally

```bash
git clone https://github.com/NackBard/Web-API-Janti.git
cd "Web-API-Janti/Web API Janti"
dotnet run
```

Open Swagger UI at: `https://localhost:{port}/swagger`

---

> Built with ❤️ using ASP.NET Core 6 and NodaTime

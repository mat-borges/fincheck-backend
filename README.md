# FinCheck – Personal Finance Tracker 💰

FinCheck is a personal finance tracker to manage accounts, categories, and transactions with ease.
Built with **.NET 9 + EF Core + SQL Server** (Backend) and **Next.js + TypeScript + Tailwind** (Frontend).

---

## 🚀 Features

- User authentication with JWT
- Accounts and balance tracking
- Transactions CRUD (income, expense, transfer)
- Categories (global + custom per user)
- Automatic balance updates
- Soft delete with global query filters
- Database migrations with EF Core

---

## 🛠️ Tech Stack

- **Backend:** .NET 9, Entity Framework Core, SQL Server
- **Frontend:** Next.js (TS, TailwindCSS, Redux Toolkit) _(WIP)_
- **Other:** Docker, JWT, BCrypt for password hashing

---

## 📂 Project Structure

```

fincheck-backend/
├── FinCheck.Api/ # Entry point (controllers, middlewares, startup)
├── FinCheck.Application/ # Services, DTOs, business logic
├── FinCheck.Domain/ # Entities, enums, base classes
├── FinCheck.Infrastructure # EF Core DbContext, Repositories, Seeders

```

---

## ⚡ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mat-borges/fincheck.git
cd fincheck/fincheck-backend
```

### 2. Configure environment

Create an `appsettings.Development.json` in `FinCheck.Api/`:

```json
{
	"ConnectionStrings": {
		"Default": "Server=localhost;Database=FinCheck;User Id=sa;Password=your_password;TrustServerCertificate=True"
	},
	"JwtSettings": {
		"Secret": "supersecretkey123",
		"Issuer": "FinCheck",
		"Audience": "FinCheckUsers",
		"ExpiryMinutes": 60
	}
}
```

### 3. Run migrations

```bash
dotnet ef database update --project FinCheck.Infrastructure --startup-project FinCheck.Api
```

### 4. Run the API

```bash
cd src/FinCheck.Api
dotnet run
```

API will be available at: [https://localhost:5001](https://localhost:5001)

---

## 🧪 Tests

Tests are under `FinCheck.Tests/` using **xUnit + Moq**.
Run tests with:

```bash
dotnet test
```

---

## 📖 API Endpoints (basic)

- `POST /api/auth/register` – Register user
- `POST /api/auth/login` – Login
- `GET /api/accounts` – List accounts
- `POST /api/accounts` – Create account
- `GET /api/transactions` – List transactions
- `POST /api/transactions` – Add transaction (updates balance)

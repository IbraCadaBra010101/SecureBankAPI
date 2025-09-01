# FastighetsApp - Real Estate Management System

A real estate management application built with .NET 8.0 backend API and React TypeScript frontend.

## Architecture

- **Backend**: .NET 8.0 Web API with Entity Framework Core
- **Frontend**: React 18 with TypeScript
- **Database**: SQL Server with Entity Framework migrations
- **Features**: Companies, Apartments, Expiring Contracts, Webhook Updates

## Quick Start

### Prerequisites

- .NET 8.0 SDK
- Node.js 16+
- SQL Server or SQL Server Express

### Installation

1. Clone the repository
2. Install dependencies:
   ```bash
   npm run install-all
   ```
3. Update connection string in `FastighetsApp/appsettings.json` if needed
4. Run the application:
   ```bash
   npm start
   ```

This will start:
- Backend API: `http://localhost:5019`
- Frontend: `http://localhost:3000`

## Project Structure

```
FastighetsApp/
├── FastighetsApp/           # .NET Backend API
│   ├── Controllers/         # API Controllers
│   ├── Models/             # Data Models
│   ├── Services/           # Business Logic
│   ├── Repository/         # Data Access
│   ├── Data/              # Database Context
│   └── Migrations/        # EF Core Migrations
├── frontend/               # React Frontend
│   ├── src/               # Source Code
│   ├── public/            # Static Files
│   └── package.json       # Frontend Dependencies
├── package.json            # Root Dependencies & Scripts
└── README.md              # This File
```

## Available Scripts

- `npm start` - Run both frontend and backend
- `npm run backend` - Run only the backend API
- `npm run frontend` - Run only the React frontend
- `npm run install-all` - Install all dependencies
- `npm run build` - Build the frontend for production
- `npm run test` - Run frontend tests

## API Endpoints

- `GET /api/apartments/companies` - Get all companies
- `GET /api/apartments/companies/{id}/apartments` - Get apartments for a company
- `GET /api/apartments/companies/{id}/contracts/expiring` - Get expiring contracts
- `POST /api/webhook/apartment-attribute` - Webhook for apartment updates

## Database Schema

### Companies
- CompanyId (Guid, Primary Key)
- Name (string)
- OrganizationNumber (string)
- Email (string)
- PhoneNumber (string)

### Apartments
- ApartmentId (Guid, Primary Key)
- CompanyId (Guid, Foreign Key)
- Address (string)
- Floor (int)
- Rooms (int)
- RentPerMonth (decimal)
- LeaseStartDate (DateTime)
- LeaseEndDate (DateTime)
- IsOccupied (bool)

## Development

### Backend Development
```bash
cd FastighetsApp
dotnet run
```

### Frontend Development
```bash
cd frontend
npm start
```

### Database Migrations
```bash
cd FastighetsApp
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Environment Variables

Create `appsettings.Development.json` for local development:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FastighetsDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

## License

This project is licensed under the MIT License.
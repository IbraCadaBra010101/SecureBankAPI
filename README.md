# FastighetsApp - Real Estate Management System

A modern real estate management application built with .NET 8.0 backend API and React TypeScript frontend.

## 🏗️ Architecture

- **Backend**: .NET 8.0 Web API with Entity Framework Core
- **Frontend**: React 18 with TypeScript
- **Database**: SQL Server with Entity Framework migrations
- **Features**: Companies, Apartments, Expiring Contracts, Webhook Updates

## 🚀 Quick Start

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 16+](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd FastighetsApp
```

### 2. Install Dependencies

```bash
npm run install-all
```

This will install both root dependencies and frontend dependencies.

### 3. Database Setup

#### Option A: Using SQL Server
1. Ensure SQL Server is running
2. Update connection string in `FastighetsApp/appsettings.json` if needed
3. Run migrations:
   ```bash
   cd FastighetsApp
   dotnet ef database update
   ```

#### Option B: Using SQLite (Development)
The project is configured to use SQLite by default for development.

### 4. Seed Sample Data

The application includes sample data that will be automatically loaded when you first run it. If you need to manually seed data:

```bash
cd FastighetsApp
dotnet run
```

Then visit: `https://localhost:5019/swagger/index.html` to see the API endpoints.

### 5. Run the Application

**Single Command to Run Both Frontend and Backend:**

```bash
npm start
```

This will start:
- **Backend API**: `http://localhost:5019`
- **Frontend**: `http://localhost:3000`

## 📁 Project Structure

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

## 🔧 Available Scripts

- `npm start` - Run both frontend and backend
- `npm run backend` - Run only the backend API
- `npm run frontend` - Run only the React frontend
- `npm run install-all` - Install all dependencies
- `npm run build` - Build the frontend for production
- `npm run test` - Run frontend tests

## 🌐 API Endpoints

- `GET /api/RealEstate/companies` - Get all companies
- `GET /api/RealEstate/companies/{id}/apartments` - Get apartments for a company
- `GET /api/RealEstate/companies/{id}/contracts/expiring` - Get expiring contracts
- `POST /api/RealEstate/webhook/apartment-update` - Webhook for apartment updates

## 🗄️ Database Schema

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

## 🚀 Development

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

## 🐛 Troubleshooting

### Port Already in Use
If you get "address already in use" errors:
```bash
# Kill existing processes
taskkill /f /im dotnet.exe
taskkill /f /im node.exe

# Or use different ports
dotnet run --urls "http://localhost:5000"
```

### Database Connection Issues
- Check if SQL Server is running
- Verify connection string in `appsettings.json`
- Ensure database exists and migrations are applied

### Frontend Build Issues
```bash
cd frontend
rm -rf node_modules package-lock.json
npm install
```

## 📝 Environment Variables

Create `appsettings.Development.json` for local development:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FastighetsDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Run tests
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

If you encounter any issues:
1. Check the troubleshooting section above
2. Review the API documentation at `/swagger/index.html`
3. Check the console logs for error details
4. Create an issue in the repository

---

**Happy Coding! 🎉**

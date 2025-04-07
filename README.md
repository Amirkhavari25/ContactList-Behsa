# 📇 ContactList API

A clean, modular, and testable ASP.NET Core 8 Web API designed for managing personal contacts.  
Built as a **practice project** for the **Behsa Company hiring task**, this application follows **Clean Architecture**, implements **CQRS** with **MediatR**, and leverages both **EF Core** and **Dapper** for data access.

---

## 📌 Project Structure

This solution is organized into 5 main layers:

```
/ContactList
│
├── 📁 Domain             → Entity models and core contracts
├── 📁 Application        → Business logic, CQRS (Commands & Queries), DTOs
├── 📁 Infrastructure     → Data access logic (Dapper + EF), implementation of contracts
├── 📁 Presentation       → API controllers, middleware, documentation
├── 📁 DependencyInjection→ Configuration for service registrations
```

---

## ⚙️ Technologies Used

- **.NET 8**
- **Clean Architecture**
- **CQRS with MediatR**
- **Dapper** for high-performance queries
- **EF Core** only for initial database migrations
- **AutoMapper**
- **JWT Authentication** (without ASP.NET Identity)
- **Swagger** for API documentation
- **Postman** (collection provided)
- **SQL Server**

---

## 🔐 Authentication

- The API uses **JWT Bearer Token Authentication**
- Tokens include `NameIdentifier` as `UserId`
- Only the authenticated user can manage their own contacts

---

## 🧪 API Documentation

### Swagger
Visit `/swagger` once the application is running to explore and test endpoints interactively.

### Postman
A full Postman collection is included at:

```
/Presentation/API-Documentation/ContactListAPI.postman_collection.json
```

You can import it into Postman to access ready-to-use requests for all endpoints.

---

## 🚀 Getting Started

### Prerequisites

- .NET SDK 8+
- SQL Server (Local or Remote)
- Visual Studio 2022+ or VS Code

### Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/contactlist-api.git
   cd contactlist-api
   ```

2. Configure your database connection:
   - Copy `appsettings.Development.json` settings into `appsettings.json`
   - Adjust the connection string as needed

3. Run the project:
   ```bash
   dotnet run
   ```

   ✅ The database will be automatically created via **EF Core** on first run.

---

## 📞 Features

- 🔐 Register/Login with JWT
- 👤 Each user has their own contacts
- ➕ Add, update, delete, and retrieve contacts (CRUD)
- 🧼 Soft-delete support with `IsDelete` flag
- 🧭 Swagger & Postman documentation included
- 🧠 Clean separation of concerns using Clean Architecture

---

## 🛠 Future Improvements (Optional Ideas)

- Pagination and Filtering
- Unit Testing (MediatR Handlers & Services)
- Role-based Authorization
- Docker support

---

## 👨‍💻 Author

**Amir hossein khavari**  
Built for the Behsa hiring task

---

## 📄 License

This project is licensed under the MIT License.

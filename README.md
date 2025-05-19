# 🎯 Activity Tracking Application (ASP.NET Core 8 + MVC + API)

This project is an Activity Tracking System that allows users to follow events, join activities, and share comments or photos. Built with a modern layered architecture, the application separates its API and MVC layers. It employs JWT for security, uses Cookie Authentication for safe token storage, and follows a robust, maintainable design.

💡 Project Motivation
This project was born out of a real-world need within my own social circle. A friend of mine expressed frustration that our group never organized events in a structured way. In response, I decided to develop a system that would bring consistency to our gatherings and activities. The friend who raised the concern now serves as a moderator within the application. This project combines technical implementation with a practical solution to improve real-life coordination and community engagement.
---

## 📁 Project Structure

* `App.Entities/` → Contains entity classes.
* `App.Dto/` → Contains Data Transfer Object (DTO) classes.
* `App.Repositories/` → Contains repository interfaces and implementations.
* `App.Services/` → Contains the business logic and service layer.
* `App.Api/` → RESTful Web API (secured endpoints with JWT).
* `App.Web/` → MVC layer (Razor Views, API consumption).

---

## 🔐 Authentication & Authorization

* **JWT Token Generation:** Handled in the API layer.
* **HttpOnly Cookie:** JWT tokens are securely stored in HttpOnly cookies in the MVC layer.
* **Role-based Authorization:** Implemented using `[Authorize]` and `[Authorize(Roles = "Admin,Moderator,Member")]`.

---

## 📸 Comment & Photo System

* Users can post comments and upload photos on event detail pages.
* Uploaded photos are stored in `wwwroot/uploads/comments` (MVC project).
* Comments are managed using ViewComponents:

  * `CommentsViewComponent`: Lists comments.
  * `CreateCommentViewComponent`: Displays comment form.
* Comments are submitted from MVC and sent to the API via `multipart/form-data` with JWT in the request header.
* Only authenticated users can post comments.

---

## 🗕️ Event Features

* **Event Listing:** Shows current week's and upcoming events.
* **Participation Check:** Confirms if a user is already attending an event (via userId & eventId API query).
* **Event Details:** Displays full event data including user comments and participation button.

---

## ⚙️ Technologies Used

* **ASP.NET Core 8**
* **Entity Framework Core** (PostgreSQL)
* **JWT & Cookie Authentication**
* **AutoMapper**
* **FluentValidation**
* **Razor Views + ViewComponents**
* **RESTful API with HttpClient**
* **File Upload Handling**

---

## 🛠️ Setup & Run

### 1. Database Configuration

Update `appsettings.json` with your PostgreSQL connection string. Then run:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 2. Launching the App

Make sure both `App.Api` and `App.Web` are set as startup projects in your solution and run them simultaneously.

### 3. JWT & Cookie Setup

Ensure correct JWT + Cookie middleware configuration exists in `Program.cs`. Set HttpOnly = true for token cookies.

---


## 🔗 Contact

For questions or feedback, please contact: `ilhanrandakk@gmail.com`
![Image](https://github.com/user-attachments/assets/67ac12ef-831f-4370-8b45-3b642febab6e)
![Image](https://github.com/user-attachments/assets/61380249-0ab4-4a4c-b45c-d3627f8bd6ea)
![Image](https://github.com/user-attachments/assets/86a3b9a1-f85a-4214-a904-f43f04b541b2)
![Image](https://github.com/user-attachments/assets/76f55d0e-ff3e-45f9-ba9d-b3d9ce52e09c)
![Image](https://github.com/user-attachments/assets/9e5035e4-d0bf-4407-aa4a-bae595899a4f)
![Image](https://github.com/user-attachments/assets/4e4a6324-cf27-41d3-85d0-49971fbce0fb)
![Image](https://github.com/user-attachments/assets/60ce66d9-e168-4747-8951-5919c4fc1d6e)
![Image](https://github.com/user-attachments/assets/df1d3b6d-d98a-48bb-a59f-24df83a90428)

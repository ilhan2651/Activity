# 🎯 Activity Tracking Application (ASP.NET Core 8 + MVC + API)

This project is an Activity Tracking System that allows users to follow events, join activities, and share comments or photos. Built with a modern layered architecture, the application separates its API and MVC layers. It employs JWT for security, uses Cookie Authentication for safe token storage, and follows a robust, maintainable design.

---

## 📁 Project Structure

App.Entities/ -> Contains entity classes.
App.Dto/ -> Contains Data Transfer Object (DTO) classes. 
App.Repositories/ -> Contains repository interfaces and implementations.
App.Services/ -> Contains the business logic and service layer.
App.Api/ -> RESTful Web API (secured endpoints with JWT).
App.Web/ -> MVC layer (Razor Views, API consumption).
---

## 🔐 Authentication & Authorization

- **JWT Token Generation:** The API layer generates JWT tokens.
- **HttpOnly Cookie:** JWT tokens are securely stored in HttpOnly cookies on the MVC side.
- **Role-based Authorization:** Implemented using `[Authorize]` and `[Authorize(Roles = "Admin,Moderator,Member")]` filters.

---

## 📸 Comment & Photo System

- Users can post comments and upload photos on the event detail page.
- Uploaded photos are stored in the `wwwroot/uploads/comments` directory.
- Comments are managed via dedicated ViewComponents:
  - **CommentsViewComponent:** Displays the list of comments.
  - **CreateCommentViewComponent:** Handles comment creation.
- The comment API endpoint is secured using JWT authentication.

---

## 📅 Event Features

- **Event Listing:** Displays weekly and upcoming events.
- **Participation Check:** Determines if a user is already participating in an event based on the user ID and event ID retrieved from the API.
- **Event Details:** Shows event information along with user comments and participation status.

---

## ⚙️ Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core** (with PostgreSQL)
- **JWT & Cookie Authentication**
- **AutoMapper**
- **FluentValidation**
- **Razor Views (MVC) & ViewComponents**
- **REST API** (consumed via HttpClient)
- **File Upload Handling**

---

## 🛠️ Setup & Run

1. **Database Configuration:**
   - Update the connection string in `appsettings.json` with your PostgreSQL credentials.
   - Apply migrations using the following commands:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

2. **Running the Application:**
   - Launch both the `App.Api` and `App.Web` projects together within the same solution.

3. **JWT Settings:**
   - Verify the JWT Bearer and Cookie Authentication settings in `Program.cs` or the appropriate configuration files.

---

## 💬 Developer

This project was developed by [Your Name]. The project emphasizes accessibility and an improved user experience.

---

## 📄 License

Include your project's license details here (e.g., MIT License).

---

## 🔗 Contact

For any questions or feedback regarding this project, please contact [your email address].

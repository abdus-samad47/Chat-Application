
Real-Time Chat Application
This project is a real-time chat application built with ASP.NET Core, React, and SQL Server. It enables secure, interactive, and scalable chat functionalities using JWT for authorization, SignalR for real-time updates, and Serilog for logging.

Features
User Registration and Login
Real-Time Messaging with SignalR
Group Chats
Add Users to Groups
User Credential Management
Logging with Serilog for better tracking and debugging

Technologies Used
ASP.NET Core (.NET 6) - Backend framework
React - Frontend library
SQL Server - Database
JWT - For secure authorization
SignalR - Enables real-time updates
Serilog - Logging library

Project Structure
├── Backend
│   ├── Controllers
│   ├── Services
│   ├── Models
│   └── SignalR Hub
├── Frontend
│   ├── Components
│   ├── Pages
│   └── Services
├── Database
│   └── SQL Server
└── Logs
    └── SeriLog

Setup Instructions
1. Clone the repository:
git clone https://github.com/your-username/real-time-chat-app.git
cd real-time-chat-app

2. Backend Setup:
Install .NET 6 SDK if not already installed.
Configure the SQL Server database connection in appsettings.json.
Run the following command to build and start the server:
dotnet build
dotnet run

3. Frontend Setup:
Navigate to the frontend folder.
Install dependencies:
npm install
Start the React application:
npm start

4. Database:
Set up the SQL Server database and apply necessary migrations.

5. Logging:
Serilog is configured in the backend to capture logs for various operations, including errors and user actions.

Detailed Functionality
. User Authentication
JWT is used to securely handle user authentication. Users can register, login, and their sessions are authenticated with JWT tokens.
. Real-Time Messaging
SignalR enables real-time, bidirectional communication. Users can send and receive messages instantly without refreshing the page.
. Group Chat
Users can create chat groups, add other users, and communicate within those groups, allowing for private group conversations.
. User Profile Management
Users can update their credentials, ensuring their profiles remain up to date.
. Logging
Serilog provides structured logging to capture and monitor various application events, including login attempts, message exchanges, and group creation activities.

License
This project is licensed under the MIT License - see the LICENSE file for details.

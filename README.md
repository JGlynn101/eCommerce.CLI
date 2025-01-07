# eCommerce API Project

## Description
This project is a C# .NET API for an eCommerce platform, designed to handle basic operations like managing items (ToDos and Appointments) and storing them persistently using a file-based storage system (`Filebase.cs`). The application uses `Newtonsoft.Json` to serialize and deserialize data, ensuring that the state of the application is maintained even when the project is stopped and restarted.

## Features
- **CRUD Operations** for managing `ToDos` and `Appointments`.
- Persistent storage via `Filebase`, saving data to a local directory (`C:\temp`) in JSON format.
- Lightweight and easy to set up—ideal for learning and small-scale applications.

## Requirements
- **Visual Studio** (2019 or later recommended)
- **.NET Framework** (version 4.7.2 or later) or **.NET Core** (3.1 or later)
- **Newtonsoft.Json** NuGet package

## Setup Instructions

1. **Clone the Repository**
2. **Open the Project in Visual Studio**
- Launch Visual Studio.
- Open the solution file (`ecommerce-api.sln`).

3. **Restore NuGet Packages**
- In Visual Studio, go to `Tools > NuGet Package Manager > Manage NuGet Packages for Solution`.
- Restore any missing packages (e.g., `Newtonsoft.Json`).

4. **Run the Application**
- Press `F5` or click the green "Run" button in Visual Studio.
- The application will launch, and its persistent storage will default to `C:\temp`.

## Filebase Storage Details
The `Filebase` class provides a simple and effective way to persist application state:
- All `ToDos` are saved in `C:\temp\ToDos` as JSON files.
- All `Appointments` are saved in `C:\temp\Appointments` as JSON files.
- Each item is uniquely identified by a `GUID`.

## API Endpoints
### ToDo Operations
- **Add or Update ToDo**
- Automatically assigns an ID if none exists.
- **Get All ToDos**
- Retrieves a list of all `ToDos`.
- **Delete ToDo**
- Deletes a specified `ToDo` by ID.

### Appointment Operations
- **Add or Update Appointment**
- Automatically assigns an ID if none exists.
- **Get All Appointments**
- Retrieves a list of all `Appointments`.
- **Delete Appointment**
- Deletes a specified `Appointment` by ID.

## Project Structure
## Customization
You can modify the `Filebase.cs` file to:
- Change the storage root directory (`_root`).
- Implement the `Delete` method for enhanced functionality.

## How to Use
1. Download the repository and open it in Visual Studio.
2. Run the project and use your preferred API testing tool (e.g., Postman, curl) to interact with the endpoints.
3. Add, retrieve, and manage `ToDos` and `Appointments`. The data will persist in the `C:\temp` directory even after the application stops.

## Dependencies
- [Newtonsoft.Json](https://www.newtonsoft.com/json): For JSON serialization and deserialization.


---



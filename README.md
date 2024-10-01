
### Overview

This project is Web API based a banking application built using ASP.NET Core.

It includes functionalities for managing bank accounts, handling direct debits, and calculating interest for savings accounts. 
The project is structured into multiple layers to separate concerns and improve maintainability.

### Project

1. **RamaBankProj**:
   - **Controllers**:
     - `AccountController.cs`: Manages general account operations such as retrieving and freezing accounts.
     - `CurrentAccountController.cs`: Handles operations specific to current accounts, such as scheduling direct debits.
     - `SavingsAccountController.cs`: Manages operations specific to savings accounts, such as calculating interest.
   - **Dto**: Contains Data Transfer Objects used for transferring data between layers.
   - **Enums**: Defines enumerations used throughout the application.
   - **Model**: Contains the data models representing the entities in the application.
   - **Services**: Implements the business logic and interacts with the data layer.
   - **Shared**: Contains shared utilities and helpers.
   - **Program.cs**: Configures and runs the application.
   - **MappingProfile.cs**: Configures AutoMapper mappings.

2. **RamaBankProj.Tests**:
   - Contains unit tests for the controllers to ensure the correctness of the application logic.
   - `AccountControllerTests.cs`: Tests for `AccountController`.
   - `CurrentAccountControllerTests.cs`: Tests for `CurrentAccountController`.
   - `SavingsAccountControllerTests.cs`: Tests for `SavingsAccountController`.

### Key Functionalities

1. **Account Management**:
   - Create account
   - Retrieve account details.
   - Freeze accounts.
     
2. **Account Types**
   - Current Account
   - Savings Account

4. **Current Account Operations**:
   - Schedule direct debits.

5. **Savings Account Operations**:
   - Calculate interest.

### Steps to add New account type

1. Update the AccountType enum
2. Update the Account Model
3. Create new Service Interface & implementation
4. Register the new service
5. Create new controller
6. Write some Unit tests

### Dependencies

- **Entity Framework Core**: Used for data access and management.
- **AutoMapper**: Used for object-to-object mapping.
- **Swashbuckle.AspNetCore**: Used for generating Swagger documentation.
- **XUnit**: Used for unit testing.
- **Moq**: Used for mocking dependencies in unit tests.

### Configuration

- **appsettings.json**: Contains configuration settings for the application.
- **appsettings.Development.json**: Contains development-specific configuration settings.

### Running the Application

To run the application, use the following command:

```sh
dotnet run --project RamaBankProj

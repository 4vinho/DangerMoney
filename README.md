# Danger Money

## English

Danger Money is a robust financial management application designed to help retail businesses track and analyze their expenses efficiently. It provides tools for categorizing expenditures, visualizing spending trends, and managing user access.

## Portuguese (Português)

Danger Money é uma aplicação robusta de gestão financeira projetada para ajudar empresas de varejo a rastrear e analisar suas despesas de forma eficiente. Ela oferece ferramentas para categorizar gastos, visualizar tendências de despesas e gerenciar o acesso de usuários.

## Features

*   **Expense Tracking:** Record and categorize your daily expenses.
*   **Monthly Overview:** View a summary of your expenses for the current month.
*   **User Authentication:** Securely register, log in, and manage your account.
*   **Data Persistence:** All data is stored locally using SQLite.
*   **Responsive UI:** User interface designed to work well on various screen sizes.

## Technologies Used

*   **Backend:** ASP.NET Core 9.0 (C#)
*   **Database:** SQLite (Entity Framework Core)
*   **Frontend:** Razor Pages, HTML, CSS, JavaScript (Bootstrap 5)

## Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

*   .NET SDK 9.0 or later (Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download))
*   A code editor (e.g., Visual Studio, Visual Studio Code)

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/your-username/DangerMoney.git
    cd DangerMoney
    ```
    *(Note: Replace `https://github.com/your-username/DangerMoney.git` with the actual repository URL if it's hosted on GitHub or another platform.)*

2.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```

3.  **Apply Database Migrations:**
    This project uses SQLite as its database. The database files (`Identity.db` and `Repository.db`) will be created in the project's root directory when you run the migrations.

    First, ensure you have the `dotnet-ef` tool installed globally:
    ```bash
    dotnet tool install --global dotnet-ef
    ```
    If you already have it, you can update it:
    ```bash
    dotnet tool update --global dotnet-ef
    ```

    Now, apply the migrations for both Identity and Repository contexts:
    ```bash
    dotnet ef database update -c IdentityDbContext
    dotnet ef database update -c RepositoryDbContext
    ```

### Running the Application

To run the application from the command line:

```bash
dotnet run
```

The application will typically run on `https://localhost:7000` or a similar port. Check the console output for the exact URL.

### Manual Database Deletion (if needed)

If you encounter issues with the database and need to start fresh, you can manually delete the `Migrations` folder and the `.db` files (`Identity.db`, `Repository.db`) from the project's root directory. After deletion, re-run the migration steps.

## Usage

1.  **Register:** Create a new user account.
2.  **Login:** Log in with your registered credentials.
3.  **Add Expenses:** Navigate to the "Gastos" section to add new expenses.
4.  **View Expenses:** See your monthly expenses overview.
5.  **Edit/Delete Expenses:** Modify or remove existing expense entries.

## Contributing

(Optional: Add guidelines for contributions if this were an open-source project)

## License

(Optional: Add license information)
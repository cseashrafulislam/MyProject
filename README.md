# QTecTask

This repository contains the QTecTask project, which is primarily developed in C#. The project is designed to handle various tasks related to employee management, including querying employee data based on specific criteria.

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Introduction

QTecTask is designed to manage employee data efficiently. It includes functionalities to search for employees based on various criteria such as name, department, position, and performance score.

## Installation

To install and run the QTecTask project locally, follow these steps:

1. **Clone the repository:**

    ```sh
    git clone https://github.com/mdtuhinislam/QTecTask.git
    ```

2. **Navigate to the project directory:**

    ```sh
    cd QTecTask
    ```

3. **Install the required dependencies:**

    Ensure you have .NET SDK installed. Then, restore the dependencies:

    ```sh
    dotnet restore
    ```

4. **Build the project:**

    ```sh
    dotnet build
    ```
5. **Build the project:**

    select project QTecTask.Database and Run update-database
   [N.B] Make sure you have an MS SQL Server Database and it is accessible via Windows Authentication mode or you can configure the appsettings.json of QTecTask MVC Project (Presentation).

5. **Run the project:**

    ```sh
    dotnet run
    ```

## Usage

### Querying Employees

The project provides an API to query employees based on various search criteria such as name, department, position, and score. The primary method used for this functionality is `GetEmployeeBySearchingCriteria2`.

Example usage:

```csharp
public async Task<IEnumerable<EmployeeQuery>> GetEmployeeBySearchingCriteria2(int pageSize, int pageNumber, string searchName, string searchDepartment, string searchPosition, decimal searchScore)
{
    // Implementation
}

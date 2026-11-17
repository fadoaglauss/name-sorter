# Name Sorter

A .NET console application developed as part of the Dye & Durham coding assessment.

The application reads names from a text file, sorts them alphabetically by last name and then by given names, prints the sorted names to the console, and writes the results to `sorted-names-list.txt`.

The solution emphasizes clean code, SOLID principles, separation of concerns, dependency injection, and automated unit testing.

## 1. Project Structure

The solution is organized into five independent projects, each with a clearly defined responsibility.

```text
NameSorter/
│
├── NameSorter.Application/
│   ├── INameSorterApplication.cs
│   └── NameSorterApplication.cs
│
├── NameSorter.Console/
│   └── Program.cs
│
├── NameSorter.Models/
│   └── Person.cs
│
├── NameSorter.Services/
│   ├── IIOService.cs
│   ├── ISorterService.cs
│   ├── IOService.cs
│   └── SorterService.cs
│
└── NameSorter.Tests/
    ├── IOServiceTests.cs
    ├── NameSorterApplicationTests.cs
    ├── PersonTests.cs
    └── SorterServiceTests.cs
```

### NameSorter.Console

**Program.cs**

The application entry point and dependency injection composition root.

Responsibilities:

- Registers services and their implementations.
- Configures dependency injection.
- Resolves the application instance.
- Starts the application using command-line arguments.

The entry point contains no sorting or file-processing business logic.

### NameSorter.Application

**INameSorterApplication.cs**

Defines the contract for executing the name-sorting workflow.

**NameSorterApplication.cs**

Coordinates the application's execution.

Responsibilities:

- Validates command-line arguments.
- Reads names using the injected I/O service.
- Invokes the injected sorting service.
- Writes the sorted results to the output file.
- Displays results in the console.
- Handles expected execution errors.

The application depends on service abstractions rather than concrete implementations, reducing coupling and improving testability.

### NameSorter.Models

**Person.cs**

Represents a person's name and encapsulates its structure and validation.

Responsibilities:

- Represents given names and last name.
- Supports one to three given names.
- Validates the name format.
- Provides a string representation of the full name.

The model centralizes name-related rules, preventing their duplication across other components.

### NameSorter.Services

**IIOService.cs**

Defines the contract for reading and writing names.

**IOService.cs**

Implements file operations.

Responsibilities:

- Reads names from the input file.
- Converts file content into person objects.
- Writes sorted names to the output file.
- Overwrites the output file when it already exists.

**ISorterService.cs**

Defines the contract for sorting a collection of people.

**SorterService.cs**

Implements the sorting logic.

Responsibilities:

- Sorts primarily by last name.
- Uses given names as a secondary sorting criterion.
- Uses LINQ for readable and maintainable sorting.
- Keeps sorting independent of file operations.

### NameSorter.Tests

Contains automated tests using xUnit, with Moq where appropriate.

| Test Class | Responsibility |
|---|---|
| `PersonTests` | Validates name parsing and model behavior |
| `SorterServiceTests` | Verifies sorting correctness and edge cases |
| `IOServiceTests` | Tests file reading, writing, and error scenarios |
| `NameSorterApplicationTests` | Verifies application orchestration using mocked dependencies |

## 2. Requirements

- .NET SDK compatible with the project's target framework.
- Git to clone the repository.
- No external databases or services.

## 3. How to Run

### Clone the repository

```bash
git clone <repository-url>
cd NameSorter
```

Replace `<repository-url>` with the repository URL.

### Build the solution

```bash
dotnet build
```

### Run the application

From the solution directory:

```bash
dotnet run --project NameSorter.Console -- ./unsorted-names-list.txt
```

The input file must contain one name per line.

Example input:

```text
Janet Parsons
Vaughn Lewis
Adonis Julius Archer
Shelby Nathan Yoder
Marin Alvarez
London Lindsey
Beau Tristan Bentley
Leo Gardner
Hunter Uriah Mathew Clarke
Mikayla Lopez
Frankie Conner Ritter
```

Expected output:

```text
Marin Alvarez
Adonis Julius Archer
Beau Tristan Bentley
Hunter Uriah Mathew Clarke
Leo Gardner
Vaughn Lewis
London Lindsey
Mikayla Lopez
Janet Parsons
Frankie Conner Ritter
Shelby Nathan Yoder
```

The application prints the sorted names and creates or overwrites `sorted-names-list.txt` in the current working directory.

### Error Handling

The application is designed to handle expected errors, including missing arguments, invalid names, missing files, and file access failures.

Successful execution returns exit code `0`, while handled failures return a nonzero exit code.

## 4. Running Tests

The project uses:

- **xUnit:** Unit testing framework.
- **Moq:** Mocking dependencies to isolate application behavior.

Run all tests:

```bash
dotnet test
```

Run with detailed output:

```bash
dotnet test --verbosity normal
```

Run only the application tests:

```bash
dotnet test --filter FullyQualifiedName~NameSorterApplicationTests
```

### Testing Strategy

Tests are organized by component to validate behavior independently.

**Model tests** verify name validation, parsing, and representation.

**Sorting tests** verify alphabetical ordering, multiple given names, duplicates, empty collections, and large datasets.

**I/O tests** verify reading files, creating output files, overwriting existing content, and handling missing files.

**Application tests** use mocked dependencies to verify the correct execution sequence and error handling without relying on real file operations.

The test design emphasizes repeatability, isolation, and clear assertions.

## 5. Software Design and SOLID Principles

The architecture separates application orchestration, domain representation, infrastructure operations, and application startup.

### Single Responsibility Principle (SRP)

Each class has a specific responsibility and a clear reason to change.

| Component | Responsibility |
|---|---|
| `Person` | Name representation and validation |
| `SorterService` | Sorting rules |
| `IOService` | File operations |
| `NameSorterApplication` | Application workflow |
| `Program` | Dependency configuration and startup |

For example, modifying the sorting rules does not require changing file operations or the application entry point.

This separation improves readability, maintainability, and testability.

### Open/Closed Principle (OCP)

The application uses interfaces to allow alternative service implementations without modifying the application workflow.

For example, another implementation of `ISorterService` could introduce a different sorting strategy while preserving the existing application contract.

New behavior can be introduced through additional implementations rather than changes to the orchestration logic.

### Liskov Substitution Principle (LSP)

Service implementations are expected to respect the contracts defined by their interfaces.

For example, `IOService` can be replaced by another implementation of `IIOService`, provided it preserves the expected reading and writing behavior.

This supports substitutability without requiring the application to understand implementation details.

### Interface Segregation Principle (ISP)

The solution defines focused interfaces for distinct responsibilities.

- `IIOService` exposes file-related operations.
- `ISorterService` exposes sorting functionality.
- `INameSorterApplication` defines application execution behavior.

Consumers depend only on the operations relevant to their responsibilities, rather than a single interface containing unrelated methods.

### Dependency Inversion Principle (DIP)

High-level application logic depends on abstractions rather than concrete implementations.

`NameSorterApplication` receives its services through constructor injection.

Concrete implementations are registered in `Program.cs`, keeping dependency configuration separate from application behavior.

This design reduces coupling and enables isolated unit testing using mocks.

## 6. Code Quality Decisions

### Clear Separation of Concerns

The five-project structure provides explicit boundaries between application startup, orchestration, domain models, services, and tests.

Each project has a specific purpose, making the solution easier to navigate and maintain.

### Readability

The implementation prioritizes descriptive names, focused methods, and straightforward control flow.

Business logic is separated from application configuration, reducing the amount of code a developer needs to understand when making a change.

### Built-in Sorting

The application uses LINQ sorting operations instead of implementing a custom sorting algorithm.

This reduces unnecessary complexity and keeps the implementation focused on correctness, readability, and maintainability.

### Dependency Injection

Constructor injection makes dependencies explicit and enables implementations to be replaced during testing.

It also avoids directly constructing concrete services inside the application workflow.

### Error Handling

Expected execution errors are handled at the application boundary.

The application communicates failures through error messages and exit codes rather than silently ignoring exceptions.

### Automated Testing

Tests are separated from production code and focus on observable behavior.

Moq allows the application workflow to be tested independently.
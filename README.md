# Acme.School

## Overview

This is a .NET 8 PoC project to demonstrate important development concepts like Clean Architecture, Low Coupling, Abstractions, Clean Code, Maintainability and Testability of the code, among others.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Important notes of the project

**Core folder**

For the Application layer I have used Mediator pattern with the MediatR library, so every use case is well organized in commands/queries, I didn't wanted to overkill the project with the full CQRS pattern on data access side, but normally this goes with proper CQRS pattern with CommandRepositories and QueryRepositories.

The abstractions lives in the Domain layer (Repositories and external services)

**Infrastructure**

It's not fully implemented, it's just to demonstrate that the actual implementation details will live there so the Core knows nothing about it's details and is not coupled to any technology such as EntityFramework, Dapper, etc

**Key technologies and libraries used**
- MediatR (for Mediator pattern)
- Moq (for Mocking support)
- XUnit (for testing)
- FluentAssertions (for more readable assertions)
- AutoMocker (to handle dependency injection in tests)

**What things would you have liked to do but didn't do**
- AutoMapper, so we delegate mapping code in a separate pleace and keep the code cleaner and more readable.
- Refactor the validations that lives in the domain entities, so all validations results are saved in a ValidationResult object (today it will throw an exception with the first validation that fails)
- Summary Documentation for all public elements (classes, methods, etc)

**What things did you do but you think could be improved or would be 
necessary to return to them if the project goes ahead**
- Probably the way validations and verifications are handled, to not use a lot of custom exceptions, and we use a Result-oriented approach with the list of errors

**Time invested**
I have invested 2 days to do this project (about 12hs)

**What things you have had to research and what things were new to you**
During this days I have researched about the Result pattern which I think it is a better choice to handle errors instead of having many custom exceptions.
The rest of concepts/libraries are not new to me, it's how I like to organize things (except for the Mediator pattern, I know it and I used personally but not professionally)

# MagicVilla web API

## Description

This project is an basic API for managing villas in a tourist resort.

## Technologies used

- Docker
- C#
- ASP.NET Core Open API
- AutoMapper: For object-object mapping.
- Entity Framework Core: For data access and manipulation.
- Newtonsoft.Json: For JSON serialization and deserialization.
- Swagger/OpenAPI: For API documentation.
- Microsoft SQL Server: For database management.

## Project structure

The project follows the Repository pattern and is organized as follows:

- `Controllers`: Contains the API controllers responsible for handling incoming HTTP requests and returning appropriate responses.
- `Data`: Houses the logic for accessing and manipulating data, including interactions with the database using Entity Framework Core.
- `Models`: Holds the model classes representing entities in the application domain, such as villas and their attributes.
- `DTO`: Contains Data Transfer Objects (DTOs) used for mapping between API requests and model classes, facilitating communication between the client and server.
- `Repository`: Implements the Repository pattern, providing interfaces and classes for data access. This directory encapsulates the logic for querying and persisting data, promoting a separation of concerns and enhancing maintainability and scalability.

This structure ensures a clean and organized codebase, following best practices and promoting modularity and reusability.

Thanks ;)

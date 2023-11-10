
#  Vakıfbank Final Case

## Dealer Management

This project is a .NET Web API application utilizing essential libraries such as FluentValidation, Dapper, AutoMapper, Entity Framework, CQRS (Command Query Responsibility Segregation), and MediatR.

## Started Project

- Clone Project
  
`git clone https://github.com/aylinyildiz/Vakifbank-Final-Case.git`
- Create Database With Code First
  
`Add-Migration InitialCreate`

`Update-Database`


## Project Details

### Used Libraries and Technologies
- FluentValidation: Employed for data validation, FluentValidation ensures the accuracy of input data.

- Dapper: An efficient ORM (Object-Relational Mapping) tool used for database operations, providing speed and performance.

- AutoMapper: Utilized for object mapping, AutoMapper transforms data from the database into appropriate objects.

- Entity Framework: Used for database operations, Entity Framework facilitates database connections and queries.

- CQRS (Command Query Responsibility Segregation): The project follows the CQRS principle, managing command and query operations in separate classes.

- MediatR: MediatR, a library used in the CQRS pattern, is employed to manage command and query operations.

- MSSQL

- C#

- Jwt Token


### Endpoints

#### Account

 - `GET /dealer/api/v1/Accounts`

An endpoint protected with Authorize(Roles = "Admin") retrieves all accounts.
- `GET /dealer/api/v1/Accounts/{id}`

An endpoint protected with Authorize(Roles = "Admin") retrieves a specific account by ID.
- `GET /dealer/api/v1/Accounts/ByUserId/{UserId}`

An endpoint protected with Authorize(Roles = "Admin") retrieves accounts belonging to a specific user.
- ` POST /dealer/api/v1/Accounts `

An endpoint protected with Authorize(Roles = "Admin") creates a new account.
- ` PUT /dealer/api/v1/Accounts/{id} `

An endpoint protected with Authorize(Roles = "Admin") updates a specific account.
- `DELETE /dealer/api/v1/Accounts/{id}`

An endpoint protected with Authorize(Roles = "Admin") deletes a specific account.

#### Address 

 - `GET /dealer/api/v1/Addresses`

- `GET /dealer/api/v1/Addresses/{id}`

- `GET /dealer/api/v1/Addresses/ByUserId/{UserId}`

- ` POST /dealer/api/v1/Addresses`

- `PUT /dealer/api/v1/Addresses/{id} `

- `DELETE /dealer/api/v1/Addresses/{id}`

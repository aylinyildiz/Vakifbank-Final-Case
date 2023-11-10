
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

#### Card
- `GET /dealer/api/v1/Cards `
- `GET /dealer/api/v1/Cards/{id} `
- `GET /dealer/api/v1/Cards/ByUserId/{Userid} `
- `GET /dealer/api/v1/Cards/ByAccountId/{accountid} `
- `POST /dealer/api/v1/Cards `
- `PUT /dealer/api/v1/Cards/{id} `
- `DELETE /dealer/api/v1/Cards/{id} `


#### Order


- `GET /dealer/api/v1/Orders`
- `GET /dealer/api/v1/Orders/GetOrderReportsQuery`
- `GET /dealer/api/v1/Orders/{id}`
- `GET /dealer/api/v1/Orders/ByUserId/{Userid}`
- `POST /dealer/api/v1/Orders`
- `PUT /dealer/api/v1/Orders/UpdateOrderStatus/{id}`
- `PUT /dealer/api/v1/Orders/UpdateOrderByDealer/{id}`
- `PUT /dealer/api/v1/Orders/{id}`
- `DELETE /dealer/api/v1/Orders/{id}`

#### Product


- `GET /dealer/api/v1/Products`
- `GET /dealer/api/v1/Products/{id}`
- `GET /dealer/api/v1/Products/ByUserId/{Userid}`
- `POST /dealer/api/v1/Products`
- `PUT /dealer/api/v1/Products/{id}`
- `GET /dealer/api/v1/Products/GetProductStockReportQuery`
- `DELETE /dealer/api/v1/Products/{id}`
- `GET /dealer/api/v1/Products/GetProductsStockStatus`


#### Token

- `POST /dealer/api/v1/Token`
- `GET /dealer/api/v1/Token/Test`


#### User
- `GET /dealer/api/v1/Users `
-  `GET /dealer/api/v1/Users/{id} `
- ` POST /dealer/api/v1/Users `
- ` PUT /dealer/api/v1/Users/{id} `
- ` DELETE /dealer/api/v1/Users/{id} `

## Author

- Aylin Yıldız




# Fullstack Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

This is the final project of Integrify Academy which involves creating a Fullstack project with React and Redux in the frontend and ASP.NET Core 7 in the backend. The result is an indoor plant e-commerce site called Evergreen which features basic user functionalities (registering, authentication, browsing through products, shopping cart, ordering) as well as admin functionalities for managing users, products and orders.

- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP.NET Core, Entity Framework Core, PostgreSQL

You can follow the same topics as your backend project or choose the alternative one, between E-commerce and Library. You can reuse the previous frontend project, with necessary modification to fit your backend server.

## Table of Contents

1. [Technologies](#technologies)
2. [Functionalities](#functionalities)
3. [Frontend](#frontend)
4. [Backend](#backend)
   - [Design](#design)
   - [Deployment](#deployment)
   - [Running the project](#running-the-project)

## Technologies

- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP.NET Core, Entity Framework Core, PostgreSQL

## Functionalities

### User

1. User Management: a user is able to...
   - register for a user account (not admin account)
   - log in and log out
   - edit their name, email and avatar
   - change their password
   - unregister
2. Products: a user is able to...
   - view all available products
   - view a single product
   - search and sort products
3. Cart: a user is able to...
   - add products to a shopping cart
   - manage their shopping cart
   - checkout the shopping cart / place an order
4. Admin can see all order list
   user can create an order

### Admin

1. User Management: an admin is able to...
   - view users
   - delete users
   - edit user roles
   - (backend only: create new users and admins)
2. Product Management: an admin is able to...
   - view products in admin mode
   - add products
   - edit products
   - delete products
3. Order Management: an admin is able to...
   - view all orders
   
   - (backend only: view order details)
   -(backend only : update order status)
   -(backend only : user can cancel order)
   - (backend only: delete an order while it is processing)

## Frontend

The frontend code and documentation are found in [this repository](https://github.com/AbhishekSingh1909/fs16_CSharp-FullStack.git).

![Screenshot](readmeImages/frontpage.png)

## Backend

### General

- CLEAN architecture
- complies with Rest API
- EF core managed database
- error handling middleware
- authentication and authorization
- unit testing for service layer (xunit)
- documentation (README.md and Swagger)
- backend and database deployed on live servers

### Design

#### Database design





##### Repositories
   - BaseRepo
   - CategoryRepo
   - ImageRepo
   - OrderRepo
   - ProductRepo
   - UserRepo

##### Services
   - AuthService
   - BaseService
   - CategoryService
   - ImageService 
   - OrderService
   - ProductService
   - UserService

##### Controllers
   - AuthController
   - Base Controller
   - CategoryController
   - ImageController
   - OrderController
   - ProductController
   - UserController
   -AddressController

##### Middleware
   - authentication: token-based
   - authorization: role-based policy, id check from token
   - error handling

##### Database
   - code-first
   - seeded data with by program (Manually) in SeedData.cs 



### Deployment

[Link to backend deployment](https://fakestore.azurewebsites.net/)

[Link to frontend deployment](https://fstore-project.vercel.app/)

### Swagger Link
[Link to Swagger](https://fakestore.azurewebsites.net/swagger/index.html)

### Running the backend locally

#### Requirements
- [.NET](https://dotnet.microsoft.com/en-us/download)
   - [Entity Framework tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

#### Instructions
- clone the project
- create a file named appsettings.json in the fStore.WebAPI folder with the following content (change ***)
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MyStoreDb": "Host=***;Database=***;Username=***;Password=***;",
  },
  "Jwt": {
    "Issuer": ***,
    "Audience": ***,
    "Key": ***
  }
}

 #### 
```
- create seed data 
- initialize database at Evergreen.Webapi with `dotnet ef migrations add {MigrationName}` and `dotnet ef database update`
- run the project with `dotnet watch --project Evergreen.WebAPI`
- run tests with `dotnet test
### Admin Credentials
email - admin@mail.com
password -admin123
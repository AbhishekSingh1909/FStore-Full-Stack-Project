# Fullstack Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

This is the final project of Integrify Academy which involves creating a Fullstack project with React and Redux in the frontend and ASP.NET Core 8, Entity Framework Core and Postgre SQL  in the backend. This is an e-commerce website called fStore which has folowing features 
User functionalities :
(Registering, Authentication, Browsing through products, Shopping Cart, Order Creation)

Admin functionalities: 
User Management (View User , Update User Role, Delete User) 
Products Management (Create New Product, Update Product, Delete Product)
Order Management (View List of Oders)

## Table of Contents

1. [Technologies](#technologies)
2. [Functionalities](#functionalities)
3. [Frontend](#frontend)
4. [Backend](#backend)
   - [Design](#design)
   - [Deployment](#deployment)
   - [Running the project](#running-the-project)

## Technologies

- Frontend: SASS, TypeScript, React, Redux Toolkit,Material UI
- Backend: ASP.NET Core, Entity Framework Core, PostgreSQL

## File Structure
  - fStore
  - fStore.Business
  - fStore.Controller
  - fStore.Core
  - fStore.WebAPI
  - fStore.Test
  - fStore.sln
  - README.md

## Functionalities

### User

1. User Management: A user is able to...
   - register an account (not admin account)
   - log in and log out
   - edit their name, email and avatar
   - change their password
   - unregister themself
2. Products: a user is able to...
   - view all available products
   - view a single product
   - search and sort products
   - filter products by category
3. Cart: a user is able to...
   - add products into shopping cart
   - manage their shopping cart increase and decrease items  qauntities
   - checkout the shopping cart / place an order
4. Admin can see all order list

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
   
   - (backend Endpoints: view each order details)
   - (backend Endpoints : update order status)
   - (backend Endpoints : user can cancel order)
   - (backend Endpoints: delete an order)

## Frontend

The frontend code and documentation are found in [this repository](https://github.com/AbhishekSingh1909/fs16_CSharp-FullStack.git).


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
   - Auth Service
   - Base Service
   - Category Service
   - Image Service 
   - Order Service
   - Product Service
   - User Service
   - Address Service

##### Controllers
   - Auth Controller
   - Base Controller
   - Category Controller
   - Image Controller
   - Order Controller
   - Product Controller
   - User Controller
   -Address Controller

##### Middleware
   - authentication: token-based
   - authorization: role-based policy, id check from token
   - error handling

##### Database
   - code-first
   - seeded data with by program (Manually) in SeedData.cs 



### Deployment

[Link to backend deployment](https://fakestore.azurewebsites.net/swagger/index.html)

[Link to frontend deployment](https://fstore-project.vercel.app/)

### Swagger Link
[Link to Swagger](https://fakestore.azurewebsites.net/swagger/index.html)


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
- initialize database at fStore.WEBAPI with `dotnet ef migrations add {MigrationName}` and `dotnet ef database update`
- run the project with `dotnet watch --project fStore.WebAPI`
- run tests with `dotnet test
### Admin Credentials
email - admin@mail.com
password -admin123
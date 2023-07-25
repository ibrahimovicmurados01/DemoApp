# DemoApp project description

---

## Summary
> DemoApp project is supposed to serve as a .NET web application.
Overall the codebase consist of c# code and some shell script for deployment.
 Consider that the project is using .NET core framework which supports crossplatform. In this readme file we will cover aspects of the project including the database and infrastructure high level description

---

## Database structure
In the project the entity framework (ef core) as a orm is being used.
Here are main tables to highlight:

| Table | Description |
| ------ | ------ |
| `users` | table contains the users information (DemoApp system users).  
| `contracts` | table contains contact information. Consider that the person infos are also in the same table. |


##### Disclaimer:
* In the important tables where we want to keep track of the data, we have `tombstoned` column. It means that when we delete smth in the system, the row is beign deleted actually but just `tombstoned` column becomes true.
* In almost all tables we have also `created` and `modified` columns

---

## High-level code description

```
Used techs:
* dotnet core version 7.x
* entity framework core (to mssql)
* Moq for mocking in unit tests
* parameterized tests for generic CRUD usecases (functional tests). Unfortunately not everything is covered by tests.
* xunit for running unit tests
```

The code consist of **4 main projects** (`DemoApp.WEB/DemoApp.Contracts/DemoApp.Entities/DemoApp.Repositories`) and plus **2 test projects** (`DemoApp.Web.Tests/DemoApp.Repository.Tests`).

##### DemoApp.Repository
```
This project consist of implementation of Data access layer. Based on the explanatory name in DemoApp Repository pattern is being used. You can find some `decorators` for standard repository implementation. My advice is to go through these decorators and understand how they work.
```
##### DemoApp.Entities
```
This project consist of entities which is used with ef core in order to be synced with database. Basically this is database entities.
```
##### DemoApp.Contracts
```
I would say that this project were supposed to include the interfaces which are going to be used between DAO layer and Web layer, but it ended up only having 2 interfaces. They can be moved to Repository project.
```
##### DemoApp.Web
```
Web project is using the `MVC pattern` (check in the internet). GET/PUT/POST/DELETE Controller action are separated into different classes and generic implementations are provided for standard usecase.
The flow starts from the Cotrollers package and from there moves to Repositories pattern which is using the DAO and Mapper. Mappers are provided also in the project in order to separate the `Entities` and `Models`.

```

---


---

## High-level infrastructure description

Deployments: 
1. DemoApp.Web
*In this project the web application is deployed and running.*
You have to run below docker commands to create docker images and then you can create docker container to run application

   ```dockerfile
    docker build -t demoapp:v1 .
    docker run -d -p 5000:5000 --name app1 {imageId}
    ```


2. mssql-deployment
*The mssql is deployed in this deployment.*

---

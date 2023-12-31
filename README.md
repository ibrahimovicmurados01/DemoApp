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
* Serilog for logging
* Bootstrap 5
* Toaster 
```

The code consist of **5 main projects** (`DemoApp.WEB/DemoApp.Contracts/DemoApp.Entities/DemoApp.Repositories`) and plus **1 test projects** (`DemoApp.Web.Tests`).

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
##### [DemoApp.Web](https://demoappwebv2.azurewebsites.net/)
```
Web project is using the `MVC pattern` (check in the internet). GET/PUT/POST/DELETE Controller action are separated into different classes and generic implementations are provided for standard usecase.
The flow starts from the Cotrollers package and from there moves to Repositories pattern which is using the DAO and Mapper. Mappers are provided also in the project in order to separate the `Entities` and `Models`.
Also Web project is using Bootsrap 5 and toastr web framework for design 

```
##### DemoApp.ClassicUI
```
The ClassicUI project uses Classic ASP (Active Server Pages) technology. To achieve session data sharing between the ClassicUI project and another web project, we leverage cookies from the Web project.

Here's a step-by-step guide to enable session data sharing:
* Ensure your Web project sends cookies with the following names: "Session_UserName" and "Session_UserId" containing the appropriate values.
* In the Classic ASP project, we read these cookies and then store the values in session variables named "UserName" and "UserId."
 

```
 * reference [Code Project ](https://www.codeproject.com/Articles/30723/Handle-session-variable-problems-between-classic-A)
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
## Run Application
To access the DemoApp web application, click on the following URL: 
[demoappwebv2.azurewebsites.net](https://demoappwebv2.azurewebsites.net/)

**Registration:**
* Upon accessing the application, you can register as a new user by clicking on the "Register" link.
* Fill in the required registration details, such as your email address and password, and submit the form.
* Once registered, you will be redirected to the login page.

**Creating and Managing Contacts:**
* After logging in with your registered credentials, you will have access to the main features of the application.
* Navigate to the "Contacts" section, where you can view your existing contacts and perform CRUD operations.
* To create a new contact, click on the "Create New Contact" button and provide the necessary contact details.
* To delete a contact, open the contact details page and click the "Delete" button.
* To update a contact's information, click the "Edit" button on the contact details page, make changes, and save.
* To view the details of a contact, click on the contact's name or select the "Info" option.


**CRUD Operations:**

The application supports Create, Read, Update, and Delete (CRUD) operations for managing your contacts. You can easily add, view, update, and delete contacts as needed.
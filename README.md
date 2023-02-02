# Assignment 01 

# Building ASP.NET Web API with  Desktop Application/Web Application
Introduction

Imagine you're an employee of a product retailer named eStore. Your manager has asked you to develop a Desktop application/Web application for member management, product management, and order management. The application has a default account whose email is “admin@estore.com” and password is “admin@@” that stored in the appsettings.json.

The application has to support adding, viewing, modifying, and removing information - a standardized usage action verbs better known as Create, Read, Update, Delete (CRUD) and Search. This assignment explores creating an ASP.NET Core Web API with C#, and ADO.NET or Entity Framework Core, the client application can be used as Desktop Application (Windows Forms, WPF) or Web Application (ASP.NET Core Web MVC or Razor Pages). An MS SQL Server database will be created to persist the data and it will be used for reading and managing data.

Assignment Objectives

In this assignment, you will:

 ▪ Use the Visual Studio.NET to create a Desktop/Web application and ASP.NET Core Web API project.

 ▪ Perform CRUD actions using ADO.NET or Entity Framework Core.

 ▪ Use LINQ to query and sort data.
  
 ▪ Apply 3-layers architecture to develop the application.
  
 ▪ Apply Repository pattern and Singleton pattern in a project.
  
 ▪ Add CRUD and searching actions to the Desktop/Web application with ASP.NET Core Web API.
  
 ▪ Apply to validate data type for all fields. 
  
 ▪ Run the project and test the actions of the Web application.

Main Functions:

 ▪ Create Web API with Member management, Product management, and Order management: Read, Create, Update and Delete actions.

 ▪ Create Client application (with Desktop/Web application) interactive with WebAPI to perform these functions: 
  	
   o Search ProductName (keywork of ProductName) and UnitPrice
 
   o Create a report statistics sales by the period from StartDate to EndDate, and sort sales in descending order

   o Member authentication by Email and Password. If the user is “Admin” then allows to perform all actions, otherwise, the normal user is allowed to view/update the profile and view their orders history.

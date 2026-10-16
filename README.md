# COMP306-Lab3-LearningResourceSystem
an ASP.NET Core MVC web application that allows instructors to publish learning resources and students to browse, search, view, and comment on those resources.

# COMP306 Lab #3 — Learning Resource Portal

## Project Overview

The Learning Resource Portal is an ASP.NET Core MVC web application developed for the API Engineering COMP306 Lab #3 assignment.

The application allows students and instructors to register, log in, share learning resources, browse and search educational materials, upload files, and comment on learning resources.

## Team Members

- **Member 1:** Jasmin Cameron-Mcdonald
- **Member 2:** Samreet Kaur

## Technologies Used

- C#
- ASP.NET Core MVC
- Microsoft SQL Server
- Amazon RDS
- Amazon DynamoDB
- Amazon S3
- AWS Systems Manager Parameter Store
- AWS Elastic Beanstalk
- Git and GitHub
- Visual Studio 2022

## Application Features

- User registration and login
- Create, view, update, and delete learning resources
- Upload and manage learning-resource files
- Search resources by title, topic, and author/instructor
- Display the most recently created resources first
- Track resource views and display popular resources
- Add and view comments
- Edit comments within 24 hours, subject to comment ownership
- Secure database credential retrieval through AWS Systems Manager Parameter Store

## Database Design

### Amazon RDS — Microsoft SQL Server

The relational database contains the following tables:

- **Users:** User account and registration information
- **Categories:** Learning-resource categories
- **LearningResources:** Resource metadata, author, category, creation date, S3 object key, and view count

--- May change as project progesses

### Amazon DynamoDB

The `Comments` table stores comments associated with learning resources.

- Partition key: `ResourceID` (String)
- Sort key: `CommentID` (String)

Additional attributes include `UserID`, `CommentText`, and `Timestamp`.

### Amazon S3

Stores uploaded learning-resource files. The relational database stores the corresponding S3 object key rather than the file itself.

## Project Structure

```text
LearningResourceSystem/
├── Controllers/
├── Models/
├── Data/
├── Services/
├── Views/
├── wwwroot/
├── Database/
│   └── Lab3_Database.sql
├── Program.cs
├── appsettings.json
└── LearningResourcePortal.csproj
```

----- The exact structure may change as project progresses.

## Getting Started

### Prerequisites

- Visual Studio 2022
- A compatible .NET SDK
- Git
- Access to the required AWS resources

### Clone the Repository

```bash
git clone (https://github.com/Jasmin-mc/COMP306-Lab3-LearningResourceSystem.git)
cd COMP306-Lab3-LearningResourcePortal
```

### Open and Run the Application

1. Open `LearningResourcePortal.sln` in Visual Studio 2022.
2. Restore the required NuGet packages.
3. Configure the required AWS services and application settings.
4. Ensure database credentials are retrieved securely from AWS Systems Manager Parameter Store.
5. Build and run the application.

AWS configuration and deployment instructions will be completed as the project develops.

## Team Collaboration

The project is maintained in a shared GitHub repository.

- `main` contains stable, integrated code.
- Feature branches are used for individual tasks.
- Changes are shared through commits, pushes, pulls, and pull requests.
- Both team members participate in integration testing and the final demonstration.

## Deployment

The completed application will be deployed to AWS Elastic Beanstalk.

The demonstration video and final submission will follow the COMP306 Lab #3 requirements.

## Assignment Information

**Course:** API Engineering COMP306  
**Assignment:** Lab #3 — Develop a Cloud-Native Application  
**Due Date:** October 26, 2026, at midnight

This project is developed for educational purposes as part of the COMP306 coursework.


# Document Management Web API

This ASP.NET Core Web API project allows you to perform basic document management functions, including CRUD operations (Create, Read, Update, Delete) for documents. It provides endpoints to manage documents and serves as a simple example of a document management system.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Schemas](#schemas)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

Before you begin, ensure you have met the following requirements:

- Visual Studio, Visual Studio Code, or another compatible IDE
- Git (optional, if you want to clone the repository)

## Installation

1. Clone the repository:
   ```bash
   https://github.com/Mahmoud-ghb/DocumentManagement.git
2. Open the project in your preferred IDE.
3. Build and run the project.

## Usage
Once the project is up and running, you can interact with the API using the provided auto generated client by Swagger.
Here's how to use the CRUD methods:

API Endpoints:

Create (Upload) a Document

URL: /api/documents
HTTP Method: POST
Request Body: Form data with fields:
Name (string): Name of the document.
File (file): The document file to upload (accepted types: txt, docx, pdf).
______________________________________________________________________________
Read (Download) a Document

URL: /api/documents/{id}
HTTP Method: GET
Parameters:
{id} (int): The unique identifier of the document.
______________________________________________________________________________
Update a Document

URL: /api/documents/{id}
HTTP Method: PUT
Parameters:
{id} (int): The unique identifier of the document.
Request Body: JSON object with field:
Name (string): New name for the document.
_______________________________________________________________________________
Delete a Document

URL: /api/documents/{id}
HTTP Method: DELETE
Parameters:
{id} (int): The unique identifier of the document.

## Schemas
Document:
ID (int): Unique identifier for the document.
Name (string): Name of the document.
Content (byte[]): Binary content of the document.
FileType (string): Type of the document (e.g., "txt", "docx", "pdf").
CreationDate (DateTime): Date and time when the document was created.

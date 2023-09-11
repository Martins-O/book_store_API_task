# Online Bookstore Project

This project is an online bookstore system that provides users with the ability to browse books, 
add them to the cart, make purchases, and update the stock. It consists of multiple components, 
including a RESTful API, RabbitMQ-based messaging system, and services for order processing and inventory management.

## Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Project Structure](#project-structure)
- [API Development](#api-development)
  - [API Endpoints](#api-endpoints)
- [RabbitMQ Messaging System](#rabbitmq-messaging-system)
  - [Components](#components)
- [Running the Applications](#running-the-applications)
- [Testing the System](#testing-the-system)
- [Documentation](#documentation)

## Getting Started

### Prerequisites

Before you begin, ensure you have the following prerequisites installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [RabbitMQ Server](https://www.rabbitmq.com/download.html) (or a cloud-based RabbitMQ service)

### Installation

1. Clone this repository:

   ```bash
   git clone <repository_url>
   cd <project_directory>
   ```
	1. Configure RabbitMQ connection details in your project (refer to RabbitMQ Configuration).

	2. Build and run the applications (refer to Running the Applications).
### Project Structure

/bookstore-api      # ASP.NET Core API for bookstore operations
/order-service      # Order Processing Service
/inventory-service  # Inventory Management Service

## API Development
### API Endpoints
Fetching the list of books
Endpoint: GET /api/books
Description: Retrieve a list of available books.
Fetching the details of a specific book
Endpoint: GET /api/books/{id}
Description: Retrieve detailed information about a specific book.
Placing an order
Endpoint: POST /api/orders
Description: Place a new order with a list of items.
Fetching order status
Endpoint: GET /api/orders/{id}
Description: Retrieve the status of a specific order.

## RabbitMQ Messaging System
Components
Order Processing Service
Description: Handles order placement and publishing order messages to RabbitMQ.
Inventory Management Service
Description: Subscribes to RabbitMQ messages, processes orders, and updates book inventory.

### Running the Applications
cd <project_directory>

### Build the solution
dotnet build

### Run the ASP.NET Core API
dotnet run --project bookstore-api

### Run the Order Processing Service
dotnet run --project order-service

### Run the Inventory Management Service
dotnet run --project inventory-service

## Testing the System
Provide instructions on how to test the system, including:

Sending HTTP requests to the API endpoints using tools like Postman or curl.
Monitoring RabbitMQ queues for messages.
Testing various scenarios, such as high traffic, invalid orders, and error handling.


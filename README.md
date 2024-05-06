# Insurance Claims Handling

This API provides endpoints for managing insurance claims and covers. It allows users to retrieve, create, and delete both claims and covers.

# Prerequisites for local environment:
- .NET 8 SDK
- `Docker` and `docker-compose`
- An Azure Subscription

# Steps to do:
- Create a new Service Bus resource for local claims API. 
- Create new queues in this resource: `claim_audit` and `cover_audit`.
- Insert Service bus connection string into the appsettings.json: `ConnectionStrings:ServiceBus`
- Use  `$ docker-compose up --detach` to start the containers with dependencies. The existing code base is preconfigured to work with these containers. There are no volumes setup for any of the storage, so when you docker-compose down these storage media **WILL NOT BE PERSISTED**.

# Deployed playground:
- A version is deployed in Azure, where the functionality of the Insurance Claims Handling service can be accessed: https://its-claims-api.azurewebsites.net/swagger/index.html

# TODO:
A list of tasks to improve the Insurance Claims Handling service:

- Implement Authentication and Authorization.
- Implement Pagination for GetAll HTTP Requests.
- Implement Continuous Integration and Continuous Deployment (CI/CD).
- Implement KeyVault for secrets management.
- Implement Infrastructure as Code (IaC). 

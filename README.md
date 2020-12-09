# VirtualMind

- API is implemented following Cqs principle using MediatR library
- DB commands are executed using Dapper micro ORM.
- Logging is implemented using Serilog.
- Unit tests are implemented using XUnit 
- UI: only the first page is implemented

# Answer 2a
In order to validate that the user id actually belongs to the user we need a Bearer or JWT Token containing the user information encrypted. Then in the backend add an Authorization Filter to decrypt the token and validate the user id

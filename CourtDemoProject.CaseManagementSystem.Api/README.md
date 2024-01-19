# CourtDemoProject.CaseManagementSystem.Api

## Overview
The CourtDemoProject.CaseManagementSystem.Api is the backend service of the CourtDemoProject, responsible for managing and providing access to case management data. It serves as the primary interface for the front-end application to interact with the stored data.

### Features:

- RESTful API design.
- CRUD operations for cases, charges, case participants, and case details.
Secure endpoints with appropriate HTTP methods.

### API Routes:

#### Case Routes:
- GET /v1/Cases: Retrieve all cases.
- GET /v1/Cases/{id}: Retrieve a specific case by ID.
- POST /v1/Cases: Create a new case.
- PUT /v1/Cases/{id}: Update an existing case by ID.
- DELETE /v1/Cases/{id}: Delete a case by ID.

#### Charge Routes:
- GET /v1/Charges: Retrieve all charges.
- GET /v1/Charges/{id}: Retrieve a specific charge by ID.
- POST /v1/Charges: Create a new charge.
- PUT /v1/Charges/{id}: Update an existing charge by ID.
- DELETE /v1/Charges/{id}: Delete a charge by ID.

#### Case Participant Routes:
- GET /v1/CaseParticipants: Retrieve all case participants.
- GET /v1/CaseParticipants/{id}: Retrieve a specific participant by ID.
- POST /v1/CaseParticipants: Create a new case participant.
- PUT /v1/CaseParticipants/{id}: Update an existing participant by ID.
- DELETE /v1/CaseParticipants/{id}: Delete a participant by ID.

#### Case Details Routes:
- GET /v1/CaseDetails: Retrieve all case details.
- GET /v1/CaseDetails/{id}: Retrieve specific case details by ID.
- POST /v1/CaseDetails: Create new case details.
- PUT /v1/CaseDetails/{id}: Update existing case details by ID.
- DELETE /v1/CaseDetails/{id}: Delete specific case details by ID.

### Testing
The API can be tested using tools like Postman or Swagger UI.
Ensure to follow the API documentation for testing different endpoints.

### License
This project is licensed under the MIT License.
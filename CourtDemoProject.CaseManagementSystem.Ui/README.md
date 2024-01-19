# CourtDemoProject.CaseManagementSystem.Ui

## Overview:
The CourtDemoProject.CaseManagementSystem.Ui serves as the front-end user interface for the CourtDemoProject. It interacts with the CourtDemoProject.CaseManagementSystem.Api to manage and display case-related data in an intuitive and user-friendly manner.

### Features:
Responsive UI for managing cases, charges, case participants, and case details.
Easy navigation and interaction with various components of the case management system.
Real-time data fetching and display from the backend API.

### Technology Stack:
- Next.js for server-side rendering and routing.
- React for building the user interface.
- Material-UI for styled components and responsive design.
- Axios for making HTTP requests to the backend API.

### Pages and Components:

#### Pages:
- Home Page: Overview of the system.
- Case Details: Lists all case details and allows for CRUD operations.
- Charges: Manages charge information.
- Case Participants: Handles the details of case participants.
- Cases: Showcases and manages all cases.

#### Components:
- Navbar: Navigation component for easy access to different pages.
- CaseDetails, Charges, CaseParticipants, Cases: Components for managing respective entities.

### Getting Started:
Ensure Node.js is installed.
Clone the repository and navigate to the CourtDemoProject.CaseManagementSystem.Ui directory.
Run npm install to install dependencies.
Start the development server with npm run dev.

### Testing:
The front-end can be tested manually by navigating through different pages and components.
Ensure the backend API is running to fetch and post data.

### Deployment:
The application can be built for production using npm run build.
It can be deployed to any static hosting service or a server capable of serving Next.js applications.

### License:
This project is licensed under the MIT License.
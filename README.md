# Contract_Monthly_Claims

# 📝 Contract Monthly Claims Application

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-6.0-blue?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-9.0-blue?logo=csharp)
![MVC](https://img.shields.io/badge/MVC-Pattern-green)

---

## Overview
The **Contract Monthly Claims Application** is an ASP.NET Core MVC web application that allows lecturers to submit their monthly teaching claims and enables administrators to review, verify, and approve them.  

- **No database required**: Uses in-memory storage, JSON, or encrypted files.
- **No authentication/roles** required.
- **Focus on UI/UX**: Clean and intuitive design for smooth claim submission and management.

---

## Features

### Lecturer
- Submit claims with:
  - Lecturer ID
  - Module selection (with fixed hourly rates)
  - Hours worked
  - Additional notes
  - Upload supporting documents (`.pdf`, `.docx`, `.xlsx`)
- Track the status of submitted claims.
- Real-time updates as claims move through verification and approval.

### Programme Coordinator
- View all pending claims.
- Verify or reject claims.
- Access uploaded supporting documents.

### Academic Manager
- View verified claims.
- Approve or reject claims.
- Access uploaded supporting documents.

---

## Modules & Hourly Rates

| Module Name            | Hourly Rate (R) |
|------------------------|----------------|
| Mathematics 101        | 150            |
| Physics 201            | 180            |
| Computer Science 301   | 200            |
| Chemistry 101          | 160            |

> Selecting a module automatically updates the hourly rate in the claim form.

---

## Screenshots

*(Add screenshots of Lecturer Form, Coordinator View, Manager View here)*

---

## Setup & Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Contract_Monthly_Claims.git

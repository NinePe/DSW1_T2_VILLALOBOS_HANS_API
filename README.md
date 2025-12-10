# DSW1_T2_VILLALOBOS_HANS_API
# Library API – Sistema de Gestión de Biblioteca

API REST desarrollada en **.NET 8** con arquitectura por capas (Domain, Application, Infrastructure, API) para gestionar:

- Libros (`Books`)
- Préstamos (`Loans`) con control de stock

## 🏗 Arquitectura de Proyectos

La solución contiene los siguientes proyectos:

- `Library.Domain`
  - Entidades: `Book`, `Loan`
  - Puertos de salida (repositorios): `IRepository<T>`, `IBookRepository`, `ILoanRepository`, `IUnitOfWork`

- `Library.Application`
  - DTOs:
    - `BookDto`, `CreateBookDto`
    - `LoanDto`, `CreateLoanDto`
  - Servicios:
    - `IBookService`, `BookService`
    - `ILoanService`, `LoanService`
  - AutoMapper:
    - `MappingProfile` para mapear Entidades ↔ DTOs

- `Library.Infrastructure`
  - `ApplicationDbContext` (EF Core + MySQL)
  - Implementaciones de repositorios:
    - `Repository<T>`
    - `BookRepository`
    - `LoanRepository`
    - `UnitOfWork`
  - `DependencyInjection.AddInfrastructure` para registrar DbContext, repos y UoW

- `Library.API`
  - Controladores:
    - `BooksController`
    - `LoansController`
  - Configuración de Swagger, CORS, DI, etc.

---

## ✅ Requisitos

- **.NET 8 SDK** instalado
- **MySQL 8** (o compatible con Pomelo.EntityFrameworkCore.MySql)
- Herramienta de línea de comandos:
  - `dotnet ef` (para migraciones, opcional si ya tienes la DB creada)

---

## ⚙️ Configuración de Base de Datos

La conexión a la base de datos se configura mediante variables de entorno:

- `BD_HOST` – host del servidor MySQL  
- `DB_PORT` – puerto (p.ej. `3306`)  
- `DB_NAME` – nombre de la base de datos (p.ej. `library_db`)  
- `DB_USER` – usuario MySQL  
- `DB_PASSWORD` – contraseña MySQL  

Ejemplo (Windows PowerShell):

```powershell
$env:BD_HOST="localhost"
$env:DB_PORT="3306"
$env:DB_NAME="library_db"
$env:DB_USER="root"
$env:DB_PASSWORD="tu_password"


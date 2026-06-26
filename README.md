# Atestat Cafenea

A Windows desktop ordering app for a café, built with C# / WinForms as a school graduation project (*atestat*). Customers register, log in, browse the menu, and add items to a cart. Orders and product data are persisted in a SQL Server database.

---

## Tech Stack

| Layer | Technology |
|---|---|
| UI | WinForms (.NET Framework 4.7.2) |
| UI Components | Guna.UI2.WinForms 2.0.4.6 |
| Data access | ADO.NET typed DataSet + TableAdapters |
| Database | SQL Server (local `.mdf` file — `AtestatDB.mdf`) |

---

## Database Schema

Four tables with the following relationships:

```
Clienti (ClientID PK, nume, prenume, nr_telefon)
    │
    └── Comenzi (ComandaID PK, data_comenzii, pret_total, ClientID FK)
                    │
                    └── DetaliiComenzi (ProdusID FK, ComandaID FK, Cantitate, Mentiuni)

Produse (ProdusID PK, Pret, Stoc, Valori_nutritionale)
    │
    └── DetaliiComenzi (via ProdusID FK)
```

- **Clienti** — registered customers (first name, last name, phone number)
- **Produse** — menu items with price, stock, and nutritional info
- **Comenzi** — orders placed by a customer, with timestamp and total price
- **DetaliiComenzi** — line items linking an order to specific products (quantity + optional notes)

---

## Application Flow

```
App start
    └── SignUpControl  ──(already have account?)──> LogInControl
            │                                             │
       (register)                                   (verify name +
            │                                        surname + phone)
            └──────────────────────────────────────────> DashboardControl
                                                              │
                                                     Loads product list
                                                     from DB, renders
                                                     a ProdusControl
                                                     card per item
                                                              │
                                                       Add to cart →
                                                       in-memory List<string>
                                                       (product IDs)
```

### Screens

| Control | File | Responsibility |
|---|---|---|
| `SignUpControl` | [SignUpControl.cs](SignUpControl.cs) | Collects name, surname, phone — inserts new row into `Clienti` |
| `LogInControl` | [LogInControl.cs](LogInControl.cs) | Matches name + surname + phone against `Clienti` |
| `DashboardControl` | [DashboardControl.cs](DashboardControl.cs) | Fetches all products and renders one `ProdusControl` per row |
| `ProdusControl` | [ProdusControl.cs](ProdusControl.cs) | Single menu-item card — shows image, price, nutritional values; fires `AddProduct` event |
| `Form1` | [Form1.cs](Form1.cs) | Host shell — swaps controls in/out, wires up events, owns DB logic |

Navigation is handled by `Form1` swapping user controls rather than opening new windows. All DB calls go through typed TableAdapters generated from [`AtestatDBDataSet.xsd`](AtestatDBDataSet.xsd).

Product images are loaded from a `media/` folder at runtime using the pattern `media/{ProdusID}.jpg`.

---

## Prerequisites

- Visual Studio 2019+ (Windows)
- .NET Framework 4.7.2
- SQL Server LocalDB or a full SQL Server instance
- The `AtestatDB.mdf` database file attached and the connection string configured in `App.config`

---

## Setup

1. Clone the repository.
2. Attach `AtestatDB.mdf` to your SQL Server instance (or LocalDB).
3. Update the `AtestatDBConnection` connection string in `App.config` to point to your instance.
4. Open `Atestat.sln` in Visual Studio.
5. Restore NuGet packages (`Guna.UI2.WinForms` will be downloaded automatically).
6. Add product images to a `media/` folder next to the executable (`bin/Debug/media/{ProdusID}.jpg`).
7. Build and run.

---

## Project Structure

```
Atestat/
├── Form1.cs                  # Main host form — navigation + DB event handlers
├── SignUpControl.cs          # Registration screen
├── LogInControl.cs           # Login screen
├── DashboardControl.cs       # Product menu + cart logic
├── ProdusControl.cs          # Individual product card component
├── UserConfig.cs             # Static singleton holding the logged-in user ID
├── AtestatDBDataSet.xsd      # Typed DataSet — defines tables and TableAdapters
├── App.config                # Connection string
├── packages.config           # NuGet dependencies
└── Properties/
    └── AssemblyInfo.cs
```

# Parco 🚗  
A parking management system designed to streamline operations, improve customer experience, and support data-driven decision-making.

## 📌 Overview
Parco manages vehicle entries and exits, reservations, payments, and reporting.  
The system is built with **React + Tailwind** on the frontend and **.NET 8 (ASP.NET Core, EF Core, PostgreSQL, Redis)** on the backend.  

It supports both **web (PWA, offline-ready)** and **desktop (Electron/Tauri)** deployments from a single codebase.  
Future extensions include **machine learning** modules for license plate recognition and demand forecasting.  

## ✨ Features
- Client and vehicle registration (CRUD)  
- Entry and exit tracking with ticket and receipt generation  
- Payment management, including plans, discounts, and subscriptions  
- Reservation and occupancy control  
- Reports: revenue, occupancy, and vehicle flow  
- Audit logs and alerts for security and transparency  
- Offline support (PWA) and cross-platform desktop support (Electron/Tauri)  

## 🛠️ Tech Stack
**Frontend**
- React (Vite) + Tailwind CSS  
- PWA (IndexedDB, Service Workers) for offline mode  

**Backend**
- .NET 8 (ASP.NET Core Web API, EF Core)  
- PostgreSQL (main database)  
- Redis (cache, pub/sub, background jobs)  
- Hangfire (background tasks, reporting, sync)  

**Infrastructure**
- S3/MinIO for receipts, logs, and backups  
- Docker / Docker Compose for local development  
- GitHub Actions for CI/CD pipelines  

**Future ML Integration**
- Python (pandas, scikit-learn, PyTorch) for training  
- MLFlow for model tracking/versioning  
- FastAPI/Triton for model serving (license plate recognition, demand prediction)  

## 🚀 Getting Started
Clone the repository:
```bash
git clone https://github.com/your-org/parco.git

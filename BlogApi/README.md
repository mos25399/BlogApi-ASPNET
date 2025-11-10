# C# ASP.NET Core Blog API

**[➡️ Live Demo - 線上預覽網站](請在此貼上您未來部署前端的網址)**

這是一個使用 **C# ASP.NET Core 8** 從零到一建立的 RESTful API 專案。此專案的目的，是作為一個部落格應用的後端服務，並展示在 .NET 生態系中，使用 **Entity Framework Core (EF Core)** 搭配 **PostgreSQL** 資料庫，來實作完整 CRUD (增刪查改) 功能的能力。

此專案的 API 已透過內建的 **Swagger (OpenAPI)** 介面，完成了所有端點的功能測試。

![C# Blog API Swagger 測試頁面](https://i.imgur.com/your-swagger-screenshot.png) 
*(建議替換成您 Swagger 頁面的截圖)*

---

## ✨ 核心功能 (Features)

* **完整的 RESTful API**：提供了針對「文章 (`Posts`)」資源的全部 CRUD 操作。
* **資料庫整合**：使用 **Entity Framework Core** (ORM) 搭配 **Npgsql** 驅動程式，與 **PostgreSQL** 資料庫進行連接。
* **資料庫優先 (Code-First)**：透過 C# 的 `Post` 模型 (Model)，使用 `Add-Migration` 和 `Update-Database` 指令，自動生成並遷移資料庫結構。
* **依賴注入 (DI)**：利用 ASP.NET Core 內建的 DI 容器，將資料庫上下文 (`BlogDbContext`) 注入到 `PostsController` 中，實現了低耦合的架構。
* **非同步處理**：所有資料庫操作均使用 `async/await` 進行非同步處理，提升 API 效能。
* **安全的設定管理**：透過 `appsettings.Development.json` 和 `.gitignore`，確保資料庫密碼等機密資訊不會被上傳到 Git 倉庫。
* **自動化 API 文件**：整合 `Swagger (OpenAPI)`，自動生成可互動的 API 測試文件。

---

## 🛠️ 使用技術 (Technology Stack)

* **核心框架**: `C# 12`, `.NET 8.0`, `ASP.NET Core`
* **資料庫 (ORM)**: `Entity Framework Core 8 (EF Core)`
* **資料庫 (Driver)**: `Npgsql`
* **資料庫 (Database)**: `PostgreSQL`
* **開發環境**: `Visual Studio 2022`
* **API 測試**: `Swagger (OpenAPI)`

---

## 🚀 如何在本地端運行 (Getting Started)

1.  **Clone 專案到本地**
    ```bash
    git clone [https://github.com/YourUsername/BlogApi.git](https://github.com/YourUsername/BlogApi.git)
    ```
    *(請將 YourUsername 和 BlogApi 換成您自己的 GitHub 名稱和儲存庫名稱)*

2.  **設定資料庫**
    * 在您的本機安裝 [PostgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)。
    * 使用 DBeaver 或其他工具，建立一個新的資料庫（例如 `YourBlogDb`）。

3.  **設定環境變數**
    * 在 Visual Studio 的「方案總管」中，建立一個 `appsettings.Development.json` 檔案。
    * 填入您的資料庫連線字串：
      ```json
      {
        "ConnectionStrings": {
          "DefaultConnection": "Host=localhost;Port=5432;Database=YourBlogDb;Username=postgres;Password=YourPassword"
        }
      }
      ```

4.  **套用資料庫遷移**
    * 在 Visual Studio 頂部選單，選擇「檢視」->「其他視窗」->「套件管理器主控台」。
    * 在 `PM>` 提示符號後，執行 `Update-Database` 指令。

5.  **啟動專案**
    * 按下 Visual Studio 頂部的綠色「執行」按鈕。
    * 專案將會自動啟動，並打開 Swagger 測試頁面。
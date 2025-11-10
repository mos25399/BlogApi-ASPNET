using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BlogApi.Models;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BlogApi
{
    // 這個工廠的唯一目的，就是告訴 EF Core 的 "Add-Migration" 和 "Update-Database" 工具
    // 該如何在 "設計階段" 建立 DbContext
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            // 1. 建立一個 ConfigurationBuilder
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // 2. 關鍵修正：
                //    - 先讀取 appsettings.json (作為預設值)
                //    - 再讀取 appsettings.Development.json (它會覆蓋掉 appsettings.json 中的同名設定)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true) // 標記為 optional: true
                .Build();

            // 3. 建立一個新的 DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BlogDbContext>();

            // 4. 從組合後的設定中，安全地讀取 "DefaultConnection"
            //    (它現在會從 appsettings.Development.json 中讀取到)
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 5. 告訴 builder 要使用 Npgsql 和這個連線字串
            builder.UseNpgsql(connectionString);

            // 6. 建立並回傳 DbContext
            return new BlogDbContext(builder.Options);
        }
    }
}
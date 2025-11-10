using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models
{
    public class BlogDbContext : DbContext
    {
        // 建立一個建構函式，接收設定
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        // 告訴 EF Core，我們的 Post 模型 (class)
        // 對應到資料庫中一個叫做 "Posts" 的表格 (DbSet)
        public DbSet<Post> Posts { get; set; }
    }
}
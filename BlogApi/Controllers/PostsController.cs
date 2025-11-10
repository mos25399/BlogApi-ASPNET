using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // 1. 引入 Entity Framework Core
using BlogApi.Models; // 引入我們的模型

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // 路由仍是 /api/posts
    public class PostsController : ControllerBase
    {
        // 2. 宣告一個私有的 DbContext 變數
        private readonly BlogDbContext _context;

        // 3. 透過「建構函式注入」，讓 ASP.NET Core 把資料庫"橋樑"交給我們
        public PostsController(BlogDbContext context)
        {
            _context = context;
        }

        // --- 4. 建立完整的 CRUD API ---

        // GET: api/posts (讀取所有文章)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            // 這就等於 TypeORM 的 "await eventRepo.find()"
            var posts = await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return Ok(posts);
        }

        // GET: api/posts/5 (讀取單篇文章)
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            // 這就等於 TypeORM 的 "await eventRepo.findOneBy({ id: id })"
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound(); // 回傳 404 Not Found
            }

            return Ok(post);
        }

        // POST: api/posts (新增文章)
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            post.CreatedAt = DateTime.UtcNow; // 設定建立時間

            // 這就等於 TypeORM 的 "eventRepo.save(post)"
            _context.Posts.Add(post);
            await _context.SaveChangesAsync(); // 執行資料庫儲存

            // 回傳 201 Created，並附上新建立的文章
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // PUT: api/posts/5 (更新文章)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest(); // 回傳 400 Bad Request
            }

            // 這就等於 TypeORM 的 "eventRepo.update(id, post)"
            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posts.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 回傳 204 No Content，代表更新成功
        }

        // DELETE: api/posts/5 (刪除文章)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // 這就等於 TypeORM 的 "eventRepo.delete(id)"
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent(); // 回傳 204 No Content，代表刪除成功
        }
    }
}
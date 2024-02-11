using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Modelss;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly BuiltContext _context;

        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger, BuiltContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var post = await _context.Posts.OrderBy(x => x.TimeStamp).ToListAsync();
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(post);
        //}

        [HttpGet("{id}")]
        // GET: Students/Details/5
        public async Task<IActionResult> GetPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        // GET: Students/Details/5
        public async Task<IActionResult> GetPostByCategory([FromQuery]int? categoryId)
        {

            var post = await _context.Posts.Where(x => x.CategoryId == categoryId)
                .ToListAsync();
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }



        [HttpPost]
        public async Task<IActionResult> Create(Post postReq)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = postReq.Title,
                    Contents = postReq.Contents,
                    CategoryId = postReq.CategoryId,
                    TimeStamp = DateTime.UtcNow //UTC to make UI localizstion Easy later. 
                };
                _context.Add(post);
                await _context.SaveChangesAsync();
                return Ok(post);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Post postReq)
        {
            if (postReq == null)
            {
                return NotFound();
            }
            if (postReq?.PostId == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(postReq?.PostId);
            if (post == null)
            {
                return NotFound();
            }

            // Aautomapper
            // Ignore timestamp to leave chronological order unbothered.
            // Would have edit time stamp in real app

            post.Title = postReq.Title;
            post.Contents = postReq.Contents;
            post.CategoryId = postReq.CategoryId;

         
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {

           _context.Posts.RemoveRange(_context.Posts);
           var result = await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            var result = await _context.SaveChangesAsync();

            return Ok(result);
        }
    }
}

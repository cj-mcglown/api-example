using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplication1.Modelss;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("categories")] //[Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly BuiltContext _context;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, BuiltContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var category = await _context.Categories.ToListAsync();
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }



    }
}

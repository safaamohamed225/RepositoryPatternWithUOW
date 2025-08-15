using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IGenericRepository<Book> _bookRepo;

        public BooksController(IGenericRepository<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }


        [HttpGet]

        public async Task<IActionResult> GetById()
        {
            var book = await _bookRepo.GetByIdAsync(1);

            return Ok(book);
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var all = await _bookRepo.GetAllAsync();
            return Ok(all);
        }

        [HttpGet("GetByTitle")]

        public async Task<IActionResult> GetByTitle([FromQuery] string title)
        {
            var founded = await _bookRepo.FindAsync(b=>b.Title == title, new[] {"Author"} );

            if (founded == null)
                return NotFound($"This book named {title} not found");

            return Ok(founded);
        }


        [HttpGet("GetAllWithAuthors")]

        public async Task<IActionResult> GetAllWithAuthors([FromQuery] string title)
        {
            var founded = await _bookRepo.FindAllAsync(b => b.Title.Contains(title), new[] { "Author" });

            if (founded == null)
                return NotFound($"This book named {title} not found");

            return Ok(founded);
        }

        [HttpGet("GetOrderd")]

        public async Task<IActionResult> GetOrderd([FromQuery] string title)
        {
            var founded = await _bookRepo.FindAllAsync(b => b.Title.Contains(title), null, null, b=>b.Id, OrderBy.Descending);

            if (founded == null)
                return NotFound($"This book named {title} not found");

            return Ok(founded);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is required.");

            var addedBook = await _bookRepo.AddAsync(book);
            return CreatedAtAction(nameof(GetByTitle), new { title = addedBook.Title }, addedBook);
        }

        [HttpPost("AddBooks")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> books)
        {
            if (books == null || books.Count == 0)
                return BadRequest("Books list is required.");

            var addedBooks = await _bookRepo.AddRangeAsync(books);
            return Ok(addedBooks);
        }

    }
}

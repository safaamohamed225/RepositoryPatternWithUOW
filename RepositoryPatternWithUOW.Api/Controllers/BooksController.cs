using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("id")]

        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);

            return Ok(book);
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var all = await _unitOfWork.Books.GetAllAsync();
            return Ok(all);
        }

        [HttpGet("GetByTitle")]

        public async Task<IActionResult> GetByTitle([FromQuery] string title)
        {
            var founded = await _unitOfWork.Books.FindAsync(b=>b.Title == title, new[] {"Author"} );

            if (founded == null)
                return NotFound($"This book named {title} not found");

            return Ok(founded);
        }


        [HttpGet("GetAllWithAuthors")]

        public async Task<IActionResult> GetAllWithAuthors([FromQuery] string title)
        {
            var founded = await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains(title), new[] { "Author" });

            if (founded == null)
                return NotFound($"This book named {title} not found");

            return Ok(founded);
        }

        [HttpGet("GetOrderd")]

        public async Task<IActionResult> GetOrderd([FromQuery] string title)
        {
            var founded = await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains(title), null, null, b=>b.Id, OrderBy.Descending);

            if (founded == null)
                return NotFound($"This book named {title} not found");

            return Ok(founded);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is required.");

            var addedBook = await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetByTitle), new { title = addedBook.Title }, addedBook);
            //return Ok(addedBook);
        }

        [HttpPost("AddBooks")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> books)
        {
            if (books == null || books.Count == 0)
                return BadRequest("Books list is required.");

            var addedBooks = await _unitOfWork.Books.AddRangeAsync(books);
            return Ok(addedBooks);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IGenericRepository<Author> _authorRepo;

        public AuthorsController(IGenericRepository<Author> authorRepo)
        {
            _authorRepo = authorRepo;   
        }

        [HttpGet]

        public async Task<IActionResult> GetById()
        {
            var author = await _authorRepo.GetByIdAsync(1);

            return Ok(author);
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var all = await _authorRepo.GetAllAsync();
            return Ok(all);
        }

    }
}

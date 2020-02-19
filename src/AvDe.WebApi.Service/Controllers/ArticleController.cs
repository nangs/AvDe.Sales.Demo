using AvDe.Contracts.Models;
using AvDe.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AvDe.WebApi.Service.Controllers
{
    /// <summary>
    /// Contains methods for interacting with article data
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// Deletes an article with the given id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _articleRepository.DeleteAsync(id).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Gets all articles in the database
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _articleRepository.GetAsync());
        }

        /// <summary>
        /// Gets the article with the given id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            var articles = await _articleRepository.GetAsync(id);
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles);
        }

        /// <summary>
        /// Creates a new article or updates an existing one
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Article article)
        {
            var result = await _articleRepository.UpsertAsync(article).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Gets all articles with a data field matching the start of the given string
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return BadRequest();
            }
            var articles = await _articleRepository.SearchAsync(value);
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles);
        }
    }
}

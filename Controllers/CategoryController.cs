
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICatrgoryRepository _CatrgoryRepository;
        private readonly IMapper _Mapper;

        public CategoryController(ICatrgoryRepository CatrgoryRepository, IMapper Mapper)
        {
            _CatrgoryRepository = CatrgoryRepository;
            _Mapper = Mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var Categories = _Mapper.Map<List<CategoryDto>>(_CatrgoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Categories);
        }

        [HttpGet("{CategoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_CatrgoryRepository.CatregoryExists(id))
                return NotFound();

            var Category = _Mapper.Map<CategoryDto>(_CatrgoryRepository.GetCategory(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Category);

        }

        [HttpGet("pokemon/{categoryId}")] 
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int Cateid)
        {
            var Pokecate = _Mapper.Map<PokemonDto>(_CatrgoryRepository.GetPokemonByCategory(Cateid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Pokecate);

        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int Catrgoryid , [FromBody] CategoryDto Updatecategory)
        {
            if ( Updatecategory == null)
                return BadRequest(ModelState);

            if (Catrgoryid != Updatecategory.Id)
                return BadRequest(ModelState);

            if (!_CatrgoryRepository.CatregoryExists(Catrgoryid))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CategortyMap = _Mapper.Map<Category>(Updatecategory);

            if(!_CatrgoryRepository.UpdateCategory(CategortyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_CatrgoryRepository.CatregoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = _CatrgoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_CatrgoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }


    }
}

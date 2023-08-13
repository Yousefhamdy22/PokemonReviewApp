using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.dto;


namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _PokemonRepository;
        private readonly IMapper _Mapper;
        // private readonly AppDbContext _Context; // BadCode // (using Repository Pattern )
        public PokemonController(IPokemonRepository PokemonRepository, IMapper Mapper)
        {
            _PokemonRepository = PokemonRepository;
            _Mapper = Mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public ActionResult GetPokemon()
        {
            var pokemon = _Mapper.Map<Pokemon>(_PokemonRepository.GetPokemons());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);


        }

        [HttpGet("Pokeid")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public ActionResult GetPokemon(int Pokeid)
        {
            if (!_PokemonRepository.PokemonExist(Pokeid))
                return NotFound();

            var Pokemon = _Mapper.Map<PokemonDto>(_PokemonRepository.GetPokemon(Pokeid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Pokemon);

        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int Pokeid)
        {
            if (!_PokemonRepository.PokemonExist(Pokeid))
                return NotFound();
            var rating = _PokemonRepository.GetPokemonRating(Pokeid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PokemonCreate([FromQuery] int Ownerid, [FromQuery] int Categoryid, [FromBody] PokemonDto Createpokemon)
        {
            if (Createpokemon == null)
                return BadRequest(ModelState);

            var POkemons = _PokemonRepository.GetPokemonTrimToUpper(Createpokemon);

            if (POkemons == null)
            {
                ModelState.AddModelError("", "Owner Already Exist ");
                return StatusCode(204, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();



            var PokemonMap = _Mapper.Map<Pokemon>(Createpokemon);

            if(!_PokemonRepository.PokemonCreate( Ownerid  ,  Categoryid , PokemonMap ))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Seccessful Created");
        }

       
        [HttpPut("{Pokeid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(  int Pokeid,[FromQuery] int Ownerid , [FromQuery] int Categoryid 
            , [FromBody] PokemonDto UPdatePokemon )
        {
            if (UpdatePokemon == null)
                return BadRequest(ModelState);

            if (Pokeid != UPdatePokemon.Id)
                return BadRequest(ModelState);

            if (!_PokemonRepository.PokemonExist(Pokeid))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PokemonMap = _Mapper.Map<Pokemon>(UPdatePokemon);

            if(!_PokemonRepository.PokemonCreate(Ownerid , Categoryid , PokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }







    }
}

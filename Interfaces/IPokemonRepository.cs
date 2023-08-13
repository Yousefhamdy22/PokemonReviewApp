using PokemonReviewApp.dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string Name);
        decimal GetPokemonRating(int pokeid);
        Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate); //?
        bool PokemonExist(int pokeid);
        bool PokemonCreate(int Ownerid, int Categoryid, Pokemon pokemon);
        bool UpdatePokemon(int Ownerid, int Categoryid, Pokemon pokemon);
        bool Delete(Pokemon pokemon);
        bool Save();


    }
}

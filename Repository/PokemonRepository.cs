using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PokemonReviewApp.Data;
using PokemonReviewApp.dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository 
    {

        private readonly AppDbContext _Context;

        public PokemonRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public bool PokemonCreate (int Owenerid , int Categoryid , Pokemon pokemon)
        {
            var PokemoeOwnerEntity = _Context.Owners.Where(p => p.Id == Owenerid).FirstOrDefault();
            var Category = _Context.Categories.Where(p => p.Id == Categoryid).FirstOrDefault();

            var PokemonOwner = new PokemonOwner()
            {
                Owner = PokemoeOwnerEntity ,
                Pokemon = pokemon

            };
            _Context.Add(PokemonOwner);
            var Category_ = new PokemonCategory()
            {
                Categories = Category ,
                Pokemon = pokemon

            };
            _Context.Add(Category_);

            _Context.Add(pokemon);

            return Save();

        }

        public Pokemon GetPokemon(int id)
        {
            return _Context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
            
        }

        public Pokemon GetPokemon(string name)
        {
            return _Context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeid)
        {
            var Reviwe_ = _Context.Reviews.Where(p => p.Pokemon.Id == pokeid);

            if (Reviwe_.Count() <= 0)
                return 0;
            return((decimal) Reviwe_.Sum(r => r.Rating)/ Reviwe_.Count());

        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _Context.Pokemons.OrderBy(p => p.Id).ToList();
        }
        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate) 
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool PokemonExist(int pokeid)
        {
            return _Context.Pokemons.Any(p => p.Id == pokeid);
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _Context.Update(pokemon);
            return Save();
        }
        public bool Save()
        {
            var saved = _Context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        

        public bool Delete(Pokemon pokemon)
        {
            _Context.Remove(pokemon);
            return Save();
        }
    }
}

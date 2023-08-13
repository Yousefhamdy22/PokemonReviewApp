using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepesitory : IOwnerRepesitory
    {
        private readonly AppDbContext _Context;

        public OwnerRepesitory(AppDbContext Context)
        {
            _Context = Context;
        }
        public bool CreateOwner(Owner owner)
        {
            _Context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _Context.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _Context.Owners.Where(O => O.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _Context.PokemonOwners.Where(P => P.PokemonId == pokeId).Select(O => O.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _Context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _Context.PokemonOwners.Where(P => P.OwnerId == ownerId).Select(P => P.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _Context.Owners.Any(O => O.Id == ownerId);
        }

        public bool Save()
        {
          var saved = _Context.SaveChanges();
            return saved > 0 ? true : false ;  
        }

        public bool UpdateOwner(Owner owner)
        {
            _Context.Update(owner);
            return Save();
        }
    }
}

using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CatrgoryRepository : ICatrgoryRepository
    {
      
        private readonly AppDbContext _Context;


        public CatrgoryRepository(AppDbContext Context )
        {
            _Context = Context;
          
        }


        public bool CatregoryExists(int Categoryid)
        {
            return _Context.Categories.Any(c => c.Id == Categoryid);
        }

        public bool CeateCategory(Category category)
        {
            _Context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _Context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _Context.Categories.OrderBy(c=>c.Id).ToList();
        }

        public Category GetCategory()
        {
            return _Context.Categories.FirstOrDefault();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int Categoryid)
        {
            return _Context.PokemonCategories.Where(c => c.CategoryId == Categoryid).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var Saved = _Context.SaveChanges();
            return Saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
             _Context.Update(category);
            return Save();
        }
    }
}

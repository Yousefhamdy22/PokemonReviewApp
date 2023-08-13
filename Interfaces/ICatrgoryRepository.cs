using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICatrgoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);

        ICollection<Pokemon> GetPokemonByCategory(int Categoryid);

        bool CatregoryExists(int Categoryid);
        bool CeateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);

        bool Save();

    }
}

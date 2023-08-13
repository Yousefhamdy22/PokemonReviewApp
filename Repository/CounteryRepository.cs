using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CounteryRepository : ICounteryRepository
    {
        public bool CountryExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public ICollection<Country> GetCountries()
        {
            throw new NotImplementedException();
        }

        public Country GetCountry(int id)
        {
            throw new NotImplementedException();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}

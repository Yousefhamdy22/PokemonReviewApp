namespace PokemonReviewApp.Models
{
    public class PokemonCategory
    {
        public int PokemonId { get; set; }
        public int CategoryId { get; set; }
         //Navigate 
        public Pokemon Pokemon { get; set; }
        public Category Categories { get; set; }


    }



    
}

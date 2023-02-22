using System.ComponentModel.DataAnnotations;

namespace ExampleApiCatalogGames.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Game name must contain between 3 and 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The name of the producer must contain between 3 and 100 characters")]
        public string Producer { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be a minimum of 1 dollar and a maximum of 1000 dollars")]
        public double Price { get; set; }
    }
}

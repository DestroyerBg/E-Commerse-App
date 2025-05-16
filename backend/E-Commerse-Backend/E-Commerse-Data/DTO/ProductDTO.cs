using System.ComponentModel.DataAnnotations;

namespace E_Commerse_Data.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Image { get; set; } = null!;

        public RatingDTO Rating { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse_Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(0.01, 10000.00)]
        [Precision(18,2)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(1.0,10.00)]
        [Precision(2, 2)]
        public double RatingRate { get; set; }

        [Required]
        [Range(0, 10000)]
        public int RatingCount { get; set; }

    }
}

using E_Commerse_Data.DTO;
using E_Commerse_Data.Models;

namespace E_Commerse_Core.Extensions
{
    public static class ProductExtensions
    {
        public static ProductDTO ToDto(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,    
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Image = product.ImageUrl,
                Rating = new RatingDTO
                {
                    Rate = product.RatingRate,
                    Count = product.RatingCount
                }
            };
        }

        public static Product ToModel(this ProductDTO productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                Category = productDto.Category,
                ImageUrl = productDto.Image,
                RatingRate = productDto.Rating.Rate,
                RatingCount = productDto.Rating.Count
            };
        }
    }
}

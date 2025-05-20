using E_Commerse_Core.Extensions;
using E_Commerse_Core.Helpers;
using E_Commerse_Core.Interfaces;
using E_Commerse_Data;
using E_Commerse_Data.DTO;
using E_Commerse_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse_Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductDTO>> GetAllProductsAsync()
        {
            IList<ProductDTO> products = await _context.Products
                .Select(p => p.ToDto())
                .ToListAsync();

            return products;
        }

        public async Task<ProductDTO> GetProductByIdAsync(string id)
        {
            ValidationResult<int> validationResult = ValidateId(id);

            if (!validationResult.Success)
            {
                return null;
            }

            int productId = validationResult.Data;

            ProductDTO? product = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.ToDto())
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<Result> CreateProductAsync(ProductDTO productDto)
        {
            // TODO : Validate productDto before creating a new product
            Product product = productDto.ToModel();

            await _context.Products.AddAsync(product);

            try
            {
                await _context.SaveChangesAsync();
                return Result.Successfull();
            }
            catch (Exception e)
            {
                return Result.Failed($"Error creating product: {e.Message}");
            }

        }

        public async Task<Result> UpdateProductAsync(string id, ProductDTO productDto)
        {
            ValidationResult<int> validationResult = ValidateId(id);

            if (!validationResult.Success)
            {
                return Result.Failed(validationResult.Message);
            }

            int productId = validationResult.Data;

            Product? existingProduct = await _context.Products
                .FindAsync(productId);

            if (existingProduct == null)
            {
                return Result.Failed("Product not found.");
            }

            // TODO : Validate productDto before updating the product
            EditProduct(existingProduct, productDto);

            try
            {
                await _context.SaveChangesAsync();
                return Result.Successfull();
            }
            catch (Exception e)
            {
                return Result.Failed($"Error updating product: {e.Message}");
            }
        }

        public async Task<Result> DeleteProductAsync(string id)
        {
            ValidationResult<int> validationResult = ValidateId(id);

            if (!validationResult.Success)
            {
                return Result.Failed(validationResult.Message);
            }

            int productId = validationResult.Data;
            Product? product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return Result.Failed($"Product with this {id} not found.");
            }

            try
            { 
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Result.Successfull();
            }
            catch (Exception e)
            {
                return Result.Failed(e.Message);
            }
        }

        public async Task<IList<ProductDTO>> GetProductsByCategoryAsync(string category)
        {
            IList<ProductDTO> products = await _context.Products
                .Where(p => p.Category == category)
                .Select(p => p.ToDto())
                .ToListAsync();

            return products;
        }

        public async Task<IList<ProductDTO>> SearchProductsAsync(string searchTerm)
        {
            IList<ProductDTO> products = await _context.Products
                .Where(p => p.Title.Contains(searchTerm))
                .Select(p => p.ToDto())
                .ToListAsync();

            return products;
        }

        private ValidationResult<int> ValidateId(string id)
        {
            bool isValid = int.TryParse(id, out int parsedId);

            if (!isValid || parsedId <= 0)
            {
                return ValidationResult<int>.Failed("Invalid ID format. ID must be a positive integer.");
            }

            return ValidationResult<int>.Successfull(parsedId);
        }

        private Product EditProduct(Product product, ProductDTO productDto)
        {
            product.Title = productDto.Title;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.Category = productDto.Category;
            product.ImageUrl = productDto.Image;
            product.RatingRate = productDto.Rating.Rate;
            product.RatingCount = productDto.Rating.Count;
            return product;
        }
    }
}

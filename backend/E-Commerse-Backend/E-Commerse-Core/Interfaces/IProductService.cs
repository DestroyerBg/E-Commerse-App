using E_Commerse_Core.Helpers;
using E_Commerse_Data.DTO;

namespace E_Commerse_Core.Interfaces
{
    public interface IProductService
    {
        Task<IList<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(string id);
        Task<Result> CreateProductAsync(ProductDTO productDto);
        Task<Result> UpdateProductAsync(string id, ProductDTO productDto);
        Task<Result> DeleteProductAsync(string id);
        Task<IList<ProductDTO>> GetProductsByCategoryAsync(string category);
        Task<IList<ProductDTO>> SearchProductsAsync(string searchTerm);
    }
}

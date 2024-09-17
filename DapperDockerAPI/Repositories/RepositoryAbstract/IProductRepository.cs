using DapperDockerAPI.Models;

namespace DapperDockerAPI.Repositories.RepositoryAbstract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProductsAsync();
        Task<Products> GetProductByIdAsync(int product_id);

        Task<int> CreateProductAsync(Products product);

        Task<int> UpdateProductAsync(Products product);

        Task<int> DeleteProductAsync(int id);

    }
}

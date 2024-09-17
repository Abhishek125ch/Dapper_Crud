using Dapper;
using DapperDockerAPI.Models;
using DapperDockerAPI.Repositories.RepositoryAbstract;
using System.Data;

namespace DapperDockerAPI.Repositories.RepositoryConcrete
{
    public class ProductRepository : IProductRepository
    {

        public readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateProductAsync(Products product)
        {
            const string query = "Insert into Products(name,price)values(@name,@price);SELECT CAST(SCOPE_IDENTITY() AS int)";
            return await _dbConnection.ExecuteScalarAsync<int>(query, product);
        }

        public async Task<int> DeleteProductAsync(int product_id)
        {
            const string query = "Delete From Products where id = @id";
            return await _dbConnection.ExecuteAsync(query, new { id = product_id });
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            const string query = "Select * from Products";

            return await _dbConnection.QueryAsync<Products>(query);

        }

        public async Task<Products> GetProductByIdAsync(int product_id)
        {
            const string query = "Select * from Products where id = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Products>(query, new { id = product_id });
        }

        public async Task<int> UpdateProductAsync(Products product)
        {
            const string query = "Update Products set name = @name, price = @price where id = @id";
            return await _dbConnection.ExecuteAsync(query, product);
        }
    }
}

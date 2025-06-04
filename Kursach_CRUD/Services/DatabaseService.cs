using SQLite;
using Kursach_CRUD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Kursach_CRUD.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public Task<int> AddProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<int> UpdateProductAsync(Product product)
        {
            return _database.UpdateAsync(product);
        }

        public Task<List<Product>> GetProductsByNameCategoryPriceAsync(string name, string category, decimal price)
        {
            return _database.Table<Product>()
                .Where(p => p.Name == name && p.Category == category && p.Price == price)
                .ToListAsync();
        }
    }
}

using MiPrimerAPI.Models;
using MiPrimerAPI.Repositories;

namespace MiPrimerAPI.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo) => _repo = repo;

        public Task<IEnumerable<Product>> GetAllProducts() => _repo.GetAllAsync();
    }
}

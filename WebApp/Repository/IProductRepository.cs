using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using X.PagedList;

namespace WebApp.Repository
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAll(int? page);
        Task<Product> GetByID(int id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task Delete(int id);
        Task<PagedList<Product>> GetByCategory(int categoryId);
    }
}
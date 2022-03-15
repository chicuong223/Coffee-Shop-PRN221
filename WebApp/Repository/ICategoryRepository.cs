using System.Threading.Tasks;
using WebApp.Models;
using X.PagedList;

namespace WebApp.Repository
{
    public interface ICategoryRepository
    {
        Task<IPagedList<Category>> GetCategories(int? page = 1);
        Task<Category> GetByID(int id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task Delete(int id);
    }
}
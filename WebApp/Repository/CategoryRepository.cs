using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using X.PagedList;

namespace WebApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();
        public async Task<Category> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetByID(int id)
        {
            var result = await _context.Categories.Include(c => c.Products)
                .FirstOrDefaultAsync(ca => ca.Id == id);
            return result;
        }

        public async Task<IPagedList<Category>> GetCategories(int? page = 1)
        {
            var categories = _context.Categories;
            var pageNumber = page ?? 1;
            var result = await categories.ToPagedListAsync(pageNumber, 2);
            return result;
        }

        public async Task<Category> Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
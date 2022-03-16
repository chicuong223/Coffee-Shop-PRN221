using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.RepositoryInterface;
using X.PagedList;

namespace WebApp.Repository
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Category>> GetList(Expression<Func<Category, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Category> list;
            if (isDeep.HasValue && isDeep.Value) {
                list = await _context.Categories.Where(expression)
                    .Include(i=>i.Products)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Categories.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Category> GetByID(int id, bool? isDeep = true)
        {
            Category result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Categories.Include(c => c.Products)
                .FirstOrDefaultAsync(ca => ca.Id == id);
            }
            else
            {
                result = await _context.Categories.FirstOrDefaultAsync(ca => ca.Id == id);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.Where(expression).CountAsync();
        }

        public async Task<Category> Create(Category entity)
        {
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Category> Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
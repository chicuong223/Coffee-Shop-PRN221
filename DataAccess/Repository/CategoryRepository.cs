using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using X.PagedList;

namespace DataAccess.Repository
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Category>> GetList(Expression<Func<Category, bool>> expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Category> list;
            if(expression == null)
            {
                expression = a => true;
            }
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Categories.Where(expression)
                    .Include(i => i.Products)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Categories.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Category> GetByID(object id, bool? isDeep = true)
        {
            Category result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Categories.Include(c => c.Products)
                .FirstOrDefaultAsync(ca => ca.Id == (int)id);
            }
            else
            {
                result = await _context.Categories.FirstOrDefaultAsync(ca => ca.Id == (int)id);
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

        public async Task<Category> Update(Category entity)
        {
            var category = await _context.Categories.FindAsync(entity.Id);
            if(category != null)
            {
                category.Status = entity.Status;
                category.CategoryName = entity.CategoryName;
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Categories.FindAsync((int)key);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> expression, bool? isDeep = false)
        {
            return await _context.Categories.Where(expression).ToListAsync();
        }

        public Task<Category> GetSingle(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
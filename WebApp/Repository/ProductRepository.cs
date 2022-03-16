using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.RepositoryInterface;
using X.PagedList;

namespace WebApp.Repository
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Product>> GetList(Expression<Func<Product, bool>> expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Product> list;
            if(expression == null && isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Products
                   .Include(i => i.Category)
                   .ToPagedListAsync(pageNumber, 2);
            }
            else if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Products.Where(expression)
                    .Include(i => i.Category)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Products.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Product> GetByID(object key, bool? isDeep = true)
        {
            Product result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Products
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            else
            {
                result = await _context.Products
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.Where(expression).CountAsync();
        }

        public async Task<Product> Create(Product category)
        {
            _context.Products.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Products.FindAsync((int)key);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Update(Product category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}

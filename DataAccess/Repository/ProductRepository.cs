using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using X.PagedList;

namespace DataAccess.Repository
{
    public class ProductRepository : IBaseRepository<Product>
    {
        //private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Product>> GetList(Expression<Func<Product, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Product> list;
            if (expression == null)
            {
                expression = e => true;
            }
            using (var _context = new CoffeeShopDBContext())
            {
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await _context.Products.Where(expression)
                        .Include(i => i.Category)
                        .ToPagedListAsync(pageNumber, 6);
                }
                else
                {
                    list = await _context.Products.Where(expression)
                        .ToPagedListAsync(pageNumber, 6);
                }
                return list;
            }
        }

        public async Task<Product> GetByID(object key, bool? isDeep = true)
        {
            Product result;
            using (var _context = new CoffeeShopDBContext())
            {
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
        }

        public Task<int> Count(Expression<Func<Product, bool>> expression)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                return _context.Products.Where(expression).CountAsync();
            }
        }

        public async Task<Product> Create(Product category)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                _context.Products.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
        }

        public async Task Delete(object key)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                var product = await _context.Products.FindAsync((int)key);
                product.Status = false;
                _context.Entry(product).State = EntityState.Detached;
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> Update(Product entity)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                var product = await _context.Products.FindAsync(entity.Id);
                if (product != null)
                {
                    product.Status = entity.Status;
                    product.Stock = entity.Stock;
                    product.Price = entity.Price;
                    product.ProductName = entity.ProductName;
                    product.CategoryId = entity.CategoryId;
                    if (!string.IsNullOrWhiteSpace(entity.ImageURL))
                    {
                        product.ImageURL = entity.ImageURL;
                    }
                    _context.Entry(product).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return product;
                }
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> expression, bool? isDeep = false)
        {
            if (expression == null)
            {
                expression = a => true;
            }
            using (var _context = new CoffeeShopDBContext())
            {
                return await _context.Products.Where(expression).ToListAsync();
            }
        }

        public Task<Product> GetSingle(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

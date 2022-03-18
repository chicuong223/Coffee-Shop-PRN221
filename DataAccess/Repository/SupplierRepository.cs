using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataObject.Models;
using DataAccess.RepositoryInterface;
using X.PagedList;
using DataObject.Models;

namespace DataAccess.Repository
{
    public class SupplierRepository : IBaseRepository<Supplier>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Supplier>> GetList(Expression<Func<Supplier, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Supplier> list;
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Suppliers.Where(expression)
                    .Include(i => i.Supplies)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Suppliers.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Supplier> GetByID(object key, bool? isDeep = true)
        {
            Supplier result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Suppliers
                    .Include(c => c.Supplies)
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            else
            {
                result = await _context.Suppliers
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Supplier, bool>> expression)
        {
            return _context.Suppliers.Where(expression).CountAsync();
        }

        public async Task<Supplier> Create(Supplier category)
        {
            _context.Suppliers.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Suppliers.FindAsync((int)key);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Supplier> Update(Supplier category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<IEnumerable<Supplier>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Supplier>> GetAll(Expression<Func<Supplier, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> GetSingle(Expression<Func<Supplier, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

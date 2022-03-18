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
    public class SupplierRepository : IBaseRepository<Supplier>
    {
        //private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Supplier>> GetList(Expression<Func<Supplier, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
            {
                var pageNumber = page ?? 1;
                IPagedList<Supplier> list;
                if (expression == null)
                {
                    expression = e => true;
                }
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await _context.Suppliers.Where(expression)
                        .Include(i => i.Supplies)
                        .ToPagedListAsync(pageNumber, 5);
                }
                else
                {
                    list = await _context.Suppliers.Where(expression)
                        .ToPagedListAsync(pageNumber, 5);
                }

                return list;
            }
        }

        public async Task<Supplier> GetByID(object key, bool? isDeep = true)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
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
        }

        public Task<int> Count(Expression<Func<Supplier, bool>> expression)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
            {
                if (expression == null)
                {
                    expression = e => true;
                }
                return _context.Suppliers.Where(expression).CountAsync();
            }
        }

        public async Task<Supplier> Create(Supplier entity)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
            {
                _context.Suppliers.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task Delete(object key)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
            {
                var category = await _context.Suppliers.FindAsync((int)key);
                category.Status = false;
                _context.Entry(category).State = EntityState.Detached;
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Supplier> Update(Supplier entity)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
            {
                var supplier = await _context.Suppliers.FindAsync(entity.Id);
                if (supplier != null)
                {
                    supplier.Status = entity.Status;
                    supplier.Name = entity.Name;
                    supplier.Phone = entity.Phone;

                    _context.Entry(supplier).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return supplier;
                }
                return null;
            }
        }


        public async Task<IEnumerable<Supplier>> GetAll(Expression<Func<Supplier, bool>> expression)
        {
            using (CoffeeShopDBContext _context = new CoffeeShopDBContext())
            {
                return await _context.Suppliers.Where(expression).ToListAsync();
            }
        }

        public Task<Supplier> GetSingle(Expression<Func<Supplier, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

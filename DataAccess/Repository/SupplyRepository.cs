using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.RepositoryInterface;
using X.PagedList;
using DataObject.Models;

namespace DataAccess.Repository
{
    public class SupplyRepository : IBaseRepository<Supply>
    {
        //private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Supply>> GetList(Expression<Func<Supply, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Supply> list;
            if (expression == null)
            {
                expression = e => true;
            }
            using (var _context = new CoffeeShopDBContext())
            {
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await _context.Supplies.Where(expression)
                        .Include(i => i.Supplier)
                        .Include(i => i.Product)
                        .OrderByDescending(i => i.SupplyDate)
                        .ToPagedListAsync(pageNumber, 2);
                }
                else
                {
                    list = await _context.Supplies.Where(expression)
                        .OrderByDescending(i => i.SupplyDate)
                        .ToPagedListAsync(pageNumber, 2);
                }
                return list;
            }
        }

        public async Task<Supply> GetByID(object key, bool? isDeep = true)
        {
            Supply result;
            var keyObject = ((int SupplierId, int ProductId, DateTime SupplyDate))key;
            using (var _context = new CoffeeShopDBContext())
            {
                if (isDeep.HasValue && isDeep.Value)
                {
                    result = await _context.Supplies
                        .Include(c => c.Supplier)
                        .Include(c => c.Product)
                        .FirstOrDefaultAsync(ca => ca.SupplierId == keyObject.SupplierId && ca.ProductId == ca.ProductId && ca.SupplyDate == keyObject.SupplyDate);
                }
                else
                {
                    result = await _context.Supplies
                        .FirstOrDefaultAsync(ca => ca.SupplierId == keyObject.SupplierId && ca.ProductId == ca.ProductId && ca.SupplyDate == keyObject.SupplyDate);
                }
                return result;
            }
        }

        public Task<int> Count(Expression<Func<Supply, bool>> expression)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                return _context.Supplies.Where(expression).CountAsync();
            }
        }

        public async Task<Supply> Create(Supply category)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                _context.Supplies.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
        }

        public async Task Delete(object key)
        {
            var keyObject = ((int SupplierId, int ProductId, DateTime SupplyDate))key;
            using (var _context = new CoffeeShopDBContext())
            {
                var supply =
                    await _context.Supplies
                        .FirstOrDefaultAsync(ca => ca.SupplierId == keyObject.SupplierId && ca.ProductId == ca.ProductId && ca.SupplyDate == keyObject.SupplyDate);
                if(supply != null)
				{
                    _context.Supplies.Remove(supply);
                    await _context.SaveChangesAsync();
				}
            }
        }

        public async Task<Supply> Update(Supply category)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return category;
            }
        }

        public Task<IEnumerable<Supply>> GetAll(Expression<Func<Supply, bool>> expression, bool? isDeep = false)
        {
            throw new NotImplementedException();
        }

        public Task<Supply> GetSingle(Expression<Func<Supply, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class BillRepository : IBaseRepository<Bill>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Bill>> GetList(Expression<Func<Bill, bool>> expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Bill> list;
            if (expression == null)
            {
                list = await _context.Bills
                    .ToPagedListAsync(pageNumber, 2);
            }
            else if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Bills.Where(expression)
                    .Include(i => i.Voucher)
                    .Include(i=>i.BillDetails)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Bills.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Bill> GetByID(object key, bool? isDeep = true)
        {
            Bill result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Bills
                    .Include(c => c.Voucher)
                    .Include(i => i.BillDetails)
                .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            else
            {
                result = await _context.Bills.FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Bill, bool>> expression)
        {
            return _context.Bills.Where(expression).CountAsync();
        }

        public async Task<Bill> Create(Bill category)
        {
            _context.Bills.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Bills.FindAsync((int)key);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Bill> Update(Bill category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<IEnumerable<Bill>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bill>> GetAll(Expression<Func<Bill, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> GetSingle(Expression<Func<Bill, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

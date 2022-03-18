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
                expression = a => true;
            }
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Bills.Where(expression)
                    .Include(i => i.Voucher)
                    .Include(i => i.BillDetails)
                    .Include(i => i.StaffUsernameNavigation)
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
                    .Include(c => c.StaffUsernameNavigation)
                    .Include(i => i.BillDetails)
                    .ThenInclude(d => d.Product)
                    .SingleOrDefaultAsync(b => b.Id == (int)key) ?? null;
            }
            else
            {
                result = await _context.Bills.SingleOrDefaultAsync(ca => ca.Id == (int)key) ?? null;
            }
            if(result != null)
            {
                _context.Entry(result).State = EntityState.Detached;
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
            var updated = await _context.Bills.FindAsync(category.Id);
            if(updated != null)
            {
                updated.Discount = category.Discount;
                updated.VoucherId = category.VoucherId;
                _context.Entry(updated).State = EntityState.Detached;
                _context.Entry(updated).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;
        }


        public async Task<IEnumerable<Bill>> GetAll(Expression<Func<Bill, bool>> expression)
        {
            return await _context.Bills.Where(expression).ToListAsync();
        }

        public Task<Bill> GetSingle(Expression<Func<Bill, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

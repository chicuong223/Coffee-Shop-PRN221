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
        public async Task<IPagedList<Bill>> GetList(Expression<Func<Bill, bool>> expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Bill> list;
            if (expression == null)
            {
                expression = a => true;
            }
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    if (isDeep.HasValue && isDeep.Value)
                    {
                        list = await context.Bills.Where(expression)
                            .Include(i => i.Voucher)
                            .Include(i => i.BillDetails)
                            .Include(i => i.StaffUsernameNavigation)
                            .OrderByDescending(i => i.BillDate)
                            .ToPagedListAsync(pageNumber, 2);
                    }
                    else
                    {
                        list = await context.Bills.Where(expression)
                            .OrderByDescending(i => i.BillDate)
                            .ToPagedListAsync(pageNumber, 2);
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        public async Task<Bill> GetByID(object key, bool? isDeep = true)
        {
            Bill result;
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    if (isDeep.HasValue && isDeep.Value)
                    {
                        result = await context.Bills
                            .Include(c => c.Voucher)
                            .Include(c => c.StaffUsernameNavigation)
                            .Include(i => i.BillDetails)
                            .ThenInclude(d => d.Product)
                            .SingleOrDefaultAsync(b => b.Id == (int)key) ?? null;
                    }
                    else
                    {
                        result = await context.Bills.SingleOrDefaultAsync(ca => ca.Id == (int)key) ?? null;
                    }
                    if (result != null)
                    {
                        context.Entry(result).State = EntityState.Detached;
                    }
                }

            }
            catch
            {
                throw;
            }

            return result;
        }

        public Task<int> Count(Expression<Func<Bill, bool>> expression)
        {
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    return context.Bills.Where(expression).CountAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bill> Create(Bill bill)
        {
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    context.Bills.Add(bill);
                    await context.SaveChangesAsync();
                    return bill;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(object key)
        {
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    var bill = await context.Bills.FindAsync((int)key);
                    bill.Status = false;
                    context.Entry(bill).State = EntityState.Detached;
                    context.Entry(bill).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bill> Update(Bill bill)
        {
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    var updated = await context.Bills.FindAsync(bill.Id);
                    Console.WriteLine(updated == null);
                    if (updated != null)
                    {
                        updated.Discount = bill.Discount;
                        updated.VoucherId = bill.VoucherId;
                        updated.Status = bill.Status;
                        context.Entry(updated).State = EntityState.Detached;
                        context.Entry(updated).State = EntityState.Modified;
                        await context.SaveChangesAsync();
                        return updated;
                    }
                }
            }
            catch
            {
                throw;
            }

            return null;
        }


        public async Task<IEnumerable<Bill>> GetAll(Expression<Func<Bill, bool>> expression, bool? isDeep = false)
        {
            if (expression == null)
            {
                expression = a => true;
            }
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    return await context.Bills.Where(expression).ToListAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bill> GetSingle(Expression<Func<Bill, bool>> expression)
        {
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    return await context.Bills.FirstOrDefaultAsync(expression);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

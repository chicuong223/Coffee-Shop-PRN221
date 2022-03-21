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
    public class VoucherRepository : IBaseRepository<Voucher>
    {
        //private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Voucher>> GetList(Expression<Func<Voucher, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Voucher> list;
            if (expression == null)
            {
                expression = e => true;
            }
            using (var _context = new CoffeeShopDBContext())
            {
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await _context.Vouchers.Where(expression)
                        .Include(i => i.Bills)
                        .ToPagedListAsync(pageNumber, 2);
                }
                else
                {
                    list = await _context.Vouchers.Where(expression)
                        .ToPagedListAsync(pageNumber, 2);
                }
                return list;
            }
        }

        public async Task<Voucher> GetByID(object key, bool? isDeep = true)
        {
            Voucher result;
            using (var _context = new CoffeeShopDBContext())
            {
                if (isDeep.HasValue && isDeep.Value)
                {
                    result = await _context.Vouchers
                        .Include(v => v.Bills)
                        .FirstOrDefaultAsync(v => v.Id == key.ToString());
                }
                else
                {
                    result = await _context.Vouchers
                        .FirstOrDefaultAsync(v => v.Id == key.ToString());
                }
                return result;
            }
        }

        public Task<int> Count(Expression<Func<Voucher, bool>> expression)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                return _context.Vouchers.Where(expression).CountAsync();
            }
        }

        public async Task<Voucher> Create(Voucher category)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                //CoffeeShopDBContext _context = new CoffeeShopDBContext();
                _context.Vouchers.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
        }

        public async Task Delete(object key)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                var category = await _context.Vouchers.FindAsync((string)key);
                category.Status = false;
                _context.Entry(category).State = EntityState.Detached;
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Voucher> Update(Voucher entity)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                var voucher = await _context.Vouchers.FindAsync(entity.Id);
                if (voucher != null)
                {
                    voucher.Status = entity.Status;
                    voucher.Percentage = entity.Percentage;
                    voucher.Name = entity.Name;
                    voucher.UsageCount = entity.UsageCount;
                    voucher.Description = entity.Description;
                    voucher.ExpirationDate = entity.ExpirationDate;
                    _context.Entry(voucher).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return voucher;
                }
                return null;
            }
        }

        public async Task<IEnumerable<Voucher>> GetAll(Expression<Func<Voucher, bool>> expression, bool? isDeep = false)
        {
            if (expression == null)
            {
                expression = e => true;
            }
            using (var _context = new CoffeeShopDBContext())
            {
                return await _context.Vouchers.Where(expression).ToListAsync();
            }
        }

        public Task<Voucher> GetSingle(Expression<Func<Voucher, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

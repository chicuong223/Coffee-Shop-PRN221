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
    public class BillDetailRepository : IBaseRepository<BillDetail>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<BillDetail>> GetList(Expression<Func<BillDetail, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<BillDetail> list;
            if (expression == null)
            {
                expression = e => true;
            }
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.BillDetails.Where(expression)
                    .Include(i => i.Bill)
                    .Include(i => i.Product)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.BillDetails.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<BillDetail> GetByID(object key, bool? isDeep = true)
        {
            var keyObject = ((int BillId, int ProductId))key;
            BillDetail result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.BillDetails
                    .Include(c => c.Bill)
                    .Include(i => i.Product)
                .FirstOrDefaultAsync(ca => ca.BillId == keyObject.BillId && ca.ProductId == keyObject.ProductId);
            }
            else
            {
                result = await _context.BillDetails.FirstOrDefaultAsync(ca => ca.BillId == keyObject.BillId && ca.ProductId == keyObject.ProductId);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<BillDetail, bool>> expression)
        {
            return _context.BillDetails.Where(expression).CountAsync();
        }

        public async Task<BillDetail> Create(BillDetail category)
        {
            _context.BillDetails.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            //var category = await _context.BillDetails.FindAsync((int)key);
            //category.Status = false;
            //_context.Entry(category).State = EntityState.Detached;
            //_context.Entry(category).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task<BillDetail> Update(BillDetail category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<IEnumerable<BillDetail>> GetAll(Expression<Func<BillDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BillDetail> GetSingle(Expression<Func<BillDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

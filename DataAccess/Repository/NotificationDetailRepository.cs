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
    public class NotificationDetailRepository : IBaseRepository<NotificationDetail>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<NotificationDetail>> GetList(Expression<Func<NotificationDetail, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<NotificationDetail> list;
            if (expression == null)
            {
                expression = e => true;
            }
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.NotificationDetails.Where(expression)
                    .Include(i => i.Notification)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.NotificationDetails.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<NotificationDetail> GetByID(object key, bool? isDeep = true)
        {
            NotificationDetail result;
            var keyObject = ((int NotificationId, int ProductId))key;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.NotificationDetails
                    .Include(c => c.Notification)
                    .FirstOrDefaultAsync(ca => ca.NotificationId == keyObject.NotificationId && ca.ProductId == keyObject.ProductId);
            }
            else
            {
                result = await _context.NotificationDetails.FirstOrDefaultAsync(ca => ca.NotificationId == keyObject.NotificationId && ca.ProductId == keyObject.ProductId);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<NotificationDetail, bool>> expression)
        {
            return _context.NotificationDetails.Where(expression).CountAsync();
        }

        public async Task<NotificationDetail> Create(NotificationDetail category)
        {
            _context.NotificationDetails.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            //var category = await _context.NotificationDetails.FindAsync((int)key);
            //category. = false;
            //_context.Entry(category).State = EntityState.Detached;
            //_context.Entry(category).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task<NotificationDetail> Update(NotificationDetail category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<IEnumerable<NotificationDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificationDetail>> GetAll(Expression<Func<NotificationDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationDetail> GetSingle(Expression<Func<NotificationDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

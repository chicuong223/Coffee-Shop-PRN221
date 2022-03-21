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
        //private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<NotificationDetail>> GetList(Expression<Func<NotificationDetail, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<NotificationDetail> list;
            if (expression == null)
            {
                expression = e => true;
            }
            using (var _context = new CoffeeShopDBContext())
            {
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
        }

        public async Task<NotificationDetail> GetByID(object key, bool? isDeep = true)
        {
            NotificationDetail result;
            var keyObject = ((int NotificationId, int ProductId))key;
            using (var _context = new CoffeeShopDBContext())
            {
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
        }

        public Task<int> Count(Expression<Func<NotificationDetail, bool>> expression)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                return _context.NotificationDetails.Where(expression).CountAsync();
            }
        }

        public async Task<NotificationDetail> Create(NotificationDetail category)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                _context.NotificationDetails.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
        }

        public async Task Delete(object key)
        {
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    var id = ((int notificationId, int productId))key;
                    var detail = await context.NotificationDetails
                        .FirstOrDefaultAsync(d => d.NotificationId == id.notificationId
                            && d.ProductId == id.productId) ?? null;
                    Console.WriteLine(detail == null);
                    if (detail != null)
                    {
                        context.Entry(detail).State = EntityState.Detached;
                        context.NotificationDetails.Remove(detail);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch
            {
                throw;
            }

            //category. = false;
            //_context.Entry(category).State = EntityState.Detached;
            //_context.Entry(category).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task<NotificationDetail> Update(NotificationDetail category)
        {
            using (var _context = new CoffeeShopDBContext())
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return category;
            }
        }

        public Task<NotificationDetail> GetSingle(Expression<Func<NotificationDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NotificationDetail>> GetAll(Expression<Func<NotificationDetail, bool>> expression, bool? isDeep = false)
        {
            IEnumerable<NotificationDetail> result = new List<NotificationDetail>();
            try
            {
                using (var context = new CoffeeShopDBContext())
                {
                    if (isDeep.HasValue && isDeep.Value)
                    {
                        result = await context.NotificationDetails
                            .Include(detail => detail.Notification)
                            .Include(detail => detail.Product)
                            .Where(expression)
                            .ToListAsync();
                    }
                    else
                    {
                        result = await context.NotificationDetails.Where(expression).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) throw new Exception(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}

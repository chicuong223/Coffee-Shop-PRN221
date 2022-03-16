using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.RepositoryInterface;
using X.PagedList;

namespace WebApp.Repository
{
    public class NotificationRepository : IBaseRepository<Notification>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Notification>> GetList(Expression<Func<Notification, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Notification> list;
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Notifications.Where(expression)
                    .Include(i => i.NotificationDetails)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Notifications.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Notification> GetByID(object key, bool? isDeep = true)
        {
            Notification result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Notifications
                    .Include(c => c.NotificationDetails)
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            else
            {
                result = await _context.Notifications.FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Notification, bool>> expression)
        {
            return _context.Notifications.Where(expression).CountAsync();
        }

        public async Task<Notification> Create(Notification category)
        {
            _context.Notifications.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Notifications.FindAsync((int)key);
            category.IsRead = true;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Notification> Update(Notification category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<IEnumerable<Notification>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetAll(Expression<Func<Notification, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

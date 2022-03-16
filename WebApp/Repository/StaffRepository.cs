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
    public class StaffRepository : IBaseRepository<Staff>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Staff>> GetList(Expression<Func<Staff, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Staff> list;
            if (isDeep.HasValue && isDeep.Value)
            {
                list = await _context.Staff.Where(expression)
                    .Include(i => i.Notifications)
                    .ToPagedListAsync(pageNumber, 2);
            }
            else
            {
                list = await _context.Staff.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
            }
            return list;
        }

        public async Task<Staff> GetByID(object key, bool? isDeep = true)
        {
            Staff result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Staff.Include(c => c.Notifications)
                .FirstOrDefaultAsync(ca => ca.Username == key.ToString());
            }
            else
            {
                result = await _context.Staff.FirstOrDefaultAsync(ca => ca.Username == key.ToString());
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Staff, bool>> expression)
        {
            return _context.Staff.Where(expression).CountAsync();
        }

        public async Task<Staff> Create(Staff category)
        {
            _context.Staff.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Staff.FindAsync((int)key);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Staff> Update(Staff category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<IEnumerable<Staff>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Staff>> GetAll(Expression<Func<Staff, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

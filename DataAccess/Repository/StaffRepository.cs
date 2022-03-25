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
    public class StaffRepository : IBaseRepository<Staff>
    {
        public async Task<IPagedList<Staff>> GetList(Expression<Func<Staff, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Staff> list;
            if (expression == null)
            {
                expression = a => true;
            }
            if (isDeep.HasValue && isDeep.Value)
            {
                using (var context = new CoffeeShopDBContext())
                {
                    list = await context.Staff.Where(expression)
                    .Include(i => i.Notifications)
                    .ToPagedListAsync(pageNumber, 2);
                }
            }
            else
            {
                using (var context = new CoffeeShopDBContext())
                {
                    list = await context.Staff.Where(expression)
                    .ToPagedListAsync(pageNumber, 2);
                }
            }
            return list;
        }

        public async Task<Staff> GetByID(object key, bool? isDeep = true)
        {
            Staff result;
            if (isDeep.HasValue && isDeep.Value)
            {
                using (var context = new CoffeeShopDBContext())
                {
                    result = await context.Staff.Include(c => c.Notifications)
                .FirstOrDefaultAsync(ca => ca.Username == key.ToString());
                }
            }
            else
            {
                using (var context = new CoffeeShopDBContext())
                {
                    result = await context.Staff.FirstOrDefaultAsync(ca => ca.Username == key.ToString());
                }

            }
            return result;
        }

        public Task<int> Count(Expression<Func<Staff, bool>> expression)
        {
            using (var context = new CoffeeShopDBContext())
            {
                return context.Staff.Where(expression).CountAsync();
            }

        }

        public async Task<Staff> Create(Staff category)
        {
            using (var context = new CoffeeShopDBContext())
            {
                context.Staff.Add(category);
                await context.SaveChangesAsync();
                return category;
            }
        }

        public async Task Delete(object key)
        {
            using (var context = new CoffeeShopDBContext())
            {
                var category = await context.Staff.FindAsync((int)key);
                category.Status = false;
                context.Entry(category).State = EntityState.Detached;
                context.Entry(category).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Staff> Update(Staff category)
        {
            using(var context = new CoffeeShopDBContext())
            {
                var staff = await context.Staff.FindAsync(category.Username);
                if (staff != null)
                {
                    staff.Status = category.Status;
                    staff.Email = category.Email;
                    staff.AvatarUrl = category.AvatarUrl;
                    staff.Phone = category.Phone;
                    staff.Password = category.Password;
                    context.Entry(staff).State = EntityState.Detached;
                    context.Entry(staff).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return staff;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Staff>> GetAll(Expression<Func<Staff, bool>> expression, bool? isDeep = false)
        {
            using (var context = new CoffeeShopDBContext())
            {
                return await context.Staff.Where(expression).ToListAsync();
            }

        }

        public async Task<Staff> GetSingle(Expression<Func<Staff, bool>> expression)
        {
            using (var context = new CoffeeShopDBContext())
            {
                var result = await context.Staff
                    .SingleOrDefaultAsync(expression) ?? null;
                return result;
            }
        }
    }
}

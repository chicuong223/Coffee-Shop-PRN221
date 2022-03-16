﻿using Microsoft.EntityFrameworkCore;
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
    public class VoucherRepository : IBaseRepository<Voucher>
    {
        private readonly CoffeeShopDBContext _context = new CoffeeShopDBContext();

        public async Task<IPagedList<Voucher>> GetList(Expression<Func<Voucher, bool>>? expression, bool? isDeep = false, int? page = 1)
        {
            var pageNumber = page ?? 1;
            IPagedList<Voucher> list;
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

        public async Task<Voucher> GetByID(object key, bool? isDeep = true)
        {
            Voucher result;
            if (isDeep.HasValue && isDeep.Value)
            {
                result = await _context.Vouchers
                    .Include(c => c.Bills)
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            else
            {
                result = await _context.Vouchers
                    .FirstOrDefaultAsync(ca => ca.Id == (int)key);
            }
            return result;
        }

        public Task<int> Count(Expression<Func<Voucher, bool>> expression)
        {
            return _context.Vouchers.Where(expression).CountAsync();
        }

        public async Task<Voucher> Create(Voucher category)
        {
            _context.Vouchers.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(object key)
        {
            var category = await _context.Vouchers.FindAsync((int)key);
            category.Status = false;
            _context.Entry(category).State = EntityState.Detached;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Voucher> Update(Voucher category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
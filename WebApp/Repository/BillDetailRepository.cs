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
    public class BillDetailRepository : IBaseRepository<BillDetail>
    {
        public Task<int> Count(Expression<Func<BillDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BillDetail> Create(BillDetail t)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BillDetail>> GetAll(Expression<Func<BillDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BillDetail> GetByID(object key, bool? isDeep = true)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<BillDetail>> GetList(Expression<Func<BillDetail, bool>> expression, bool? isDeep = false, int? page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<BillDetail> GetSingle(Expression<Func<BillDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BillDetail> Update(BillDetail t)
        {
            throw new NotImplementedException();
        }
    }
}

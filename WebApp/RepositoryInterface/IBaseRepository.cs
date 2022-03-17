using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebApp.RepositoryInterface
{
    public interface IBaseRepository<T>
    {
        public Task<IPagedList<T>> GetList(Expression<Func<T, bool>>? expression, bool? isDeep = false, int? page = 1);
        public Task<int> Count(Expression<Func<T, bool>>? expression);
        public Task<T> GetByID(object key, bool? isDeep = true);
        public Task<T> Update(T t);
        public Task<T> Create(T t);
        public Task Delete(object key);
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression);
        public Task<T> GetSingle(Expression<Func<T, bool>> expression);
    }
}

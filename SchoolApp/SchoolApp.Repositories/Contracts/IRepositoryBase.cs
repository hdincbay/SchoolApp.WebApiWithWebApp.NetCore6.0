using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        public Task<IQueryable<T>> FindAll(bool trackChanges);
        public Task<T?> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        public Task Create(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
    }
}

using Microsoft.EntityFrameworkCore;
using SchoolApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Concrete
{
    public class RepositorBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;

        public RepositorBase(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            await Task.Run(() => _context.Set<T>().Add(entity));
        }

        public async Task Delete(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }

        public async Task<IQueryable<T>> FindAll(bool trackChanges)
        {
            return await Task.Run(() => trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking());
        }

        public async Task<T?> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return await Task.Run(() => trackChanges ? _context.Set<T>().Where(expression).SingleOrDefault() : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault());
        }
        public async Task Update(T entity)
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }
    }
}

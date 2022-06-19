using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        IQueryable<T> FindBy(Expression<Func<T,bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T,bool>> predicate, int skip, int take);
        T Add(T entity);
        void Delete(T entity);
        void HardDelete(T entity);
        T Update(T entity);
    }
}

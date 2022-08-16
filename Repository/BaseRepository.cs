using Core.Common;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private MCLDBContext _Context = null;
        private DbSet<T> _Entities = null;
        private IUserResolverService _User = null;

        public BaseRepository(MCLDBContext context, IUserResolverService user)
        {
            _Context = context;
            _User = user;
            _Entities = _Context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _Entities.AsNoTracking().AsQueryable<T>().Where(x => x.IsActive == true);
            return query;
        }

        public virtual T GetById(int id)
        {
            T entity = _Entities.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _Entities
                .AsNoTracking()
                .AsQueryable<T>()
                .Where(predicate)
                .Where(x => x.IsActive == true);
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, int skip, int take)
        {
            IQueryable<T> query = _Entities
                .AsNoTracking()
                .AsQueryable<T>()
                .Where(predicate)
                .Where(x => x.IsActive == true)
                .Skip(skip)
                .Take(take);
            return query;
        }

        public virtual T Add(T entity)
        {
            _Context.Entry(entity).State = EntityState.Added;
            entity = _Entities.Add(entity).Entity;
            entity.CreationDate = DateTime.UtcNow;
            int userId;
            int.TryParse(_User.GetUser(), out userId);
            entity.CreatedBy = (userId == 0 ? null : userId);
            _Context.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            _Context.Entry(entity).State = EntityState.Modified;
            entity.IsActive = false;
            _Entities.Update(entity);
            _Context.SaveChanges();
        }

        public virtual void HardDelete(T entity)
        {
            _Context.Entry(entity).State = EntityState.Deleted;
            _Entities.Remove(entity);
            _Context.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            entity.ModificationDate = DateTime.UtcNow;
            int userId;
            int.TryParse(_User.GetUser(), out userId);
            entity.ModifiedBy = (userId == 0 ? null : userId);

            _Context.Entry(entity).State = EntityState.Modified;
            _Context.Entry(entity).Property(x => x.CreationDate).IsModified = false;
            _Context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
            // entity = _Context.Update(entity).Entity;

            _Context.SaveChanges();
            return entity;
        }
    }
}

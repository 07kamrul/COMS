using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ErrorRepository : IErrorRepository
    {
        private ErrorDbContext _Context;
        public ErrorRepository(ErrorDbContext context)
        {
            _Context = context;
        }

        public List<Error> GetErrors(int errorId)
        {
            return _Context.Errors.Where(x => x.Id == errorId && x.IsActive).ToList();
        }

        public Error SaveError(Error item)
        {
            _Context.Entry(item).State = EntityState.Added;
            item = _Context.Errors.Add(item).Entity;
            _Context.SaveChanges();
            return item;
        }
    }
}

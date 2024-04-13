using Backend.Interfaces.Services;
using DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Services
{
    public class ServiceBase<T> : ICRUDServiceBase<T> where T : class
    {
        public AppDbContext cxt { get; }

        public ServiceBase(AppDbContext dbContext)
        {
            cxt = dbContext;
        }


        public T Create(T entity)
        {
            cxt.Add<T>(entity);
            cxt.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            cxt.Update<T>(entity);
            cxt.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            cxt.Remove<T>(entity);
            cxt.SaveChanges();
            return entity;
        }

        public T? DeleteById(Guid Id)
        {
            T temp = cxt.Find<T>(Id);
            if (temp != null)
            {
                cxt.Remove(temp);
                cxt.SaveChanges();
                return temp;
            }
            return default;
        }

        public T? Get(Guid Id)
        {
            return cxt.Find<T>(Id);
        }

        public T[] GetMany(int offSet)
        {
            return cxt.Set<T>().ToArray();
        }

        public T[] GetByCondition(Func<T, bool> condition)
        {
            return cxt.Set<T>().Where(condition).ToArray();
        }
    }
}

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


        public async Task<T?> Create(T entity)
        {
            await cxt.AddAsync<T>(entity);
            await cxt.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> Update(T entity)
        {
            cxt.Update<T>(entity);
            await cxt.SaveChangesAsync();
            return entity;
        }

        public T Delete(T entity)
        {
            cxt.Remove<T>(entity);
            cxt.SaveChanges();
            return entity;
        }

        public async Task<T?> DeleteById(Guid Id)
        {
            T temp = await cxt.FindAsync<T>(Id);
            if (temp != null)
            {
                cxt.Remove(temp);
                await cxt.SaveChangesAsync();
                return temp;
            }
            return default;
        }

        public async Task<T?> Get(Guid Id)
        {
            return await cxt.FindAsync<T>(Id);
        }

        public async Task<T[]> GetMany(int offSet)
        {
            return await cxt.Set<T>()
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<T[]> GetByCondition(Func<T, bool> condition)
        {
            return  cxt.Set<T>().AsNoTracking()
                .Where(condition)
                .ToArray();
        }
    }
}

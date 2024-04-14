using Backend.Interfaces.Services;
using DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Services
{
    /// <summary>
    /// Simplifies creation of basic CRUD services, to create more refined logic inherit this class and extend 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceBase<T> : ICRUDServiceBase<T> where T : class
    {
        public AppDbContext _context { get; }

        public ServiceBase(AppDbContext dbContext)
        {
            _context = dbContext;
        }


        public async Task<T?> Create(T entity)
        {
            await _context.AddAsync<T>(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> Update(T entity)
        {
            _context.Update<T>(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Remove<T>(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<T?> DeleteById(Guid Id)
        {
            T temp = await _context.FindAsync<T>(Id);
            if (temp != null)
            {
                _context.Remove(temp);
                await _context.SaveChangesAsync();
                return temp;
            }
            return default;
        }

        public async Task<T?> Get(Guid Id)
        {
            return await _context.FindAsync<T>(Id);
        }

        public async Task<T[]> GetMany(int offSet)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<T[]> GetByCondition(Func<T, bool> condition)
        {
            return  _context.Set<T>().AsNoTracking()
                .Where(condition)
                .ToArray();
        }
    }
}

namespace Backend.Interfaces.Services
{
    public interface ICRUDServiceBase<T> where T : class
    {
        public Task<T> Create(T entity) ;
        public Task<T> Update(T entity) ;
        public T Delete(T entity) ;
        public Task<T> DeleteById(Guid Id);
        public Task<T> Get(Guid Id);
        public Task<T[]> GetMany(int offSet);
        public Task<T[]> GetByCondition(Func<T,bool> condition);
    }
}

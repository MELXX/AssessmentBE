namespace Backend.Interfaces.Services
{
    public interface ICRUDServiceBase<T> where T : class
    {
        public T Create(T entity) ;
        public T Update(T entity) ;
        public T Delete(T entity) ;
        public T DeleteById(Guid Id);
        public T Get(Guid Id);
        public T[] GetMany(int offSet);
        public T[] GetByCondition(Func<T,bool> condition);
    }
}

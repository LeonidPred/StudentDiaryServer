namespace StudentDiaryServer.Service.Interface
{
    public interface IBasicDAO<T> where T : class
    {
        // базовый CRUD
        public Task<List<T>> GetAllAsync();
        public Task<T> AddAsync(T entity);
        public Task<IResult> UpdateAsync(T entity);
        public Task<IResult> DeleteAsync(T id);
        public Task<IResult> GetAsync(T id);
    }
}

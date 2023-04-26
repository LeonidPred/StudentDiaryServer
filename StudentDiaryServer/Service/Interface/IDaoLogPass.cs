using StudentDiaryServer.Model.Entity;

namespace StudentDiaryServer.Service.Interface
{
    public interface IDaoLogPass
    {
        public Task<IResult> Login(LoginPassword entity);
        public Task<List<LoginPassword>> GetAllAsync();
        public Task<LoginPassword> AddAsync(LoginPassword entity);
        public Task<IResult> UpdateAsync(LoginPassword entity);
        public Task<IResult> DeleteAsync(LoginPassword id);
        public Task<IResult> GetAsync(LoginPassword id);
    }
}

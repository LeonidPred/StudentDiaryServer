using StudentDiaryServer.Model;
using StudentDiaryServer.Model.Entity;
using StudentDiaryServer.Service.Interface;

namespace StudentDiaryServer.Service
{
    public class DbDaoAccountType : IBasicDAO<AccountType>
    {
        private readonly AppDbContext daodb;

        public DbDaoAccountType(AppDbContext db)
        {
            this.daodb = db;
        }

        public Task<AccountType> AddAsync(AccountType entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAsync(AccountType id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> GetAsync(AccountType id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(AccountType entity)
        {
            throw new NotImplementedException();
        }
    }
}

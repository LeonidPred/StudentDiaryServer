using StudentDiaryServer.Model.Entity;
using StudentDiaryServer.Service.Interface;

namespace StudentDiaryServer.Service
{
    public class DbDaoStudGroup : IBasicDAO<StudentGroup>
    {
        public Task<StudentGroup> AddAsync(StudentGroup entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAsync(StudentGroup id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentGroup>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> GetAsync(StudentGroup id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(StudentGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}

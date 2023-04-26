using Microsoft.EntityFrameworkCore;
using StudentDiaryServer.Model;
using StudentDiaryServer.Model.Entity;
using StudentDiaryServer.Service.Interface;

namespace StudentDiaryServer.Service
{
    public class DbDaoAccount : IBasicDAO<Account>
    {
        private readonly AppDbContext daodb;

        public DbDaoAccount(AppDbContext db)
        {
            this.daodb = db;
        }

        //добавление студента
        public async Task<Account> AddAsync(Account student)
        {
            //проверка, существует ли уже такой логин
            var std = await daodb.Accounts.FirstOrDefaultAsync(c => c.Name == student.Name);
            if (std == null) return std;
            await daodb.Accounts.AddAsync(student);
            await daodb.SaveChangesAsync();
            return student;
        }
        //удаление студента
        public async Task<IResult>  DeleteAsync(Account id)
        {
            var student = await daodb.Accounts.FirstOrDefaultAsync(c => c.Id == id.Id);

            if (student == null)
            {
                return null;
            }

            //получаем запись логина-пароля для этого аккаунта
            var logpass = daodb.LoginPasswords.Where(
            c => c.AccountId == id.Id
            );
            //удаляем связанный с аккаунтом логин-пароль
            daodb.LoginPasswords.Remove((LoginPassword)logpass);

            daodb.Accounts.Remove(student);
            await daodb.SaveChangesAsync();

            return Results.Ok(new { message = "Студент удалён" });

        }

        //получение списка студентов
        public async Task<List<Account>> GetAllAsync()
        {
            var students = await daodb.Accounts.ToListAsync();

            if (students.Count == 0)
            {
                return null;
            }

            return students;
        }

        public async Task<IResult> GetAsync(Account id)
        {
            var entity = await daodb.Accounts.FirstOrDefaultAsync(c => c.Id == id.Id);

            if (entity == null)
            {
                return null;
            }
            return Results.Ok(entity);
        }

        public async Task<IResult> UpdateAsync(Account entity)
        {
            daodb.Accounts.Entry(entity).State = EntityState.Modified;
            await daodb.SaveChangesAsync();

            return Results.Ok(entity);
        }
    }
}

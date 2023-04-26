using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentDiaryServer.Model;
using StudentDiaryServer.Model.Entity;
using StudentDiaryServer.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentDiaryServer.Service
{
    public class DbDaoLogPass : IDaoLogPass
    {
        private readonly AppDbContext daodb;

        public DbDaoLogPass(AppDbContext db)
        {
            this.daodb = db;
        }
        public async Task<LoginPassword> AddAsync(LoginPassword entity)
        {
            //хэшируем пароль
            LoginPassword loginPassword = new LoginPassword() { Login = entity.Login };
            loginPassword.Password = Hashing(entity.Password);
            //добавляем сущность с хэшированным паролем
            await daodb.LoginPasswords.AddAsync(loginPassword);
            await daodb.SaveChangesAsync();
            //возвращаем исходную сущность
            return entity;
        }

        public async Task<IResult> DeleteAsync(LoginPassword id)
        {
            return null;
            //логин-пароль не удаляется отдельно от аккаунта
        }

        public async Task<List<LoginPassword>> GetAllAsync()
        {
            return null;
            //не планируется к реализации
        }

        public async Task<IResult> GetAsync(LoginPassword id)
        {
            var entity = await daodb.LoginPasswords.FirstOrDefaultAsync(c => c.Id == id.Id);

            if (entity == null)
            {
                return null;
            }
            return Results.Ok(entity);
        }

        public async Task<IResult> UpdateAsync(LoginPassword entity)
        {
            daodb.LoginPasswords.Entry(entity).State = EntityState.Modified;
            await daodb.SaveChangesAsync();

            return Results.Ok(entity);
        }
        public async Task<IResult> Login(LoginPassword entity)
        {
            //сравниваем логины и отхешированные пароли
            var logpassResult = await daodb.LoginPasswords.FirstOrDefaultAsync(c => c.Login == entity.Login&&c.Password == Hashing(entity.Password));
            //если логин-пасс неверные, возвращаем 401
            if (logpassResult is null) return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, entity.Login) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),  // действие токена истекает через 5 минут
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                username = entity.Login
            };
            return Results.Ok(response);
        }
        //приватная функция хэширования пароля
        private string Hashing(string income)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(income));
            return Convert.ToBase64String(hash);
        }
    }
}

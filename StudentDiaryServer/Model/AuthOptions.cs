using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StudentDiaryServer.Model
{
    public class AuthOptions
    {
        public const string ISSUER = "StudentDiary"; // издатель токена
        public const string AUDIENCE = "beget"; // потребитель токена
        const string KEY = "project!PROJECT323";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}

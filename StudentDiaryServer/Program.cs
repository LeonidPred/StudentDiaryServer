using StudentDiaryServer.Model;
using StudentDiaryServer.Model.Entity;
using StudentDiaryServer.Service;
using StudentDiaryServer.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//JWT авторизация
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddDbContext<AppDbContext>();

//crud
//аккаунт
builder.Services.AddTransient<IBasicDAO<Account>, DbDaoAccount>();
//тип аккаунта
builder.Services.AddTransient<IBasicDAO<AccountType>, DbDaoAccountType>();
//логин-пароль
builder.Services.AddTransient<IDaoLogPass, DbDaoLogPass>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", async (LoginPassword username, IDaoLogPass daoClient) =>
{
    return await daoClient.Login(username);
});

app.MapPost("/logpass/add", async (LoginPassword username, IDaoLogPass daoClient) =>
{
    return await daoClient.AddAsync(username);
});


app.Run();

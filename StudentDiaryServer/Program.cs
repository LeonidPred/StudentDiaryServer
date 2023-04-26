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


//JWT �����������
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = AuthOptions.ISSUER,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddDbContext<AppDbContext>();

//crud
//�������
builder.Services.AddTransient<IBasicDAO<Account>, DbDaoAccount>();
//��� ��������
builder.Services.AddTransient<IBasicDAO<AccountType>, DbDaoAccountType>();
//�����-������
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

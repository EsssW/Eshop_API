using E_Shop_API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddCors(options => options.AddPolicy(name: "EShopOrigns",
//    policy=>{
//        policy.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader();
//    }
//));

var connString = builder.Configuration.GetConnectionString("E_Shop");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<EShopDbContext>(opt =>
{
    opt.UseNpgsql(connString);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("EShopOrigns");
app.UseCors(
  options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

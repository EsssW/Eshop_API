using E_Shop_API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy(name: "EShopOrigns",
    policy=>{
        policy.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader();
    }
));

var connString = builder.Configuration.GetConnectionString("E_Shop");

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

app.UseHttpsRedirection();

app.UseCors("EShopOrigns");

app.UseAuthorization();

app.MapControllers();

app.Run();

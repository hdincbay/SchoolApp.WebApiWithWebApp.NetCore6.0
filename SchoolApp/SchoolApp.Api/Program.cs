using Microsoft.EntityFrameworkCore;
using SchoolApp.Entities.Models;
using SchoolApp.Repositories;
using SchoolApp.Repositories.Concrete;
using SchoolApp.Repositories.Contracts;
using SchoolApp.Services.Concrete;
using SchoolApp.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RepositoryContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("SchoolApp.Api"));
});
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<RepositoryContext>();

builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<ILessonService, LessonManager>();
builder.Services.AddScoped<ILessonTypeService, LessonTypeManager>();
builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

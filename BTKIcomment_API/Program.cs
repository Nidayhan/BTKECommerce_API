using BTKECommerce_core.Maper;
using BTKECommerce_core.Services.Abstract;
using BTKECommerce_Core.Services.Concrete;
using BTKECommerce_domain.Data;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.Repository;
using BTKECommerce_Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(BaseResponseModel<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



#region Db Connection
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion






#region DI AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
#endregion
#region DI
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion


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
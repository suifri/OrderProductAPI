using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Contexts;
using OrderProductAPI.DTO.Request;
using OrderProductAPI.Mapper;
using OrderProductAPI.Repository.Implementations;
using OrderProductAPI.Repository.Interfaces;
using OrderProductAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations()); ;

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IValidator<RequestProductDTO>, RequestProductDTOValidator>();
builder.Services.AddScoped<IValidator<RequestOrderProductDTO>, RequestOrderProductDTOValidator>();
builder.Services.AddScoped<IValidator<RequestOrderDTO>, RequestOrderDTOValidator>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

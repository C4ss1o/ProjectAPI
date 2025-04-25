using ProjectAPI.Endpoints.Categories;
using ProjectAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Endpoints.Products;


var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext com a string de conexão do appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger e API Explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativa Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// Endpoints
app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);

// Endpoints
app.MapMethods(ProductPost.Template, ProductPost.Methods, ProductPost.Handle);

// Endpoints
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);

// Endpoints
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);


// Inicia a aplicação
app.Run();
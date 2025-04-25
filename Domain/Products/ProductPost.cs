using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Domain.Products;
using ProjectAPI.Infra.Data;

namespace ProjectAPI.Endpoints.Products;

public static class ProductPost
{
    public static string Template => "/products";
    public static string[] Methods => new[] { HttpMethods.Post };
    public static Delegate Handle => Action;

    public static IResult Action(ProductRequest productRequest, ApplicationDbContext context)
    {
        var product = new Product
        {
            Name = productRequest.Name,
            Description = productRequest.Description,
            HasStock = productRequest.HasStock,
            CategoryId = productRequest.CategoryId
        };

        context.Products.Add(product);
        context.SaveChanges();

        return Results.Created($"/products/{product.Id}", product);
    }
}

public class ProductRequest
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string Description { get; set; }
    public bool HasStock { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Domain.Products;
using ProjectAPI.Infra.Data;

namespace ProjectAPI.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/Categories/{Id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action([FromRoute] Guid Id, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == Id).FirstOrDefault();
        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;

        context.SaveChanges();

        return Results.Ok();
    }
}

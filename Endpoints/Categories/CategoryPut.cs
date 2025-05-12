using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Domain.Products;
using ProjectAPI.Infra.Data;
using System.Security.Claims;

namespace ProjectAPI.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/Categories/{Id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action([FromRoute] Guid Id, HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Categories.Where(c => c.Id == Id).FirstOrDefault();

        if (category == null)
            return Results.NotFound();

        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId );
        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        

        context.SaveChanges();

        return Results.Ok();
    }
}

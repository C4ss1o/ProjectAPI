using Microsoft.AspNetCore.Authorization;
using ProjectAPI.Domain.Products;
using ProjectAPI.Infra.Data;
using System.Security.Claims;

namespace ProjectAPI.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/Categories";
    public static string[] Methods => new[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    //[AllowAnonymous]
    public static async Task<IResult> Action(CategoryRequest categoryRequest,HttpContext http, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, userId, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Results.Created($"/Categories/{category.Id}", category.Id);
    }
}

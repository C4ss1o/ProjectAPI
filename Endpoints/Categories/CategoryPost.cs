using ProjectAPI.Domain.Products;
using ProjectAPI.Infra.Data;

namespace ProjectAPI.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/Categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action (CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category
        {
            Name = categoryRequest.Name
        };
        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/Categories/{category.Id}", category.Id);
    }
}

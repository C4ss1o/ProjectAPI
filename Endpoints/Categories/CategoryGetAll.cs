﻿using ProjectAPI.Domain.Products;
using ProjectAPI.Infra.Data;

namespace ProjectAPI.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/Categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(ApplicationDbContext context)
    {
        var categories = context.Categories.ToList();
        var response = categories.Select(c => new EmployeeResponse { Id = c.Id, Name = c.Name, Active = c.Active });

        return Results.Ok(response);


    }
}

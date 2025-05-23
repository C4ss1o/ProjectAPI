﻿using Microsoft.AspNetCore.Authorization;
using ProjectAPI.Infra.Data;
using System.Threading.Tasks;

namespace ProjectAPI.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/Employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
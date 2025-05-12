using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ProjectAPI.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/Employees";
    public static string[] Methods => new[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize (Policy = "EmployeePolice") ]    
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        if (string.IsNullOrWhiteSpace(employeeRequest.Name) || string.IsNullOrWhiteSpace(employeeRequest.EmployeeCode))
            return Results.BadRequest("Os campos 'Name' e 'EmployeeCode' são obrigatórios.");

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var newUser = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = await userManager.CreateAsync(newUser, employeeRequest.Password);

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userClaims = new List<Claim>
        {
            new Claim("Name", employeeRequest.Name),
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("CreateBy", userId)
        };

        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/Employees/{newUser.Id}", newUser.Id);
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using ProjectAPI.Endpoints.Categories;

namespace ProjectAPI.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;
    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    { 
        var db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        var query =
          @"SELECT Email, ClaimValue AS Name
          FROM AspNetUsers u
          INNER JOIN AspNetUserClaims c ON u.Id = c.UserId
          WHERE ClaimType = 'Name'
          ORDER BY  Name
          OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        return await db.QueryAsync<EmployeeResponse>(
            query,
            new { page, rows });
    }
}

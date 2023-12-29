using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myafim.Domain;
using Myafim.Infrastructure;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class AccountsEndpoints
{
    public static void RegisterAccountsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/accounts");

        groupBuilder.MapGet("/", GetAccounts);
    }

    private static async Task<Ok<IReadOnlyCollection<Account>>> GetAccounts([FromServices] MyafimDbContext dbContext)
    {
        return Ok<IReadOnlyCollection<Account>>(await dbContext.Accounts.ToListAsync());
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.API.Models;
using Myafim.Domain.Handlers;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class AccountsEndpoints
{
    public static void RegisterAccountsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/accounts");

        groupBuilder.MapGet("/", ListAccounts);
        groupBuilder.MapGet("/{id:int}/balance", GetAccountBalance);
    }

    private static async Task<Ok<PaginationDto<AccountDto>>> ListAccounts(
        [FromServices] ListAccountsHandler handler,
        int page, int limit)
    {
        return Ok(PaginationDto<AccountDto>.FromDomain(
            await handler.HandleAsync(page, limit),
            AccountDto.FromDomain));
    }
    
    private static async Task<Ok<long>> GetAccountBalance(
        [FromServices] GetAccountBalanceHandler handler,
        int id)
    {
        return Ok(await handler.HandleAsync(id));
    }
}

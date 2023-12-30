using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.API.Models;
using Myafim.Domain.Handlers;

namespace Myafim.API.Endpoints;

public static class ImportFireflyIiiEndpoints
{
    public static void RegisterImportFireflyIiiEndpoints(this WebApplication app)
    {
        app.MapPost("/import/firefly-iii", ImportFireflyIiiAsync);
    }

    private static async Task<NoContent> ImportFireflyIiiAsync(
        [FromServices] ImportFireflyIiiIHandler importFireflyIiiIHandler,
        [FromBody] ImportFireflyIiiRequest request,
        CancellationToken cancellationToken = default
    )
    {
        await importFireflyIiiIHandler.HandleAsync(request.BaseUri, request.Token, cancellationToken);
        return TypedResults.NoContent();
    }
}
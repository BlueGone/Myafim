namespace Myafim.API.Models;

public record PaginationDto<TDto>(
    long TotalItems,
    int CurrentPage,
    int? NextPage,
    int? PreviousPage,
    int TotalPages,
    IEnumerable<TDto> Results
)
{
    public static PaginationDto<TDto> FromDomain<TDomain>(
        Pagination.EntityFrameworkCore.Extensions.Pagination<TDomain> pagination, Func<TDomain, TDto> fromDomain) =>
        new(pagination.TotalItems,
            pagination.CurrentPage,
            pagination.NextPage,
            pagination.PreviousPage,
            pagination.TotalPages,
            pagination.Results.Select(fromDomain).ToList()
        );
}
using Myafim.Domain.Models;

namespace Myafim.API.Models;

public record CategoryDto(int Id, string Name, string? Emoji)
{
    public static CategoryDto FromDomain(Category category) =>
        new(category.Id, category.Name, category.Emoji?.ToString());
}
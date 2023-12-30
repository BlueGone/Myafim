using Myafim.Domain.Models;

namespace Myafim.API.Models;

public record AccountDto(int Id, string Name)
{
    public static AccountDto FromDomain(Account account) =>
        new(account.Id, account.Name);
}
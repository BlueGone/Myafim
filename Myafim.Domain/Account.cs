using System.ComponentModel.DataAnnotations.Schema;

namespace Myafim.Domain;

public class Account
{
    public int Id { get; set; }

    public required string Name { get; set; }
}

using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Myafim.Infrastructure;

public class RuneConverter() : ValueConverter<Rune, int>(rune => rune.Value, value => new Rune(value));

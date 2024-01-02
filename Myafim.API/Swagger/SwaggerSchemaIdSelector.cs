using System.Text.RegularExpressions;

namespace Myafim.API.Swagger;

public static partial class SwaggerSchemaIdSelector
{
    public static string SelectSchemaId(Type type) =>
        string.Join(null, type.GenericTypeArguments.Select(SelectSchemaId)) +
        type.Name.RemoveGenericSuffix().RemoveDtoSuffix();

    [GeneratedRegex("`\\d$")] private static partial Regex GenericSuffixRegex();
    private static string RemoveGenericSuffix(this string name) => GenericSuffixRegex().Replace(name, string.Empty);
    [GeneratedRegex("Dto$")] private static partial Regex DtoSuffixRegex();
    private static string RemoveDtoSuffix(this string name) => DtoSuffixRegex().Replace(name, string.Empty);
}
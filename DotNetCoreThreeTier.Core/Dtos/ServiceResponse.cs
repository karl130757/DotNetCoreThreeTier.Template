

namespace DotNetCoreThreeTier.Core.Dtos
{
    public record ServiceResponse(bool success, string? message, object? data);
}

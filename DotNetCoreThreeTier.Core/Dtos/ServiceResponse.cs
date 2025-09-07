

namespace DotNetCoreThreeTier.Core.Dtos
{
    public record HandlerResponse(bool success, string? message, object? data);
}

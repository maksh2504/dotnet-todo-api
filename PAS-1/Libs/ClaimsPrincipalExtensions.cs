using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static int? GetCurrentUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(id, out var userId) ? userId : null; // TODO: return NOT_AUTHORIZED
    }
}
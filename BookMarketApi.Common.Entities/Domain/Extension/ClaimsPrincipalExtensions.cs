
using System.Security.Claims;

namespace BookMarketApi.Extension
{
    public static class ClaimsPrincipalExtensions 
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("UserId не найден в claims");

            return Guid.Parse(userId);
        }
    }
}

namespace BookMarketApi.Common.Entities.Domain.UserEntities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
}

namespace BookMarketApi.Model;
public class UserRegistration
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
}

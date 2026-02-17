namespace RightTest.UsersBL.Options;

public class JWTOptions
{
    public string Key { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Audiences { get; set; } = string.Empty;

    public string Scopes { get; set; } = string.Empty;

    public int ValidHours { get; set; }
}

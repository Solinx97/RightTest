namespace APIGateway.Options;

internal class AuthenticationOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Authority { get; set; } = string.Empty;
}

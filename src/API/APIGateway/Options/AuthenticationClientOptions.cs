namespace APIGateway.Options;

internal class AuthenticationClientOptions
{
    public string Audiences { get; set; } = string.Empty;

    public string Scopes { get; set; } = string.Empty;
}

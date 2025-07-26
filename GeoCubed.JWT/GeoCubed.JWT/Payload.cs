namespace GeoCubed.JWT;

/// <summary>
/// The payload of the jwt.
/// </summary>
public class Payload
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Payload"/> class.
    /// </summary>
    public Payload()
    {
        this.Claims = new();
    }

    /// <summary>
    /// The claims of the token.
    /// </summary>
    public Dictionary<string, object> Claims { get; set; }

    /// <summary>
    /// Converts the payload to it's json string.
    /// </summary>
    /// <returns>The json string.</returns>
    public override string ToString()
    {
        return base.ToString();
    }

    /// <summary>
    /// Converts the payload to it's base 64 url encoded string.
    /// </summary>
    /// <returns>The base 64 url encoded string.</returns>
    public string ToBase64UrlString()
    {
        throw new NotImplementedException();
    }
}

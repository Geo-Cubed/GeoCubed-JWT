namespace GeoCubed.JWT;

/// <summary>
/// Header class for the jwt.
/// </summary>
public class Header
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Header"/> class.
    /// </summary>
    public Header()
    {
        this.Typ = "JWT";
        this.Alg = string.Empty;
        this.Additional = new();
    }

    /// <summary>
    /// Gets the type of the token, this should be "JWT" for everything
    /// </summary>
    public string Typ { get; init; }

    /// <summary>
    /// Gets or sets the hash algorithm used for the signature of the jwt.
    /// </summary>
    public string Alg { get; set; }

    /// <summary>
    /// The additional elements of the token header.
    /// </summary>
    public Dictionary<string, object> Additional { get; set; }

    /// <summary>
    /// Converts the header to it's json string.
    /// </summary>
    /// <returns>The json string.</returns>
    public override string ToString()
    {
        return base.ToString();
    }

    /// <summary>
    /// Converts the header to it's base 64 url encodd string.
    /// </summary>
    /// <returns>The base 64 url encoded string.</returns>
    public string ToBase64UrlString()
    {
        throw new NotImplementedException();
    }
}

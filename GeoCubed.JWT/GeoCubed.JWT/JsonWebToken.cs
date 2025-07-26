using System.Text;

namespace GeoCubed.JWT;

/// <summary>
/// The class for the json web token.
/// </summary>
public class JsonWebToken
{
    public Header Header { get; set; }

    public Payload Payload { get; set; }

    public string Signaure { get; set; }

    /// <summary>
    /// Encodes the data into a json web token.
    /// </summary>
    /// <returns></returns>
    public string ToToken()
    {
        var sb = new StringBuilder();

        // Create the header and payload.
        sb.Append(this.Header.ToBase64UrlString());
        sb.Append('.');
        sb.Append(this.Payload.ToBase64UrlString());

        // Get signature.
        var signature = sb.ToString();
        sb.Append('.');
        sb.Append(signature);

        return sb.ToString();
    }

    /// <summary>
    /// Decodes a token into the json web token class.
    /// </summary>
    /// <param name="token">The token to decode.</param>
    /// <returns>A class containing the decoded information.</returns>
    public static JsonWebToken FromToken(string token)
    {
        throw new NotImplementedException();
    }
}

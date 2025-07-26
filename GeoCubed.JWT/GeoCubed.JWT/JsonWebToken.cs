using GeoCubed.JWT.Common;
using System.Text;

namespace GeoCubed.JWT;

/// <summary>
/// The class for the json web token.
/// </summary>
public class JsonWebToken
{
    /// <summary>
    /// Gets or sets the header of the jwt.
    /// </summary>
    public Header Header { get; set; }

    /// <summary>
    /// Gets or sets the payload of the jwt.
    /// </summary>
    public Payload Payload { get; set; }

    /// <summary>
    /// Gets or sets the signature of the jwt.
    /// </summary>
    public string Signaure { get; set; }

    /// <summary>
    /// Validates the token with a key.
    /// </summary>
    /// <param name="key">The key to use.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public bool Validate(string key)
    {
        if (this.Header.alg != "HS256")
        {
            throw new Exception($"Hashing algorithm {this.Header.alg} is not supported yet.");
        }

        var hash = CryptographyHelper.HMACSHA256(this.Header.ToBase64UrlString(), this.Payload.ToBase64UrlString(), key);

        var base64 = Convert.ToBase64String(hash);

        var urlEncoded = EncodeDecodeHelper.ToUrlEncoded(base64);

        return urlEncoded.Equals(this.Signaure);
    }

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
        // Split into 3 parts.
        var parts = token.Split('.');

        if (parts.Length != 3)
        {
            throw new Exception("Invalid token.");
        }

        var jwt = new JsonWebToken();

        // Decode header.
        var header = parts[0];
        jwt.Header = Header.FromBase64UrlString(header);

        // Decode payload.
        var payload = parts[1];
        jwt.Payload = Payload.FromBase64UrlEncoded(payload);

        // Set signature.
        jwt.Signaure = parts[2];

        return jwt;
    }
}

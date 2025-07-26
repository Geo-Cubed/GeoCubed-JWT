using GeoCubed.JWT.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
        return JsonConvert.SerializeObject(Claims);
    }

    /// <summary>
    /// Converts the payload to it's base 64 url encoded string.
    /// </summary>
    /// <returns>The base 64 url encoded string.</returns>
    public string ToBase64UrlString()
    {
        var json = this.ToString();
        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        return EncodeDecodeHelper.ToUrlEncoded(base64);
    }

    /// <summary>
    /// Converts from a base64 url encoded payload to a payload obj.
    /// </summary>
    /// <param name="base64UrlEncoded">The base64 url encoded string.</param>
    /// <returns>The payload obj.</returns>
    public static Payload FromBase64UrlEncoded(string base64UrlEncoded)
    {
        // Decode to json.
        var base64 = EncodeDecodeHelper.FromUrlEncodedToBase64(base64UrlEncoded);
        var json = EncodeDecodeHelper.FromBase64String(base64);

        // Create object.
        var jobj = JObject.Parse(json);
        var payload = new Payload();

        // Add properties.
        foreach (var property in jobj)
        {
            ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(payload.Claims, property.Key, out var exists);
            if (!exists && property.Value != null)
            {
                value = property.Value.ToObject<object>();
            }
        }

        return payload;
    }
}

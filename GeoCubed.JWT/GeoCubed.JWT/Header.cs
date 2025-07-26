using GeoCubed.JWT.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
        this.typ = "JWT";
        this.alg = string.Empty;
        this.Additional = new();
    }

    /// <summary>
    /// Gets the type of the token, this should be "JWT" for everything
    /// </summary>
    public string typ { get; private set; }

    /// <summary>
    /// Gets or sets the hash algorithm used for the signature of the jwt.
    /// </summary>
    public string alg { get; set; }

    /// <summary>
    /// The additional elements of the token header.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, object> Additional { get; set; }

    /// <summary>
    /// Converts the header to it's json string.
    /// </summary>
    /// <returns>The json string.</returns>
    public override string ToString()
    {
        var json1 = JObject.Parse(JsonConvert.SerializeObject(this));
        var json2 = JObject.Parse(JsonConvert.SerializeObject(this.Additional));
        json1.Merge(json2, new JsonMergeSettings()
        {
            MergeArrayHandling = MergeArrayHandling.Union,
        });

        return json1.ToString();
    }

    /// <summary>
    /// Converts the header to it's base 64 url encodd string.
    /// </summary>
    /// <returns>The base 64 url encoded string.</returns>
    public string ToBase64UrlString()
    {
        var json = this.ToString();
        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        return EncodeDecodeHelper.ToUrlEncoded(base64);
    }

    /// <summary>
    /// Created the header from the base64 url encoded string.
    /// </summary>
    /// <param name="base64UrlEncoded">The header string.</param>
    /// <returns>A header populated with the values.</returns>
    public static Header FromBase64UrlString(string base64UrlEncoded)
    {
        // Convert to json.
        var base64 = EncodeDecodeHelper.FromUrlEncodedToBase64(base64UrlEncoded);
        var json = EncodeDecodeHelper.FromBase64String(base64);

        // Parse into object.
        var jobj = JObject.Parse(json);
        var header = new Header();

        // Add Properties.
        foreach (var property in jobj)
        {
            switch (property.Key)
            {
                case nameof(typ):
                    header.typ = property.Value?.ToString() ?? throw new Exception("No token typ.");
                    break;
                case nameof(alg):
                    header.alg = property.Value?.ToString() ?? throw new Exception("No token algorithm.");
                    break;
                default:
                    ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(header.Additional, property.Key, out bool exists);
                    if (!exists && property.Value != null)
                    {
                        value = property.Value.ToObject<object>();
                    }

                    break;
            }
        }

        return header;
    }
}

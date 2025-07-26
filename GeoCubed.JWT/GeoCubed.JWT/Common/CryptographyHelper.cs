using System.Security.Cryptography;
using System.Text;

namespace GeoCubed.JWT.Common;

internal static class CryptographyHelper
{
    internal static byte[] HMACSHA256(string header, string payload, string key)
    {
        var encodedPart = $"{header}.{payload}";

        var hmacSha = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hash = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(encodedPart));

        return hash;
    }

    // TODO: RSA + ECDSA - both are asymmetric keys so may be interesing.
}

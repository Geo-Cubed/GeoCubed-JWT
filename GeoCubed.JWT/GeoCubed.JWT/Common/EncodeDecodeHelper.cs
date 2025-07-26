namespace GeoCubed.JWT.Common;

internal static class EncodeDecodeHelper
{
    internal static string FromBase64String(string base64)
    {
        // Decode into byte array.
        var decodedArray = Convert.FromBase64String(base64).AsSpan();

        // Convert bytes to chars.
        var str = string.Empty;
        for (int i = 0; i < decodedArray.Length; ++i)
        {
            str += (char)decodedArray[i];
        }

        return str;
    }

    internal static string FromUrlEncodedToBase64(string urlEncoded)
    {
        return urlEncoded
            .Replace('-', '+')
            .Replace('_', '/')
            .PadRight(4 * ((urlEncoded.Length + 3) / 4), '=');
    }

    internal static string ToUrlEncoded(string base64)
    {
        return base64
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace("-", "/");
    }
}

namespace GeoCubed.JWT.Tests;

public class JWTDecodeTests
{
    [Fact]
    public void VerifyCorrect()
    {
        // Arrange.
        var token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30";
        var jwt = JsonWebToken.FromToken(token);

        var key = "a-string-secret-at-least-256-bits-long";

        // Act and Assert.
        Assert.True(jwt.Validate(key));
    }
}

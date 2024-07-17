namespace QuakerZero
{
    public class JwtSettings{
        public const string SectionName = "JwtSettings";
        public string Secret { get; init; } = "This is your default secret key!";
        public int ExpirationMinutes { get; init; } = 5;
        public string Issuer { get; init; } = "Default";
        public string Audience { get; init; } = "Default";
    }
}

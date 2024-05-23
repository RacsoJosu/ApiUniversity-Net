namespace ApiRestfull.Models
{
    public class JwtSetting
    {
        public bool ValidateIssuerSgningKey { get; set; }

        public string IssueSigningKey { get; set; } = string.Empty;

        public bool ValidateIssuer { get; set; } = true;

        public string ValidIssuer { get; set; } = string.Empty;

        public bool ValidateAudience { get; set; } = true;

        public string ValidAudience { get; set; } = string.Empty;

        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; }




    }
}

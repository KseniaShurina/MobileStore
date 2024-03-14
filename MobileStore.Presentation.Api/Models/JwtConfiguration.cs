namespace MobileStore.Presentation.Api.Models
{
    public class JwtConfiguration
    {
        public string SecretKey { get; private set; }
        public string Issuer { get; private set; }
        public string Audience { get; private set; }

        private JwtConfiguration()
        {
            SecretKey = string.Empty;
            Issuer = string.Empty;
            Audience = string.Empty;
        }

        public static JwtConfiguration Create(IConfiguration configuration)
        {
            var config = new JwtConfiguration();
            configuration.GetSection("Jwt").Bind(config, o =>
            {
                o.BindNonPublicProperties = true;
            });

            return config;
        }
    }
}

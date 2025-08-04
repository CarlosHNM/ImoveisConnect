namespace ImoveisConnect.Application
{
    public class SecurityConfig
    {
        public string Secret { get; set; }
    }
    public class ApplicationConfig
    {
        public string SystemName { get; set; }
        public string SystemVersion { get; set; }
        public string UserPictureBaseUrl { get; set; }
        public string UsersByPass_HML { get; set; }
        public int TokenExpirationHours { get; set; }
        public SecurityConfig SecurityConfig { get; set; }


    }
}

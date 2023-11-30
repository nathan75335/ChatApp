namespace ChatApp.BusinessLogic.Configurations
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }    
        public int LifeTime { get; set; }
    }
}

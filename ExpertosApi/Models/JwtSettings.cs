namespace ExpertosApi.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }//llave secreta para firmar el token
        public string Issuer { get; set; }//emisor del token
        public string Audience { get; set; }//audiencia del token
        public int ExpireMinutes { get; set; }//tiempo de expiración del token en minutos
    }

}

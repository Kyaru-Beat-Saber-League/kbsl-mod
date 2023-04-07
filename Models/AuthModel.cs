namespace KBSL_MOD.Models
{
    public class AuthModel
    {
        public string AccessToken { get; set; }
        public string ERole { get; set; }
        public string ImageUrl { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string UserName { get; set; }
        public int UserSeq { get; set; }
    }
}
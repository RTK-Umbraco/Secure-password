namespace Secure_password.Dtos
{
    public class UsersDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        
        public UsersDto(string email, string password, string salt)
        {
            Email = email;
            Password = password;
            Salt = salt;
        }
    }
}

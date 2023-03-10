namespace Secure_password.Entity
{
    public class UsersEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public UsersEntity()
        {
        }

        public UsersEntity(string email, string password, string salt)
        {
            Email = email;
            Password = password;
            Salt = salt;
        }
    }
}

namespace Secure_password.Dtos
{
    public class PasswordDto
    {
        public byte[] HashedPassword { get; set; }

        public byte[] Salt { get; set; }
        public PasswordDto(byte[] hashedPassword)
        {
            HashedPassword = hashedPassword;
        }
        
        public PasswordDto(byte[] hashedpassword, byte[] salt)
        {
            HashedPassword = hashedpassword;
            Salt = salt;
        }
    }
}

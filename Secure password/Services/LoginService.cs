namespace Secure_password.Managements
{
    public class LoginService
    {
        /// <summary>
        /// Validates user login
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <returns>Whether the login was succesfully or not</returns>
        public string ValidateLogin(string email, string password)
        {
            UsersService usersManagement = new UsersService();
            HashService hashManagement = new HashService();

            var user = usersManagement.GetUserByEmail(email);

            var passwordDto = hashManagement.ComputeHashSha256WithStoredSalt(password, user.Salt);
            
            var hashedPassword = Convert.ToBase64String(passwordDto.HashedPassword);

            if (hashedPassword.Equals(user.Password))
            {
                return "Login success";
            }
            else
            {
                return "Login failed";
            }
        }
    }
}

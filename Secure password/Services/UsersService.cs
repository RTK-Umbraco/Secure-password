using Secure_password.Components;
using Secure_password.Dtos;
using Secure_password.Entity;

namespace Secure_password.Managements
{
    public class UsersService
    {
        /// <summary>
        /// Proxy for creating a user
        /// </summary>
        /// <param name="email">Takes the email that the user has inserted</param>
        /// <param name="passwordDto">Takes PasswordDto that include the hashedpassword and the generated salt</param>
        public void CreateUser(string email, PasswordDto passwordDto) 
        {
            UsersRepository usersRepository = new UsersRepository();

            var hashedPassword = Convert.ToBase64String(passwordDto.HashedPassword);
            var salt = Convert.ToBase64String(passwordDto.Salt);

            UsersEntity usersEntity = new UsersEntity(email, hashedPassword, salt);

            usersRepository.CreateUser(usersEntity);
        }

        /// <summary>
        /// Proxy for getting user by email
        /// </summary>
        /// <param name="email">Takes the email that the user has inserted</param>
        /// <returns></returns>
        public UsersDto GetUserByEmail(string email)
        {
            UsersRepository usersRepository = new UsersRepository();
            
            var userEntity = usersRepository.GetUserByEmail(email);

            var userDto = new UsersDto(userEntity.Email, userEntity.Password, userEntity.Salt);

            return userDto;
        }
    }
}

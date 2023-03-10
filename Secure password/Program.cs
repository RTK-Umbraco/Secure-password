using Secure_password.Dtos;
using Secure_password.Managements;

namespace Secure_password
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("1. Create User");
                    Console.WriteLine("2. Login");

                    var userInput = Convert.ToInt32(Console.ReadLine());

                    switch (userInput)
                    {
                        case 1:
                            CreateUser();
                            break;
                        case 2:
                            Login();
                            break;
                        default:
                            Console.WriteLine("Wrong input try again");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occured: {e.Message}");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        #region Generic
        private static string GetUserEmail()
        {
            Console.WriteLine();
            Console.WriteLine("Insert your email");

            var userEmail = Console.ReadLine();

            return userEmail;
        }

        private static string GetUserPassword()
        {
            Console.WriteLine();
            Console.WriteLine("Insert passowrd");

            var userPassowrd = Console.ReadLine();

            return userPassowrd;
        }
        #endregion

        #region Create User

        private static void CreateUser()
        {
            try
            {
                UsersService usersManagement = new UsersService();

                var email = GetUserEmail();
                var passwordDto = GeneratePasswordWithSalt();

                usersManagement.CreateUser(email, passwordDto);

                Console.WriteLine("Users has been created");
            }
            catch (Exception e)
            {
                Console.WriteLine("There occured an error while creating an user: " +  e.Message);
            }
        }

        private static PasswordDto GeneratePasswordWithSalt()
        {
            HashService hashManagement = new HashService();

            var userPassword = GetUserPassword();

            var passwordDto = hashManagement.ComputeHashSha256WithSalt(userPassword);

            return passwordDto;
        }
        #endregion

        #region Login

        private static void Login()
        {
            var email = GetUserEmail();
            var userPassword = GetUserPassword();

            LoginService loginManagement = new LoginService();
            Console.WriteLine(loginManagement.ValidateLogin(email, userPassword));
        }
        #endregion
    }
}
using Microsoft.Data.SqlClient;
using Secure_password.Entity;
using System.Data;

namespace Secure_password.Components
{
    public class UsersRepository
    {
        private const string _CONNECTIONSTRING = @"Server=DESKTOP-9CPP9JJ;Database=SecurePassowrd;Trusted_Connection=True;Trust Server Certificate=true";

        /// <summary>
        /// Creates user in database
        /// </summary>
        /// <param name="usersEntity">Takes userEnity that include user email, hashed password and the generated salt</param>
        public void CreateUser(UsersEntity usersEntity)
        {
            try
            {
                using (var conn = new SqlConnection(_CONNECTIONSTRING))
                {
                    var insertUser = @"INSERT INTO USERS(Email, Password, Salt) VALUES(@Email, @Password, @Salt)";
                    using (var cmd = new SqlCommand(insertUser, conn))
                    {
                        var parameterEmail = new SqlParameter("@Email", SqlDbType.VarChar, 255);
                        var parameterPassword = new SqlParameter("@Password", SqlDbType.VarChar, 255);
                        var parameterSalt = new SqlParameter("@Salt", SqlDbType.VarChar, 255);

                        parameterEmail.Value = usersEntity.Email;
                        parameterPassword.Value = usersEntity.Password;
                        parameterSalt.Value = usersEntity.Salt;

                        cmd.Parameters.Add(parameterEmail);
                        cmd.Parameters.Add(parameterPassword);
                        cmd.Parameters.Add(parameterSalt);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets user by email from database 
        /// </summary>
        /// <param name="email">Takes the email that the user has inserted</param>
        /// <returns>Returns user entity with email, password and salt</returns>
        public UsersEntity GetUserByEmail(string email)
        {
            try
            {
                UsersEntity usersEntity = new UsersEntity();
                using (var conn = new SqlConnection(_CONNECTIONSTRING))
                {
                    var selectUserByEmail = @"SELECT * FROM Users WHERE Email = @Email";
                    using (var cmd = new SqlCommand(selectUserByEmail, conn))
                    {
                        cmd.Parameters.AddWithValue(@"Email", email);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var userEntityEmail = reader["Email"].ToString();
                                var userEntityPassword = reader["Password"].ToString();
                                var userEntitySalt = reader["Salt"].ToString();

                                usersEntity = new UsersEntity(userEntityEmail, userEntityPassword, userEntitySalt);
                            }
                            conn.Close();
                        }
                    }
                }
                return usersEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

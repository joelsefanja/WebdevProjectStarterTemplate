using System;
using System.Data;
using Dapper;
using BCrypt.Net;
using nhlstendencafe.Models;

namespace nhlstendencafe.Repositories
{
    public class UserRepository
    {
        private static IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public bool RegisterUser(User user)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.", nameof(user.Email));
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    throw new ArgumentException("Password cannot be empty or whitespace.", nameof(user.Password));
                }

                if (UserExists(user.Email))
                {
                    throw new InvalidOperationException("User with the same email already exists.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                string sql = @"INSERT INTO Users (Email, PasswordHash)
                               VALUES (@Email, @PasswordHash)";

                using var connection = GetConnection();
                var rows = connection.Execute(sql, new
                {
                    Email = user.Email,
                    PasswordHash = hashedPassword,
                });
                
                return rows == 1;
            }
            catch (Exception ex)
            {
                // Handle the exception or throw a custom exception
                Console.WriteLine("Error occurred while registering user: " + ex.Message);
                throw;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.", nameof(email));
                }

                string sql = "SELECT * FROM Users WHERE Email = @Email";

                using var connection = GetConnection();
                var user = connection.QuerySingleOrDefault<User>(sql, new { Email = email });
                return user;
            }
            catch (Exception ex)
            {
                // Handle the exception or throw a custom exception
                Console.WriteLine("Error occurred while retrieving user: " + ex.Message);
                throw;
            }
        }

        public bool VerifyPassword(User user, string password)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User object cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Password cannot be empty or whitespace.", nameof(password));
                }

                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            }
            catch (Exception ex)
            {
                // Handle the exception or throw a custom exception
                Console.WriteLine("Error occurred while verifying password: " + ex.Message);
                throw;
            }
        }

        private static bool UserExists(string email)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                using var connection = GetConnection();
                int count = connection.ExecuteScalar<int>(sql, new { Email = email });

                return count == 1;
            }
            catch (Exception ex)
            {
                // Handle the exception or throw a custom exception
                Console.WriteLine("Error occurred while checking if user exists: " + ex.Message);
                throw;
            }
        }
    }
}

using System;
using System.Data;
using Dapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
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
                if (user == null)
                {
                    throw new InvalidOperationException("User is empty");
                }

                if (UserExist(user.Email))
                {
                    throw new InvalidOperationException("User with the same email already exists.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                string sql = @"INSERT INTO Users (Email, PasswordHash, FirstName, LastName)
                               VALUES (@Email, @PasswordHash, @Firstname, @LastName)";

                using var connection = GetConnection();
                var rows = connection.Execute(sql, new
                {
                    Email = user.Email,
                    PasswordHash = hashedPassword,
                    FirstName = user.FirstName,
                    LastName = user.LastName
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
        public bool VerifyPassword(LoginCredentials loginCredentials)
        {
            try
            {
                if (loginCredentials == null)
                {
                    throw new ArgumentNullException(nameof(loginCredentials), "Username or password should not be empty.");
                }

                if (string.IsNullOrWhiteSpace(loginCredentials.Password))
                {
                    throw new ArgumentException("Password cannot be empty or whitespace.", nameof(loginCredentials.Password));
                }

                string passwordHash = GetPasswordHashByEmail(loginCredentials.Email);

                return BCrypt.Net.BCrypt.Verify(loginCredentials.Password, passwordHash);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while verifying password: " + ex.Message);
                throw;
            }
        }
        private string GetPasswordHashByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.", nameof(email));
                }

                using var connection = GetConnection();
                string sql = "SELECT PasswordHash FROM Users WHERE Email = @Email";
                string passwordHash = connection.ExecuteScalar<string>(sql, new { Email = email });

                return passwordHash;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving password hash: " + ex.Message);
                throw;
            }
        }
        public bool UserExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty or whitespace.", nameof(email));
            }

            try
            {
                using var connection = GetConnection();
                string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                int count = connection.ExecuteScalar<int>(sql, new { Email = email });

                return count == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("User does not exist: " + ex.Message);
                throw;
            }
        }
        public (string FirstName, string LastName) GetUserNameByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.", nameof(email));
                }

                using var connection = GetConnection();
                string sql = "SELECT FirstName, LastName FROM Users WHERE Email = @Email";
                var userName = connection.QueryFirstOrDefault<(string FirstName, string LastName)>(sql, new { Email = email });

                return userName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving user names: " + ex.Message);
                throw;
            }
        }

    }

}


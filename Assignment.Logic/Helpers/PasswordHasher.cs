using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace MVC_Assignment.Logic.Helpers
{
    public class HashedPassword
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public interface IPasswordHasher
    {
        public byte[] GenerateSalt();
        public HashedPassword GenerateHashedPassword(string password);
        public bool CheckPassword(string password, string userPassword, string userSalt);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public HashedPassword GenerateHashedPassword(string password)
        {
            var salt = GenerateSalt();
            byte[] hashedPassword = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return new HashedPassword() { Password = Convert.ToBase64String(hashedPassword), Salt = Convert.ToBase64String(salt) };
        }

        public bool CheckPassword(string password, string userHashedPassword, string userSalt)
        {
            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(userSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return passwordHash == userHashedPassword;
        }
    }
}

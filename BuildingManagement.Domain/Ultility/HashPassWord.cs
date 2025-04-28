using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace BuildingManagement.Infrastructure.Ultility
{
    public static class HashPassWord
    {
        private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public static string HashPassword(string plainPassword)
        {
            if(string.IsNullOrEmpty(plainPassword))
            {
                throw new ArgumentNullException("Password can not empty",nameof(plainPassword));
            }
            return _passwordHasher.HashPassword(null, plainPassword);
        }

        public static bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException("Hashed password is null", nameof(hashedPassword));
            }
            if (string.IsNullOrEmpty(plainPassword))
            {
                throw new ArgumentNullException("Password can not empty", nameof(plainPassword));
            }
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}

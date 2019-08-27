using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace FluffySpoon.Security.Hashing.Strategies
{
    public class PKBDF2VersionStrategy : IVersionStrategy
    {
        public string Prefix => "PBKDF2";

        public string Generate(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                var salt = bytes.Salt;
                var buffer = bytes.GetBytes(0x20);

                var destination = new byte[0x31];
                Buffer.BlockCopy(salt, 0, destination, 1, 0x10);
                Buffer.BlockCopy(buffer, 0, destination, 0x11, 0x20);
                return Convert.ToBase64String(destination);
            }
        }

        public bool Verify(string hash, string value)
        {
            if (hash == null)
                return false;

            if (value == null)
                throw new ArgumentNullException("password");

            var hashBytes = Convert.FromBase64String(hash);
            if ((hashBytes.Length != 0x31) || (hashBytes[0] != 0))
                return false;

            var destination = new byte[0x10];
            Buffer.BlockCopy(hashBytes, 1, destination, 0, 0x10);

            var buffer = new byte[0x20];
            Buffer.BlockCopy(hashBytes, 0x11, buffer, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(value, destination, 0x3e8))
            {
                var buffer4 = bytes.GetBytes(0x20);
                return ByteArraysEqual(buffer, buffer4);
            }
        }

        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
            {
                return true;
            }

            if (b0 == null || b1 == null)
            {
                return false;
            }

            if (b0.Length != b1.Length)
            {
                return false;
            }

            for (int i = 0; i < b0.Length; i++)
            {
                if (b0[i] != b1[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

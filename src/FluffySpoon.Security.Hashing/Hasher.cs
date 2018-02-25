using Isopoh.Cryptography.Argon2;
using System;

namespace FluffySpoon.Security.Hashing
{
    public class Hasher
    {
		public string GenerateHash(string password) {
			return Argon2.Hash(password);
		}

		public bool VerifyHash(string hash, string value) {
			return Argon2.Verify(hash, value);
		}
    }
}

using Isopoh.Cryptography.Argon2;
using System;

namespace FluffySpoon.Security.Hashing
{
    public class Hasher : IHasher
	{
		public string Generate(string password) {
			if(password == null)
				throw new ArgumentNullException(nameof(password));

			return Argon2.Hash(password);
		}

		public bool Verify(string hash, string value) {
			if(password == null)
				return false;

			return Argon2.Verify(hash, value);
		}
    }
}

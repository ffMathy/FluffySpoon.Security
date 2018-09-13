using Isopoh.Cryptography.Argon2;

namespace FluffySpoon.Security.Hashing
{
    public class Hasher : IHasher
	{
		public string Generate(string password) {
			return Argon2.Hash(password);
		}

		public bool Verify(string hash, string value) {
			return Argon2.Verify(hash, value);
		}
    }
}

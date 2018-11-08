using Isopoh.Cryptography.Argon2;

namespace FluffySpoon.Security.Hashing.Strategies
{
	class Argon2VersionStrategy : ILatestVersionStrategy, IVersionStrategy
	{
		public string Prefix => "argon2";

		public string Generate(string password)
		{
			return Argon2.Hash(password);
		}

		public bool Verify(string hash, string value)
		{
			return Argon2.Verify(hash, value);
		}
	}
}

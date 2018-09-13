namespace FluffySpoon.Security.Hashing
{
	public interface IHasher
	{
		string Generate(string password);
		bool Verify(string hash, string value);
	}
}
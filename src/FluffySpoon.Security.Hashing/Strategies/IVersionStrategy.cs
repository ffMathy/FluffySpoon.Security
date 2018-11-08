namespace FluffySpoon.Security.Hashing.Strategies
{
	public interface IVersionStrategy
	{
		string Prefix { get; }

		string Generate(string password);
		bool Verify(string hash, string value);
	}
}
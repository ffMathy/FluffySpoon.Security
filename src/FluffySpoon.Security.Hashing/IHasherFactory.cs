namespace FluffySpoon.Security.Hashing
{
	public interface IHasherFactory
	{
		IHasher Create(string pepper = null);
	}
}
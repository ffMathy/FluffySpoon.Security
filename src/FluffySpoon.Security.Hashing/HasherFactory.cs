using System.Collections.Generic;
using System.Linq;
using FluffySpoon.Security.Hashing.Strategies;

namespace FluffySpoon.Security.Hashing
{
	class HasherFactory : IHasherFactory
	{
		private readonly IEnumerable<IVersionStrategy> strategies;

		public HasherFactory(
			IEnumerable<IVersionStrategy> strategies)
		{
			this.strategies = strategies;
		}

		public IHasher Create(string pepper = null)
		{
			return new Hasher(
				strategies,
				pepper);
		}
	}
}
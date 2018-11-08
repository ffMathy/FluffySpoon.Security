using System.Collections.Generic;
using FluffySpoon.Security.Hashing.Strategies;

namespace FluffySpoon.Security.Hashing
{
	class HasherFactory : IHasherFactory
	{
		private readonly IEnumerable<IVersionStrategy> strategies;
		private readonly ILatestVersionStrategy latestVersionStrategy;

		public HasherFactory(
			IEnumerable<IVersionStrategy> strategies,
			ILatestVersionStrategy latestVersionStrategy)
		{
			this.strategies = strategies;
			this.latestVersionStrategy = latestVersionStrategy;
		}

		public IHasher Create(string pepper = null)
		{
			return new Hasher(
				strategies,
				latestVersionStrategy,
				pepper);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluffySpoon.Security.Hashing.Strategies;

namespace FluffySpoon.Security.Hashing
{
	class Hasher : IHasher
	{
		private readonly IEnumerable<IVersionStrategy> strategies;
		private readonly ILatestVersionStrategy latestVersionStrategy;

		private readonly string pepper;

		public Hasher(
			IEnumerable<IVersionStrategy> strategies,
			ILatestVersionStrategy latestVersionStrategy,
			string pepper = null)
		{
			this.strategies = strategies;
			this.pepper = pepper;
			this.latestVersionStrategy = latestVersionStrategy;
		}

		public string Generate(string password)
		{
			if (password == null)
				throw new ArgumentNullException(nameof(password));

			return GeneratePrefix(latestVersionStrategy.Prefix) + latestVersionStrategy.Generate(password + pepper);
		}

		public bool IsHashUpToDate(string hash)
		{
			var strategy = FindStrategyForHash(hash);
			return strategy is ILatestVersionStrategy && hash.StartsWith(GeneratePrefix(strategy.Prefix));
		}

		public bool Verify(string hash, string value)
		{
			if (hash == null)
				return false;

			if (value == null)
				return false;

			var strategy = FindStrategyForHash(hash);
			var prefix = GeneratePrefix(strategy.Prefix);

			if (hash.StartsWith(prefix))
				hash = hash.Substring(prefix.Length);

			return strategy.Verify(hash, value + pepper);
		}

		private IVersionStrategy FindStrategyForHash(string hash)
		{
			return strategies
				.OrderByDescending(s => s.Prefix.Length)
				.FirstOrDefault(x => hash.StartsWith(GeneratePrefix(x.Prefix))) ??
				latestVersionStrategy;
		}

		private string GeneratePrefix(string value)
		{
			return $"$fluffy-spoon${value}$";
		}
	}
}

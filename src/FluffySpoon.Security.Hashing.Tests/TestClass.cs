using FluffySpoon.Security.Hashing.Strategies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluffySpoon.Security.Hashing.Tests
{
	[TestClass]
	public class TestClass
	{
		[TestMethod]
		public void TestMethod()
		{
			var services = new ServiceCollection();
			services.AddFluffySpoonHasher("pepsi");

			using(var container = services.BuildServiceProvider())
			using(var scope = container.CreateScope()) {
				var hasher = scope.ServiceProvider.GetRequiredService<IHasher>();

				var password = "my-password";
				var hash = hasher.Generate(password);

				var latestVersionStrategy = scope.ServiceProvider.GetRequiredService<ILatestVersionStrategy>();
				var hashWithoutPrefix = hash.Substring(latestVersionStrategy.Prefix.Length + "$fluffy-spoon$$".Length);

				Assert.IsTrue(hasher.Verify(hashWithoutPrefix, password));
				Assert.IsTrue(hasher.Verify(hash, password));
				Assert.IsFalse(hasher.Verify(hash, "foobar"));

				Assert.IsTrue(hasher.IsHashUpToDate(hash));
				Assert.IsFalse(hasher.IsHashUpToDate(hashWithoutPrefix));
			}
		}
	}
}

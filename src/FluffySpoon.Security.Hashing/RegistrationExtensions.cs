using FluffySpoon.Extensions.MicrosoftDependencyInjection;
using FluffySpoon.Security.Hashing.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Security.Hashing
{
	public static class RegistrationExtensions
	{
		public static void AddFluffySpoonHasher(
			this IServiceCollection serviceCollection,
			string pepper = null)
		{
			serviceCollection.AddAssemblyTypesAsImplementedInterfaces(new RegistrationSettings() {
                Assemblies = new []
                {
                    typeof(RegistrationExtensions).Assembly
                }
            });
            serviceCollection.AddScoped<IVersionStrategy, Argon2VersionStrategy>();
			serviceCollection.AddScoped(p => p.GetRequiredService<IHasherFactory>().Create(pepper));
		}
	}
}

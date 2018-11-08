using FluffySpoon.Extensions.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Security.Hashing
{
	public static class RegistrationExtensions
	{
		public static void AddFluffySpoonHasher(
			this IServiceCollection serviceCollection,
			string pepper = null)
		{
			serviceCollection.AddAssemblyTypesAsImplementedInterfaces(typeof(RegistrationExtensions).Assembly);
			serviceCollection.AddScoped(p => p.GetRequiredService<IHasherFactory>().Create(pepper));
		}
	}
}

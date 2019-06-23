using Isopoh.Cryptography.Argon2;

namespace FluffySpoon.Security.Hashing.Strategies
{
    class Argon2VersionStrategy : ILatestVersionStrategy, IVersionStrategy
    {
        private readonly IArgon2Settings settings;

        public string Prefix => "argon2";

        public Argon2VersionStrategy(IArgon2Settings settings)
        {
            this.settings = settings;
        }

        public string Generate(string password)
        {
            return Argon2.Hash(password, 
                settings.TimeCost ?? 3, 
                settings.MemoryCost ?? 65536, 
                settings.Parallelism ?? 1);
        }

        public bool Verify(string hash, string value)
        {
            return Argon2.Verify(hash, value);
        }
    }

    public interface IArgon2Settings
    {
        int? TimeCost { get; }
        int? Parallelism { get; }
        int? MemoryCost { get; }
    }

    public class Argon2Settings : IArgon2Settings
    {
        public int? TimeCost { get;set; }

        public int? Parallelism { get; set; }

        public int? MemoryCost { get; set; }
    }
}

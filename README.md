# FluffySpoon.Security
Provides mechanisms for hashing.

*Currently uses Argon2 - the best password hashing algorithm as of the 8th of November, 2018.*

This package will always be updated to contain the most secure hashing implementation. Since it prefixes the method used in the hash ([which is considered secure](https://security.stackexchange.com/questions/197236/is-it-bad-practice-to-prefix-my-hash-with-the-algorithm-used)), it can automatically verify hashes that were generated with older implementations of the library.

## Setup
```csharp
services.AddFluffySpoonHasher(/* optional pepper can be provided here */);
```

## Use
Inject an `IHasher` into your class. In the following example, the `IHasher` instance is in the variable `hasher`.

```csharp
var passwordToHash = "my_totally_safe_password";

var hash = hasher.Generate(passwordToHash);

//isHashValid will now be true since the hash corresponds to the entered password.
var isHashValid = hasher.Verify(hash, passwordToHash);

//isHashUpToDate will be true since "hash" was created using the latest hashing mechanism. useful for migrating old hashes.
var isHashUpToDate = hasher.IsHashUpToDate(hash);
```
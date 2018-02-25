# FluffySpoon.Security
Provides mechanisms for hashing.

*Currently uses Argon2 - the best password hashing algorithm as of the 25th of February, 2018.*

## Warning
Do not update this NuGet package - keep it at the same version, as each version may use a different hashing mechanism. This package will always be updated to contain the most secure hashing implementation, so good for new projects.

## Use
```
var hasher = new Hasher();

var passwordToUse = "my_totally_safe_password";

var hash = hasher.Generate(passwordToUse);
var isHashValid = hasher.Verify(hash, passwordToUse);

//isHashValid is now true since the hash corresponds to the entered password.
```
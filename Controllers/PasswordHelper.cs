using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    private const int SaltSize = 16; // Size of the salt in bytes
    private const int HashSize = 20; // Size of the hash in bytes
    private const int Iterations = 10000; // PBKDF2 iterations

    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty.");
        }

        // Generate salt
        byte[] salt = GenerateSalt();
        // Generate hash
        byte[] hash = PBKDF2(password, salt);
        // Combine salt and hash
        byte[] saltAndHash = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, saltAndHash, 0, SaltSize);
        Array.Copy(hash, 0, saltAndHash, SaltSize, HashSize);
        // Return as base64 string
        return Convert.ToBase64String(saltAndHash);
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        // Convert the stored hash from Base64
        byte[] saltAndHash = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[SaltSize];
        byte[] storedHashBytes = new byte[HashSize];
        Array.Copy(saltAndHash, 0, salt, 0, SaltSize);
        Array.Copy(saltAndHash, SaltSize, storedHashBytes, 0, HashSize);
        // Compute the hash of the provided password using the stored salt
        byte[] hash = PBKDF2(password, salt);
        // Verify that the computed hash matches the stored hash
        return AreHashesEqual(hash, storedHashBytes);
    }

    private static byte[] PBKDF2(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA1))
        {
            return pbkdf2.GetBytes(HashSize);
        }
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private static bool AreHashesEqual(byte[] hash1, byte[] hash2)
    {
        if (hash1.Length != hash2.Length)
        {
            return false;
        }
        for (int i = 0; i < hash1.Length; i++)
        {
            if (hash1[i] != hash2[i])
            {
                return false;
            }
        }
        return true;
    }
}

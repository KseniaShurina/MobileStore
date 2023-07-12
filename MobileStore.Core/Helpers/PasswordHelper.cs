using System.Security.Cryptography;
using System.Text;

namespace MobileStore.Core.Helpers;

internal static class PasswordHelper
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public static string HashPassword(string password, string salt)
    {
        var saltBytes = Encoding.UTF8.GetBytes(salt);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            saltBytes,
            Iterations,
            HashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }
}
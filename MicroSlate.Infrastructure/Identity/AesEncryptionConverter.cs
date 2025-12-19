using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text;

namespace MicroSlate.Infrastructure.Identity
{


    public class AesEncryptionConverter : ValueConverter<string, string>
    {
        public AesEncryptionConverter(AesCryptoProvider provider)
            : base(
                v => EncryptString(v, provider),
                v => DecryptString(v, provider))
        {
        }

        private static string EncryptString(string? plainText, AesCryptoProvider provider)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText ?? string.Empty;

            // Convert string to bytes
            var plainBytes = Encoding.UTF8.GetBytes(plainText);


            // Encrypt
            var encryptedBytes = provider.Encrypt(plainBytes);

            // Convert to Base64 string for storage
            return Convert.ToBase64String(encryptedBytes);
        }

        private static string DecryptString(string? encryptedText, AesCryptoProvider provider)
        {

            if (string.IsNullOrEmpty(encryptedText))
                return encryptedText ?? string.Empty;



            // Convert Base64 string back to bytes
            var encryptedBytes = Convert.FromBase64String(encryptedText);

            // Decrypt
            var plainBytes = provider.Decrypt(encryptedBytes);

            // Convert bytes to string
            return Encoding.UTF8.GetString(plainBytes);
        }
    }

}

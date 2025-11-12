using Genius.Application.Abstractions.Services;
using System.Security.Cryptography;
using System.Text;

namespace Genius.Infraestructure.Services
{
    public class PasswordHasherService : IEncryptionService
    {
        private readonly string _encryptionKey;


        public PasswordHasherService(string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptionKey))
                throw new ArgumentException("A chave de criptografia não pode ser nula ou vazia");

            _encryptionKey = encryptionKey;
        }


        [Obsolete("Obsolete")]
        public string DecryptData(byte[] encryptedData) => DecryptData(encryptedData, _encryptionKey);

        [Obsolete("Obsolete")]
        public string DecryptData(byte[] encryptedData, string key)
        {
            if (encryptedData == null || encryptedData.Length == 0)
                throw new ArgumentException("Dados criptografados inválidos");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("A chave não pode ser vazia");

            using var tdes = new TripleDESCryptoServiceProvider();
            using var memoryStream = new MemoryStream(encryptedData);

            var aKey = Encoding.ASCII.GetBytes(key);
            var aIV = new byte[8];

            tdes.Padding = PaddingMode.Zeros;

            using var cryptoStream = new CryptoStream(
                memoryStream,
                tdes.CreateDecryptor(aKey, aIV),
                CryptoStreamMode.Read);

            using var resultStream = new MemoryStream();
            cryptoStream.CopyTo(resultStream);

            // Usa Encoding.Unicode para decodificar (igual ao original)
            return Encoding.Unicode.GetString(resultStream.ToArray()).TrimEnd('\0');
        }

        [Obsolete("Obsolete")]
        public byte[] EncryptData(string data) => EncryptData(data, _encryptionKey);


        [Obsolete("Obsolete")]
        public byte[] EncryptData(string data, string key)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentException("Os dados não podem ser vazia");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("A chave não pode ser vazia");

            using var tdes = new TripleDESCryptoServiceProvider();
            using var strmEncrypted = new MemoryStream();

            // Cria os streams iniciais (fiel ao original VB)
            strmEncrypted.SetLength(0);


            // Obtem a chave do parâmetro (exatamente como no VB)
            var aKey = Encoding.ASCII.GetBytes(key);
            var aIV = new byte[8];

            tdes.Padding = PaddingMode.Zeros;

            var encStream = new CryptoStream(
                strmEncrypted,
                tdes.CreateEncryptor(aKey, aIV),
                CryptoStreamMode.Write);

            // Usa Encoding.Unicode (UTF-16LE) como no VB
            var aBytes = Encoding.Unicode.GetBytes(data);

            encStream.Write(aBytes, 0, aBytes.Length);
            encStream.FlushFinalBlock();
            strmEncrypted.Seek(0, SeekOrigin.Begin);

            aBytes = strmEncrypted.ToArray();

            encStream.Close();

            return aBytes;
        }
    }
}

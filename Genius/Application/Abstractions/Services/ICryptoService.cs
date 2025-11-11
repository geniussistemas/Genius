namespace Genius.Application.Abstractions.Services;

public interface ICryptoService
{
    byte[] EncryptData(string data);
    byte[] EncryptData(string data, string key);

    string DecryptData(byte[] encryptedData);
    string DecryptData(byte[] encryptedData, string key);
}
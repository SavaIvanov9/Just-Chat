namespace JustChat.Domain.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string text, string key);
        string Decrypt(string text, string key);
    }
}

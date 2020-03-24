namespace JustChat.Application.Interfaces.Services
{
    public interface IHashingService
    {
        string GenerateHash(string text, string saltString);
        string GenerateHash(string text);
    }
}

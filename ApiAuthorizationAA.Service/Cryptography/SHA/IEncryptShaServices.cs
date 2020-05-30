
namespace ApiAuthorizationAA.Service.Cryptography.SHA
{
    using ApiAuthotizationAA.Model.Entities.Cryptography;

    public interface IEncryptShaServices
    {
        PasswordHashContainerEntity CreateHash(string password);

        byte[] CreateHash(string password, byte[] salt);

        bool ValidatePassword(string password, byte[] salt, byte[] correctHash);

        bool CompareHashes(byte[] array1, byte[] array2);
    }
}
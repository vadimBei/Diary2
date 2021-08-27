namespace Common.System.Interfaces
{
    public interface IAesCryptoProviderService
    {
        byte[] GenerateKey();
        byte[] GenerateIV();
        byte[] EncryptValue(string value, byte[] key, byte[] iv);
        string DecryptValue(byte[] value, byte[] key, byte[] iv);
    }
}

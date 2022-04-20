using System;
using System.IO;
using System.Linq;

namespace HttpD
{
    public class MiniHttps
    {
        private byte[] _key;
        private HttpClientStream _client;

        public MiniHttps(string url, string key)
        {
            _client = new HttpClientStream(url);
            _key = System.Text.Encoding.ASCII.GetBytes(key);
        }

        public byte[] EncryptFullMessage(string message)
        {
            byte[] encryptedMessage = new byte[0];
            byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);
            int messageLength = messageBytes.Length;
            int blockCount = messageLength / 16;
            int lastBlockLength = messageLength % 16;
            int i = 0;
            for (i = 0; i < blockCount; i++)
            {
                byte[] block = new byte[16];
                Array.Copy(messageBytes, i * 16, block, 0, 16);
                byte[] encryptedBlock = Encryption.Encrypt(block, _key);
                encryptedMessage = encryptedMessage.Concat(encryptedBlock).ToArray();
            }
            if (lastBlockLength > 0)
            {
                byte[] lastBlock = new byte[lastBlockLength];
                Array.Copy(messageBytes, i * 16, lastBlock, 0, lastBlockLength);
                byte[] encryptedLastBlock = Encryption.Encrypt(lastBlock, _key);
                encryptedMessage = encryptedMessage.Concat(encryptedLastBlock).ToArray();
            }
            return encryptedMessage;
        }

        public string DecryptFullCipher(byte[] cipher)
        {
            //Decrypt the message
            byte[] decryptedMessage = new byte[0];
            int blockCount = cipher.Length / 16;
            int lastBlockLength = cipher.Length % 16;
            int i = 0;
            for (i = 0; i < blockCount; i++)
            {
                byte[] block = new byte[16];
                Array.Copy(cipher, i * 16, block, 0, 16);
                byte[] decryptedBlock = Encryption.Decrypt(block, _key);
                decryptedMessage = decryptedMessage.Concat(decryptedBlock).ToArray();
            }
            if (lastBlockLength > 0)
            {
                byte[] lastBlock = new byte[lastBlockLength];
                Array.Copy(cipher, i * 16, lastBlock, 0, lastBlockLength);
                byte[] decryptedLastBlock = Encryption.Decrypt(lastBlock, _key);
                decryptedMessage = decryptedMessage.Concat(decryptedLastBlock).ToArray();
            }
            return System.Text.Encoding.ASCII.GetString(decryptedMessage);
        }

        public string EncryptAndSend(string message)
        {
            byte[] encryptedMessage = EncryptFullMessage(message);
            StreamReader stream = _client.SendMessage(System.Text.Encoding.ASCII.GetString(encryptedMessage));
            byte[] encryptedAnswer = _client.ResponseFromStream(stream).Select(c => (byte)c).ToArray();
            return DecryptFullCipher(encryptedAnswer);

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionTool.Helpers
{
    /// bron : https://www.youtube.com/watch?v=LOmgFxPHop0
    public static class AESHelper
    {
        public static AesCryptoServiceProvider CryptoServicePr = new AesCryptoServiceProvider()
        {
            BlockSize = 128,
            KeySize = 128,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
        };
        public static void Init()
        {
            if (CryptoServicePr.IV == null)
            {
                CryptoServicePr.GenerateIV();
                CryptoServicePr.GenerateKey();
            }
            else
            {

            }
        }
        public static string GetKey()
        {
            string keystr = "";
            byte[] key = CryptoServicePr.Key;
            foreach (var item in key)
            {
                keystr += item;
            }
            return keystr;
        }
        public static string GetIV()
        {
            string ivstr = "";
            byte[] IV = CryptoServicePr.IV;
            foreach (var item in IV)
            {
                ivstr += item;
            }
            return ivstr;
        }
        public static string SetKey()
        {
            string keystr = "";
            byte[] key = CryptoServicePr.Key;
            foreach (var item in key)
            {
                keystr += item;
            }
            return keystr;
        }
        public static string SetIV()
        {
            string ivstr = "";
            byte[] IV = CryptoServicePr.IV;
            foreach (var item in IV)
            {
                ivstr += item;
            }
            return ivstr;
        }

        public static string Encrypt(string plaintext)
        {
            ICryptoTransform encryptor = CryptoServicePr.CreateEncryptor();
            byte[] byteCiphertext = encryptor.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(plaintext),0, plaintext.Length);
            string ciphertext = Convert.ToBase64String(byteCiphertext);
            return ciphertext;
        }
        public static string Decrypt(string ciphertext)
        {
            ICryptoTransform decryptor = CryptoServicePr.CreateDecryptor();
            byte[] enc_bytes = Convert.FromBase64String(ciphertext);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(enc_bytes, 0,enc_bytes.Length);
            string plaintext = ASCIIEncoding.ASCII.GetString(decryptedBytes);
            return plaintext;
        }

        /*
public static byte[] EncryptToBytes(string plainText, byte[] Key, byte[] IV)
{
    // Check arguments.
    if (plainText == null || plainText.Length <= 0)
        throw new ArgumentNullException("plainText");
    if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");
    byte[] encrypted;

    // Create an Aes object
    // with the specified key and IV.
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = Key;
        aesAlg.IV = IV;
        aesAlg.Padding = PaddingMode.Zeros;

        aesAlg.Mode = CipherMode.CBC;

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
    }

    // Return the encrypted bytes from the memory stream.
    return encrypted;
}

public static string DecryptToString(byte[] cipherText, byte[] Key, byte[] IV)
{
    // Check arguments.
    if (cipherText == null || cipherText.Length <= 0)
        throw new ArgumentNullException("cipherText");
    if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");

    // Declare the string used to hold
    // the decrypted text.
    string plaintext = "";

    // Create an Aes object
    // with the specified key and IV.
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = Key;
        aesAlg.IV = IV;
        aesAlg.Padding = PaddingMode.Zeros; 
        aesAlg.Mode = CipherMode.CBC;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {

                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
    }

    return plaintext;
}*/// old not so good working methods

    }
}

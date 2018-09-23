using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace GUISimpleTCPClient
{
    class Symmetric_Cryptography
    {




        public byte[] dycryptFile(byte[] file, byte[] key, byte[] iv)
        {
            byte[] decryptedFile = Decrypt(file, key, iv, "Rijndael");
            return decryptedFile;
        }


        public static string Encrypt(byte[] plainText, string Key, string IV, string algorithmName)
        {
            SymmetricAlgorithm symmetricAlgorithm =

               SymmetricAlgorithm.Create(algorithmName);

            symmetricAlgorithm.Key = Encoding.ASCII.GetBytes(Key);
            symmetricAlgorithm.IV = Encoding.ASCII.GetBytes(IV);
            ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();

            MemoryStream t = new MemoryStream();

            CryptoStream cryptoStream =

                new CryptoStream(t, transform, CryptoStreamMode.Write);
            cryptoStream.Write(plainText, 0, plainText.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            return Convert.ToBase64String(t.ToArray());
        }
        public static byte[] Decrypt(byte[] cipherText, string Key, string IV, string algorithmName)
        {
            SymmetricAlgorithm symmetricAlgorithm =

               SymmetricAlgorithm.Create(algorithmName);
            symmetricAlgorithm.Key = Encoding.ASCII.GetBytes(Key);
            symmetricAlgorithm.IV = Encoding.ASCII.GetBytes(IV);
            ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor();

            MemoryStream t = new MemoryStream();

            CryptoStream cryptoStream =

                new CryptoStream(t, transform, CryptoStreamMode.Write);
            cryptoStream.Write(cipherText, 0, cipherText.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            return t.ToArray();
        }
        public static string Encrypt(byte[] plainText, byte[] Key, byte[] IV, string algorithmName)
        {
            SymmetricAlgorithm symmetricAlgorithm =

               SymmetricAlgorithm.Create(algorithmName);

            symmetricAlgorithm.Key = Key;
            symmetricAlgorithm.IV = IV;
            ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();

            MemoryStream t = new MemoryStream();

            CryptoStream cryptoStream =

                new CryptoStream(t, transform, CryptoStreamMode.Write);
            cryptoStream.Write(plainText, 0, plainText.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            return Convert.ToBase64String(t.ToArray());
        }
        public static byte[] Decrypt(byte[] cipherText, byte[] Key, byte[] IV, string algorithmName)
        {
            SymmetricAlgorithm symmetricAlgorithm =

               SymmetricAlgorithm.Create(algorithmName);
            symmetricAlgorithm.Key = Key;
            symmetricAlgorithm.IV = IV;
            ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor();

            MemoryStream t = new MemoryStream();

            CryptoStream cryptoStream =

                new CryptoStream(t, transform, CryptoStreamMode.Write);
            cryptoStream.Write(cipherText, 0, cipherText.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            return t.ToArray();
        }
        public static byte[] GenerateKey(string symmetricAlgorithmName)
        {
            SymmetricAlgorithm s = SymmetricAlgorithm.Create(symmetricAlgorithmName);


            s.GenerateKey();
            return s.Key;
        }
        public static byte[] GenerateIV(string symmetricAlgorithmName)
        {
            SymmetricAlgorithm s = SymmetricAlgorithm.Create(symmetricAlgorithmName);
            s.GenerateIV();
            return s.IV;
        }
    }
}

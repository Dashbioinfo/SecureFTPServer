using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using System.IO;

namespace RSAExample
{
    class AsymmetricEncryptionUtility
    {
        public string GenerateKey(string targetFile)
        {
            RSACryptoServiceProvider Algorithm = new RSACryptoServiceProvider();
            // Save the private key
            string CompleteKey = Algorithm.ToXmlString(true);
            byte[] KeyBytes = Encoding.UTF8.GetBytes(CompleteKey);
            /* KeyBytes = ProtectedData.Protect(KeyBytes,
             null, DataProtectionScope.LocalMachine);*/
            using (FileStream fs = new FileStream("PrivateKey.xml", FileMode.Create))
            {
                fs.Write(KeyBytes, 0, KeyBytes.Length);
                fs.Close();
            }
            using (FileStream fs = new FileStream("PublicKey.xml", FileMode.Create))
            {
                string pk = Algorithm.ToXmlString(false);
                byte[] d = Encoding.UTF8.GetBytes(pk);
                fs.Write(d, 0, d.Length);
                fs.Close();
            }
            // Return the public key
            return Algorithm.ToXmlString(false);
        }

        public string GenerateprivateKey(string targetFile)
        {
            RSACryptoServiceProvider Algorithm = new RSACryptoServiceProvider();
            // Save the private key
            string CompleteKey = Algorithm.ToXmlString(true);
            byte[] KeyBytes = Encoding.UTF8.GetBytes(CompleteKey);
            /* KeyBytes = ProtectedData.Protect(KeyBytes,
             null, DataProtectionScope.LocalMachine);*/

            using (FileStream fs = new FileStream(targetFile, FileMode.Open))
            {
                string pk = Algorithm.ToXmlString(true);
                byte[] d = Encoding.UTF8.GetBytes(pk);
                fs.Read(KeyBytes, 0, (int)fs.Length);
                fs.Close();
            }
            // Return the public key
            return Algorithm.ToXmlString(true);
        }

        private void ReadKey(RSACryptoServiceProvider algorithm, string keyFile)
        {
            byte[] KeyBytes;
            FileStream fs = new FileStream(keyFile, FileMode.Open);

            KeyBytes = new byte[fs.Length];
            fs.Read(KeyBytes, 0, (int)fs.Length);
            fs.Close();
            /*  KeyBytes = ProtectedData.Unprotect(KeyBytes,
                null, DataProtectionScope.LocalMachine);*/
            algorithm.FromXmlString(Encoding.UTF8.GetString(KeyBytes));
        }


        public string DecryptData(byte[] data, string keyFile)
        {
            RSACryptoServiceProvider Algorithm = new RSACryptoServiceProvider();
            ReadKey(Algorithm, keyFile);

            //Algorithm.FromXmlString(keyFile);
            byte[] ClearData = Algorithm.Decrypt(data, true);
            return Convert.ToString(Encoding.UTF8.GetString(ClearData));
        }
    }
}

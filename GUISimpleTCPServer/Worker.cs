using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using GUISimpleTCPServer;
using GUISimpleTCPClient;
using RSAExample;

namespace Worker
{
    class Worker
    {
        TcpClient client = null;
        Symmetric_Cryptography sy_C = new Symmetric_Cryptography();
        AsymmetricEncryptionUtility asy_crypt = new AsymmetricEncryptionUtility();

        public Worker(TcpClient client)
        {



            this.client = client;

        }

        public void takeClient()
        {

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine("Client Connected");

            writer.Flush();
            String line = null;
            while ((line = reader.ReadLine()).Length != 0)
            {
                string keyencrypted = null;
                string IV = null;
                string filencrypted = null;
                keyencrypted = reader.ReadLine();// received encrypted key
                IV = reader.ReadLine();
                filencrypted = reader.ReadLine();// received encrypted file
                byte[] receiveFile = Convert.FromBase64String(filencrypted);
                byte[] sessionkey = Convert.FromBase64String(keyencrypted);
                byte[] sessioniv = Convert.FromBase64String(IV);

                string IVfile = asy_crypt.DecryptData(sessioniv, "PrivateKey.xml");
                string keyfile = asy_crypt.DecryptData(sessionkey, "PrivateKey.xml");

                writer.WriteLine("received");
                sessionkey = Convert.FromBase64String(keyfile);
                byte[] file_decrypted = sy_C.dycryptFile(receiveFile, sessionkey, Convert.FromBase64String(IVfile));
                Server.GUI.show("file recerived");
                File.WriteAllBytes(line, file_decrypted);
                Server.GUI.show("Recived from client" + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString() + "  :" + line);



                writer.Flush();
            }
            Server.GUI.show("Client " + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString() + "disconnected");
            client.Close();

        }
        public byte[] hashMAC(string massage, string secretKey)
        {
            return null;
        }
    }

}

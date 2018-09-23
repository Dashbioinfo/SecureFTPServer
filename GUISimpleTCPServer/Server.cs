using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace GUISimpleTCPServer
{
    class Server
    {
        TcpListener server = null;
        public static Form1 GUI;
        public Server(String ip, string port, Form1 f)
        {
            server = new TcpListener(IPAddress.Parse(ip), Convert.ToInt32(port));
            GUI = f;
        }
        public void run()
        {
            while (true)
            {
                server.Start(3);
                GUI.show("Waiting for Client...");
                TcpClient client = server.AcceptTcpClient();
                GUI.show("Connected with a client " + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString());
                Worker.Worker worker = new Worker.Worker(client);

                Thread th = new Thread(new ThreadStart(worker.takeClient));
                th.Start();

            }
        }
    }


}

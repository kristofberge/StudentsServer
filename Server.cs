using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentsServer
{
    class Server
    {

        private IPAddress _ipAddress;
        private AbstractResponseHelper _helper;

        private bool _continue;

        public Server(string ipAddress, AbstractResponseHelper helper)
        {
            try
            {
                _ipAddress = IPAddress.Parse(ipAddress);
            }
            catch (Exception)
            {
                throw;
            }
            this._helper = helper;
        }

        public void startListening(int port) {
            try
            {
                _continue = true;
                while (_continue)
                {
                    Thread.Sleep(10);
                    TcpListener listener = new TcpListener(_ipAddress, port);

                    listener.Start();

                    Console.WriteLine("Listening on port " + port);
                    Console.WriteLine("The local End point is: " + listener.LocalEndpoint);

                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Connection accepted from " + client.ToString());

                    byte[] bytes = new byte[256];

                    NetworkStream stream = client.GetStream();
                    stream.Read(bytes, 0, bytes.Length);
                    _helper.Respond(client, stream, bytes);

                    stream.Close();
                    client.Close();
                    listener.Stop();
                }
                Console.WriteLine("server stopped");
                
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void RequestStop() 
        {
            Console.WriteLine("stop requested: " + _ipAddress.ToString());
            _continue = false;
        }
    }
}

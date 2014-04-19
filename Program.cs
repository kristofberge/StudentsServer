using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace StudentsServer
{
    class Program
    {
        static Server server;
        static Thread serverThread;
        
        static void Main(string[] args)
        {
            server = new Server("192.168.0.105", new EchoResponseHelper());
            serverThread = new Thread(new ThreadStart(() => server.startListening(666)));
            serverThread.Start();
            Client.start();
        }
    }
}

using System.Net;
using System.Net.Sockets;

var localPort = 1488;
var ipAddress = IPAddress.Parse("127.0.0.1");

TcpListener listener = new(ipAddress, localPort);

listener.Start();

Thread t = new(new ThreadStart(Service));
t.Start();

void Service()
{
    Socket socket = listener.AcceptSocket();

    try
    {
        Stream s = new NetworkStream(socket);
        StreamReader sr = new StreamReader(s);
        StreamWriter sw = new StreamWriter(s);
        sw.AutoFlush = true;

        while (true)
        {
            string? text = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(text))
            {
                sw.WriteLine("no word recieved\n");
                continue;
            }

            Console.WriteLine(WordSorter.Core.WordSorter.ParseText(text));
            sw.WriteLine(WordSorter.Core.WordSorter.ParseText(text));
        }
    }
    catch (Exception e)
    {
    }

    socket.Close();
}
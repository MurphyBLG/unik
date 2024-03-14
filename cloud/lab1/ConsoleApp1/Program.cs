using System.Net;
using System.Net.Sockets;

Dictionary<string, string> employees =
 new Dictionary<string, string>()
 {
 {"john", "manager"},
 {"jane", "steno"},
 {"jim", "clerk"},
 {"jack", "salesman"}
 };

var localPort = 1488;
var ipAddress = IPAddress.Parse("127.0.0.1");

TcpListener listener = new(ipAddress, localPort);

listener.Start();

Thread t = new(new ThreadStart(Service));
t.Start();

void Service()
{
    while (true)
    {
        Socket socket = listener.AcceptSocket();

        try
        {
            Stream s = new NetworkStream(socket);
            StreamReader sr = new StreamReader(s);
            StreamWriter sw = new StreamWriter(s);
            sw.AutoFlush = true; // enable automatic flushing
            sw.WriteLine("{0} Employees available",
            employees.Count);

            while (true)
            {
                string name = sr.ReadLine();
                if (name == "" || name == null) break;
                string? job = null;
                employees.TryGetValue(name, out job);
                if (job is null) job = "No such employee";
                sw.WriteLine(job);
            }
            s.Close();
        }
        catch (Exception e)
        {
#if LOG
 Console.WriteLine(e.Message);
#endif
        }
#if LOG
        Console.WriteLine("Disconnected: {0}", 
        soc.RemoteEndPoint);
#endif
        socket.Close();
    }
}




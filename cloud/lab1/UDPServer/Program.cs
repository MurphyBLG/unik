using System.Net.Sockets;
using System.Net;
using System.Text;

Dictionary<string, string> employees =
 new Dictionary<string, string>()
 {
 {"john", "manager"},
 {"jane", "steno"},
 {"jim", "clerk"},
 {"jack", "salesman"}
 };

var port = 1488;
UdpClient udpc = new(port);
Console.WriteLine("Server started, servicing on port "+port.ToString());
IPEndPoint? ep = null;
while (true)
{
    var rdata = udpc.Receive(ref ep);
    var name = Encoding.ASCII.GetString(rdata);
    string? job = null;
    employees.TryGetValue(name, out job);
    if (job == null) job = "No such employee";
    byte[] sdata = Encoding.ASCII.GetBytes(job);
    udpc.Send(sdata, sdata.Length, ep);
}
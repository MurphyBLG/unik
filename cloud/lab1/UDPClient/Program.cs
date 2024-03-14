using System.Net.Sockets;
using System.Net;
using System.Text;

string ipadress = "127.0.0.1";
Int32 port = 1488;
if (args.Length > 0)
    ipadress = args[0];
UdpClient udpc = new UdpClient(ipadress, port);
IPEndPoint? ep = null;
while (true)
{
    Console.Write("Name: ");
    string name = Console.ReadLine();
    if (name == "") break;
    byte[] sdata = Encoding.ASCII.GetBytes(name);
    udpc.Send(sdata, sdata.Length);
    byte[] rdata = udpc.Receive(ref ep);
    string job = Encoding.ASCII.GetString(rdata);
    Console.WriteLine(job);
}
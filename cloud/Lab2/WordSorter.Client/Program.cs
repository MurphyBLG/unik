using System.Net.Sockets;

var localPort = 1488;
var ipAddress = "127.0.0.1";
TcpClient client = new TcpClient(ipAddress, localPort);

try
{
    Stream s = client.GetStream();
    StreamReader sr = new StreamReader(s);
    StreamWriter sw = new StreamWriter(s);
    sw.AutoFlush = true;
    while (true)
    {
        Console.Write("Text: ");
        string? text = Console.ReadLine();
        sw.WriteLine(text);
        if (text == "") break;
        Console.WriteLine(sr.ReadLine());
    }
    s.Close();
}
finally
{
    client.Close();
}
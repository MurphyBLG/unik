using System.Net.Sockets;

var localPort = 1488;
var ipAddress = "127.0.0.1";
var client = new TcpClient(ipAddress, localPort);

try
{
    Stream s = client.GetStream();
    var sr = new StreamReader(s);
    var sw = new StreamWriter(s);
    sw.AutoFlush = true;
    while (true)
    {
        Console.Write("Text: ");
        var text = Console.ReadLine();
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
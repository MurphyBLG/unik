using System.Threading.Channels;

namespace Modelation;

public class Phone : Consumer
{
    public Phone(ChannelReader<int> reader, int delay) : base(reader, delay)
    {
    }
}

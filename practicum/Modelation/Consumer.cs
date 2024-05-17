using System.Threading.Channels;

namespace Modelation;
public class Consumer(ChannelReader<int> reader, int delay)
{
    public int ConsumedMessages { get; private set; } = 0;

    public async Task ConsumeData(CancellationToken cancellationToken)
    {
        while (await reader.WaitToReadAsync(cancellationToken))
        {
            if (!reader.TryRead(out var timeString)) 
                continue;
            
            ConsumedMessages++;
            await Task.Delay(delay, cancellationToken);
        }
    }
}

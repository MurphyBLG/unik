using System.Threading.Channels;

namespace Modelation;

internal class Producer
{
    private readonly ChannelWriter<int> _writer;
    private readonly int _delay;
    private readonly int _countOfMessageProducing;

    public Producer(ChannelWriter<int> writer, int delay, int countOfMessageProducing)
    {
        _writer = writer;
        _delay = delay;
        _countOfMessageProducing = countOfMessageProducing;
    }

    public async Task ProduceAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            for (int i = 0; i < _countOfMessageProducing; i++)
                _writer.WriteAsync(i, cancellationToken);

            await Task.Delay(_delay, cancellationToken);    
        }
    }
}
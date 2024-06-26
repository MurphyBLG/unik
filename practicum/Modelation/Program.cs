﻿using Modelation;
using System.Threading.Channels;
using System.Timers;

public class Program
{
    private const int M = 10;
    private const int ProcessingTimeInMinutes = 20;
    private const int CountOfPhones = 3; // n

    private static readonly CancellationToken CancellationToken;
    private static readonly CancellationTokenSource CancellationTokenSource;

    static Program()
    {
        CancellationTokenSource = new CancellationTokenSource();
        CancellationToken = CancellationTokenSource.Token;
    }

<<<<<<< HEAD
    private static void Main(string[] args)
=======
    private static async Task Main(string[] args)
>>>>>>> 4fe7ca2d208111c4265f37389a2bc4e006dd43a7
    {
        var timer = new System.Timers.Timer(600);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = false;
        var messageQueue = Channel.CreateUnbounded<int>();
        var producer = new Producer(messageQueue.Writer, 1, Decimal.ToInt32(Math.Round((90m + M) / 60)));
        var consumers = CreatePhones(CountOfPhones, messageQueue);

        try
        {
            var runningTasks = RunConsumers(consumers, CancellationToken);
            runningTasks.Add(producer.ProduceAsync(CancellationToken));
            timer.Start();

            Task.WaitAll(runningTasks.ToArray(), CancellationToken);
        }
        catch (OperationCanceledException)
        {
            var produced = 90 + M;
            var processed = (decimal)consumers.Sum(phone => phone.ConsumedMessages);
            var rejected = produced - processed;
            var rejectionProbability = rejected / produced;
            var relativeThroughput = 1 - rejectionProbability;
            var absoluteThroughput = relativeThroughput * produced;
            Console.WriteLine($"Produced: {produced}");
            Console.WriteLine($"Processed: {processed}");
            Console.WriteLine($"Rejected: {rejected}");
            Console.WriteLine($"Rejection probability: {rejectionProbability}");
            Console.WriteLine($"Q: {relativeThroughput}");
            Console.WriteLine($"A: {absoluteThroughput}");
        }
    }

    private static void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        CancellationTokenSource.Cancel();
    }

    private static List<Phone> CreatePhones(int countOfPhones, Channel<int> messageQueue)
    {
        var phones = new List<Phone>();
        for (var i = 0; i < countOfPhones; i++)
            phones.Add(new Phone(messageQueue.Reader, ProcessingTimeInMinutes));

        return phones;
    }

    private static List<Task> RunConsumers<T>(List<T> consumers, CancellationToken cancellationToken) where T : Consumer
    {
        var consumersTask = new List<Task>();
        foreach (var consumer in consumers)
            consumersTask.Add(consumer.ConsumeData(cancellationToken));

        return consumersTask;
    }
}
﻿using Grpc.Net.Client;
using HelloWorldClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:32773");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "kell" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
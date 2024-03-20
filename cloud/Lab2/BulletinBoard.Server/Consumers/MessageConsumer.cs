using AutoMapper;
using BulletinBoard.Core.Models;
using BulletinBoard.Server.Contracts;
using BulletinBoard.Server.Persistance;
using MassTransit;

namespace BulletinBoard.Server.Consumers;

public class MessageConsumer(MainContext repostory, IMapper mapper) : IConsumer<GetAllAdsContract>, IConsumer<CreateAdContract>
{
    public async Task Consume(ConsumeContext<GetAllAdsContract> context)
    {
        await context.RespondAsync(new GetAllAdsResponse
        {
            Message = string.Join(";\n", repostory.Ads.Select(s => s.Message))
        });
    }

    public async Task Consume(ConsumeContext<CreateAdContract> context)
    {
        repostory.Ads.Add(mapper.Map<Advertisement>(context.Message));
        await repostory.SaveChangesAsync();
        await context.RespondAsync(new CreateAdResponse
        {
            Message = $"Message added: '{context.Message.Advertisement}'",
        });
    }
}

using System;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    private readonly IMapper _mapper;

    public AuctionDeletedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        System.Console.WriteLine("--> Consuming AuctionDeleted.");

        var result = await DB.DeleteAsync<Item>(context.Message.Id);

        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionDeleted), "Problem deleting auction in Mongodb");
        }
    }
}

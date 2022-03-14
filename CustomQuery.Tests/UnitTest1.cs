using System.Linq;
using System.Threading.Tasks;
using CustomQuery.Data;
using CustomQuery.Entities;
using CustomQuery.Extensions;
using CustomQuery.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CustomQuery.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        ISessionService sessionService = new SessionService();
        
        var contextFactory = new ContextFactory();
        var context = contextFactory.GetNewDbContext();
        
        context.UserBsts.Add(new UserBst
        {
            UserId = 1,
            BstId = 1
        });

        context.Shipments.Add(new Shipment
        {
            Name = "test",
            Description = "test",
            BstId = 1
        });
        
        context.Shipments.Add(new Shipment
        {
            Name = "test",
            Description = "test",
            BstId = 2
        });

        await context.SaveChangesAsync();
        
        var shipments = await context.Shipments.ToListAsync();

        var shipmentsFiltered = await context.Shipments
            .FilterByBst(
                // The Property to check
                x => x.BstId, 
                
                // Pass in session service to get the current user
                sessionService, 
                
                // Pass in a context to get the current user's bst ids
                context).ToListAsync();

        Assert.AreEqual(2, shipments.Count);
        Assert.AreEqual(1, shipmentsFiltered.Count);
    }
}
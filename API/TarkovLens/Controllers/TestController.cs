using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Enums.Services.TarkovDatabase;
using TarkovLens.Services;

namespace TarkovLens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDocumentSession session;
        private readonly ILogger<TestController> _logger;
        private ITarkovDatabaseService _tarkovDatabaseService;
        private ITarkovMarketService _tarkovMarketService;
        private IItemUpdaterService _itemUpdaterService;

        // TEST CONTROLLER FOR TESTING FUNCTIONS
        // SHOULD BE DELETED AT SOME POINT
        public TestController(ILogger<TestController> logger, IDocumentSession documentSession,
                              ITarkovDatabaseService tarkovDatabaseService, ITarkovMarketService tarkovMarketService,
                              IItemUpdaterService itemUpdaterService)
        {
            _logger = logger;
            session = documentSession;
            _tarkovDatabaseService = tarkovDatabaseService;
            _tarkovMarketService = tarkovMarketService;
            _itemUpdaterService = itemUpdaterService;
        }

        [HttpGet("create")]
        public Test Create()
        {
            var test = new Test()
            {
                TestString = "blah blah blah"
            };

            session.Store(test);
            session.SaveChanges();

            return test;
        }

        [HttpGet("tarkov-database")]
        public async Task<IActionResult> TarkovDatabaseKinds()
        {
            var token = await _tarkovDatabaseService.GetNewAuthTokenAsync();
            var data = await _tarkovDatabaseService.GetItemsByKindAsync<Ammunition>(token, KindOfItem.Ammunition);
            return Ok(data);
        }

        [HttpGet("tarkov-market")]
        public async Task<IActionResult> TarkovMarketItems()
        {
            var data = await _tarkovMarketService.GetAllItemsAsync();
            return Ok(data);
        }

        [HttpGet("update-items")]
        public async Task<IActionResult> UpdateItems()
        {
            await _itemUpdaterService.UpdateAllItemsAsync();
            return Ok();
        }
    }

    public class Test
    {
        public string TestString { get; set; }
    }
}

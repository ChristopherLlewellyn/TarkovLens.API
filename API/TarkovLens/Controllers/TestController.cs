using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Models.Services.TarkovDatabase;
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

        // TEST CONTROLLER FOR TESTING FUNCTIONS
        // SHOULD BE DELETED AT SOME POINT
        public TestController(ILogger<TestController> logger, IDocumentSession documentSession, ITarkovDatabaseService tarkovDatabaseService)
        {
            _logger = logger;
            session = documentSession;
            _tarkovDatabaseService = tarkovDatabaseService;
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

        [HttpGet("tarkov-database-kinds")]
        public async Task<ItemKindsMetadata> TarkovDatabaseKinds()
        {
            var token = await _tarkovDatabaseService.GetNewAuthToken();
            var kindsMetadata = await _tarkovDatabaseService.GetItemKindsMetadata(token);
            return kindsMetadata;
        }
    }

    public class Test
    {
        public string TestString { get; set; }
    }
}

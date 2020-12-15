using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RavenDbDotNetCore31APITemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDocumentSession session;

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger, IDocumentSession documentSession)
        {
            _logger = logger;
            session = documentSession;
        }

        [HttpGet]
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
    }

    public class Test
    {
        public string TestString { get; set; }
    }
}

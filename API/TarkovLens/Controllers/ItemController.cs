using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Indexes;
using TarkovLens.Interfaces;
using TarkovLens.Services;
using TarkovLens.Services.TarkovDatabase;

namespace TarkovLens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IDocumentSession session;
        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger, IDocumentSession documentSession)
        {
            _logger = logger;
            session = documentSession;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string id, string name)
        {
            IItem item = null;

            if (id.IsNotNullOrEmpty())
            {
                item = session.Load<IItem>(id);
            }
            else if (name.IsNotNullOrEmpty())
            {
                item = session
                    .Query<IItem, Items_ByName_ForAll>()
                    .Search(x => x.Name, $"*{name}*") // The stars (*) on either side mean it can match anywhere in the string
                    .FirstOrDefault();
            }
            else
            {
                return BadRequest("No query parameters provided.");
            }

            return Ok(item);

            /*Item_Smart_Search.Projection result = session
                .Query<Item_Smart_Search.Result, Item_Smart_Search>()
                .Search(x => x.Content, $"{name}*")
                .ProjectInto<Item_Smart_Search.Projection>()
                .FirstOrDefault();*/
        }
    }
}

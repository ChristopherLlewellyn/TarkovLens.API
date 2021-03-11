using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Characters;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Indexes;
using TarkovLens.Interfaces;
using TarkovLens.Services;
using TarkovLens.Services.Item;
using TarkovLens.Services.TarkovDatabase;

namespace TarkovLens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly IDocumentSession session;
        private readonly ILogger<CharacterController> _logger;
        private ICharacterService _characterService;

        public CharacterController(ILogger<CharacterController> logger, IDocumentSession documentSession, ICharacterService characterService)
        {
            _logger = logger;
            session = documentSession;
            _characterService = characterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ICharacter> characters = _characterService.GetAll();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return BadRequest("Missing parameters: \"id\"");
            }

            // When we pass the Id in the route, we should use "-" instead of "/"
            // Then we should convert it back to a RavenId
            id = id.ReplaceFirst("-", "/");

            ICharacter character = _characterService.GetCharacterById(id);
            if (character.IsNull())
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpGet("type/{type}")]
        public IActionResult GetByType(CharacterType type)
        {
            if (type.IsNull())
            {
                return BadRequest("Missing parameters: \"type\"");
            }

            IEnumerable<ICharacter> characters = _characterService.GetCharactersByType(type);
            return Ok(characters);
        }

        [HttpGet("type/combatant")]
        public IActionResult GetCombatants()
        {
            IEnumerable<Combatant> combatants = _characterService.GetCombatants();
            return Ok(combatants);
        }
    }
}

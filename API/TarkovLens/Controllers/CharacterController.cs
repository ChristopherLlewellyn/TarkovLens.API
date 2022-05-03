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
        private ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ICharacter> characters = _characterRepository.GetAll();
            characters = characters.OrderBy(x => x.Name);

            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            // When we pass the Id in the route, we should use "-" instead of "/"
            // Then we should convert it back to a RavenId
            id = id.ReplaceFirst("-", "/");

            ICharacter character = _characterRepository.GetCharacterById(id);
            if (character.IsNull())
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpGet("type/{type}")]
        public IActionResult GetByType(CharacterType type)
        {
            IEnumerable<ICharacter> characters = _characterRepository.GetCharactersByType(type);
            characters = characters.OrderBy(x => x.Name);

            return Ok(characters);
        }

        [HttpGet("type/combatant")]
        public IActionResult GetCombatants()
        {
            IEnumerable<Combatant> combatants = _characterRepository.GetCombatants();
            combatants = combatants.OrderBy(x => x.Name);

            return Ok(combatants);
        }
    }
}

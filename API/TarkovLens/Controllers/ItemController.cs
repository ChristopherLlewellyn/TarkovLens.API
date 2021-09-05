using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class ItemController : ControllerBase
    {
        private readonly IDocumentSession session;
        private readonly ILogger<ItemController> _logger;
        private IItemService _itemService;

        public ItemController(ILogger<ItemController> logger, IDocumentSession documentSession, IItemService itemService)
        {
            _logger = logger;
            session = documentSession;
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_itemService.GetAllItems());
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

            IItem item = _itemService.GetItemById(id);
            if (item.IsNull())
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("bsgid/{id}")]
        public IActionResult GetByBsgId(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return BadRequest("Missing parameters: \"id\"");
            }

            IItem item = _itemService.GetItemByBsgId(id);
            if (item.IsNull())
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            if (name.IsNotNullOrEmpty())
            {
                var items = _itemService.GetItemsByName(name);
                return Ok(items);
            }

            return BadRequest("Missing parameters: \"name\"");
        }

        [HttpGet("kind")]
        public IActionResult GetKinds()
        {
            return Ok(Enum.GetValues(typeof(KindOfItem)));
        }

        /// <summary>
        /// Search items by kind, with optional parameters for filtering by different attributes where possible.
        /// </summary>
        /// <param name="kind">MANDATORY - The kind of item to search for</param>
        /// <param name="name">OPTIONAL - the name of the item</param>
        /// <param name="caliber">OPTIONAL - the caliber of the item</param>
        /// <returns>List of items.</returns>
        [HttpGet("kind/{kind}")]
        public IActionResult Kind(KindOfItem kind, string name = null, string caliber = null)
        {
            if (kind.IsNull())
            {
                return BadRequest("Invalid item kind provided.");
            }

            switch (kind)
            {
                #region Kinds

                case KindOfItem.Ammunition:
                    var ammunitions = new List<Ammunition>();
                    if (caliber.IsNotNullOrEmpty())
                    {
                        ammunitions = _itemService.GetAmmunitionByCaliber(caliber, name);
                    }
                    else
                    {
                        ammunitions = _itemService.GetItemsByKindAndName<Ammunition>(name);
                    }
                    return Ok(ammunitions);

                case KindOfItem.Armor:
                    var armors = _itemService.GetItemsByKindAndName<Armor>(name);
                    return Ok(armors);

                case KindOfItem.Backpack:
                    var backpacks = _itemService.GetItemsByKindAndName<Backpack>(name);
                    return Ok(backpacks);

                case KindOfItem.Barter:
                    var barters = _itemService.GetItemsByKindAndName<Barter>(name);
                    return Ok(barters);

                case KindOfItem.Clothing:
                    var clothings = _itemService.GetItemsByKindAndName<Clothing>(name);
                    return Ok(clothings);

                case KindOfItem.Common:
                    var commons = _itemService.GetItemsByKindAndName<Common>(name);
                    return Ok(commons);

                case KindOfItem.Container:
                    var containers = _itemService.GetItemsByKindAndName<Container>(name);
                    return Ok(containers);

                case KindOfItem.Firearm:
                    var firearms = _itemService.GetItemsByKindAndName<Firearm>(name);
                    return Ok(firearms);

                case KindOfItem.Food:
                    var foods = _itemService.GetItemsByKindAndName<Food>(name);
                    return Ok(foods);

                case KindOfItem.Grenade:
                    var grenades = _itemService.GetItemsByKindAndName<Grenade>(name);
                    return Ok(grenades);

                case KindOfItem.Headphone:
                    var headphones = _itemService.GetItemsByKindAndName<Headphone>(name);
                    return Ok(headphones);

                case KindOfItem.Key:
                    var keys = _itemService.GetItemsByKindAndName<Key>(name);
                    return Ok(keys);

                case KindOfItem.Magazine:
                    var magazines = _itemService.GetItemsByKindAndName<Magazine>(name);
                    return Ok(magazines);

                case KindOfItem.Map:
                    var maps = _itemService.GetItemsByKindAndName<Map>(name);
                    return Ok(maps);

                case KindOfItem.Medical:
                    var medicals = _itemService.GetItemsByKindAndName<Medical>(name);
                    return Ok(medicals);

                case KindOfItem.Melee:
                    var melees = _itemService.GetItemsByKindAndName<Melee>(name);
                    return Ok(melees);

                case KindOfItem.Modification:
                    var modifications = _itemService.GetItemsByKindAndName<Modification>(name);
                    return Ok(modifications);

                case KindOfItem.ModificationBarrel:
                    var barrels = _itemService.GetItemsByKindAndName<ModificationBarrel>(name);
                    return Ok(barrels);

                case KindOfItem.ModificationBipod:
                    var bipods = _itemService.GetItemsByKindAndName<ModificationBipod>(name);
                    return Ok(bipods);

                case KindOfItem.ModificationCharge:
                    var charges = _itemService.GetItemsByKindAndName<ModificationCharge>(name);
                    return Ok(charges);

                case KindOfItem.ModificationDevice:
                    var devices = _itemService.GetItemsByKindAndName<ModificationDevice>(name);
                    return Ok(devices);

                case KindOfItem.ModificationForegrip:
                    var foregrips = _itemService.GetItemsByKindAndName<ModificationForegrip>(name);
                    return Ok(foregrips);

                case KindOfItem.ModificationGasblock:
                    var gasblocks = _itemService.GetItemsByKindAndName<ModificationGasblock>(name);
                    return Ok(gasblocks);

                case KindOfItem.ModificationGoggles:
                    var goggles = _itemService.GetItemsByKindAndName<ModificationGoggles>(name);
                    return Ok(goggles);

                case KindOfItem.ModificationHandguard:
                    var handguards = _itemService.GetItemsByKindAndName<ModificationHandguard>(name);
                    return Ok(handguards);

                case KindOfItem.ModificationLauncher:
                    var launchers = _itemService.GetItemsByKindAndName<ModificationLauncher>(name);
                    return Ok(launchers);

                case KindOfItem.ModificationMount:
                    var mounts = _itemService.GetItemsByKindAndName<ModificationMount>(name);
                    return Ok(mounts);

                case KindOfItem.ModificationMuzzle:
                    var muzzles = _itemService.GetItemsByKindAndName<ModificationMuzzle>(name);
                    return Ok(muzzles);

                case KindOfItem.ModificationPistolgrip:
                    var pistolgrips = _itemService.GetItemsByKindAndName<ModificationPistolgrip>(name);
                    return Ok(pistolgrips);

                case KindOfItem.ModificationReceiver:
                    var receivers = _itemService.GetItemsByKindAndName<ModificationReceiver>(name);
                    return Ok(receivers);

                case KindOfItem.ModificationSight:
                    var sights = _itemService.GetItemsByKindAndName<ModificationSight>(name);
                    return Ok(sights);

                case KindOfItem.ModificationSightSpecial:
                    var specials = _itemService.GetItemsByKindAndName<ModificationSightSpecial>(name);
                    return Ok(specials);

                case KindOfItem.ModificationStock:
                    var stocks = _itemService.GetItemsByKindAndName<ModificationStock>(name);
                    return Ok(stocks);

                case KindOfItem.Money:
                    var moneys = _itemService.GetItemsByKindAndName<Money>(name);
                    return Ok(moneys);

                case KindOfItem.Tacticalrig:
                    var tacticalrigs = _itemService.GetItemsByKindAndName<Tacticalrig>(name);
                    return Ok(tacticalrigs);

                default:
                    return NoContent();

                    #endregion
            }
        }
    }
}

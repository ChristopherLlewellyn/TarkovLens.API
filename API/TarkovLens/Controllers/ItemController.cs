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
        private IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_itemRepository.GetAllItems());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            // When we pass the Id in the route, we should use "-" instead of "/"
            // Then we should convert it back to a RavenId
            id = id.ReplaceFirst("-", "/");

            IItem item = _itemRepository.GetItemById(id);
            if (item.IsNull())
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("bsgid/{bsgId}")]
        public IActionResult GetByBsgId(string bsgId)
        {
            IItem item = _itemRepository.GetItemByBsgId(bsgId);
            if (item.IsNull())
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return BadRequest("Missing parameters: \"name\"");
            }

            var items = _itemRepository.GetItemsByName(name);
            return Ok(items);
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
            switch (kind)
            {
                #region Kinds

                case KindOfItem.Ammunition:
                    var ammunitions = new List<Ammunition>();
                    if (caliber.IsNotNullOrEmpty())
                    {
                        ammunitions = _itemRepository.GetAmmunitionByCaliber(caliber, name);
                    }
                    else
                    {
                        ammunitions = _itemRepository.GetItemsByKindAndName<Ammunition>(name);
                    }
                    return Ok(ammunitions);

                case KindOfItem.Armor:
                    var armors = _itemRepository.GetItemsByKindAndName<Armor>(name);
                    return Ok(armors);

                case KindOfItem.Backpack:
                    var backpacks = _itemRepository.GetItemsByKindAndName<Backpack>(name);
                    return Ok(backpacks);

                case KindOfItem.Barter:
                    var barters = _itemRepository.GetItemsByKindAndName<Barter>(name);
                    return Ok(barters);

                case KindOfItem.Clothing:
                    var clothings = _itemRepository.GetItemsByKindAndName<Clothing>(name);
                    return Ok(clothings);

                case KindOfItem.Common:
                    var commons = _itemRepository.GetItemsByKindAndName<Common>(name);
                    return Ok(commons);

                case KindOfItem.Container:
                    var containers = _itemRepository.GetItemsByKindAndName<Container>(name);
                    return Ok(containers);

                case KindOfItem.Firearm:
                    var firearms = _itemRepository.GetItemsByKindAndName<Firearm>(name);
                    return Ok(firearms);

                case KindOfItem.Food:
                    var foods = _itemRepository.GetItemsByKindAndName<Food>(name);
                    return Ok(foods);

                case KindOfItem.Grenade:
                    var grenades = _itemRepository.GetItemsByKindAndName<Grenade>(name);
                    return Ok(grenades);

                case KindOfItem.Headphone:
                    var headphones = _itemRepository.GetItemsByKindAndName<Headphone>(name);
                    return Ok(headphones);

                case KindOfItem.Key:
                    var keys = _itemRepository.GetItemsByKindAndName<Key>(name);
                    return Ok(keys);

                case KindOfItem.Magazine:
                    var magazines = _itemRepository.GetItemsByKindAndName<Magazine>(name);
                    return Ok(magazines);

                case KindOfItem.Map:
                    var maps = _itemRepository.GetItemsByKindAndName<Map>(name);
                    return Ok(maps);

                case KindOfItem.Medical:
                    var medicals = _itemRepository.GetItemsByKindAndName<Medical>(name);
                    return Ok(medicals);

                case KindOfItem.Melee:
                    var melees = _itemRepository.GetItemsByKindAndName<Melee>(name);
                    return Ok(melees);

                case KindOfItem.Modification:
                    var modifications = _itemRepository.GetItemsByKindAndName<Modification>(name);
                    return Ok(modifications);

                case KindOfItem.ModificationBarrel:
                    var barrels = _itemRepository.GetItemsByKindAndName<ModificationBarrel>(name);
                    return Ok(barrels);

                case KindOfItem.ModificationBipod:
                    var bipods = _itemRepository.GetItemsByKindAndName<ModificationBipod>(name);
                    return Ok(bipods);

                case KindOfItem.ModificationCharge:
                    var charges = _itemRepository.GetItemsByKindAndName<ModificationCharge>(name);
                    return Ok(charges);

                case KindOfItem.ModificationDevice:
                    var devices = _itemRepository.GetItemsByKindAndName<ModificationDevice>(name);
                    return Ok(devices);

                case KindOfItem.ModificationForegrip:
                    var foregrips = _itemRepository.GetItemsByKindAndName<ModificationForegrip>(name);
                    return Ok(foregrips);

                case KindOfItem.ModificationGasblock:
                    var gasblocks = _itemRepository.GetItemsByKindAndName<ModificationGasblock>(name);
                    return Ok(gasblocks);

                case KindOfItem.ModificationGoggles:
                    var goggles = _itemRepository.GetItemsByKindAndName<ModificationGoggles>(name);
                    return Ok(goggles);

                case KindOfItem.ModificationHandguard:
                    var handguards = _itemRepository.GetItemsByKindAndName<ModificationHandguard>(name);
                    return Ok(handguards);

                case KindOfItem.ModificationLauncher:
                    var launchers = _itemRepository.GetItemsByKindAndName<ModificationLauncher>(name);
                    return Ok(launchers);

                case KindOfItem.ModificationMount:
                    var mounts = _itemRepository.GetItemsByKindAndName<ModificationMount>(name);
                    return Ok(mounts);

                case KindOfItem.ModificationMuzzle:
                    var muzzles = _itemRepository.GetItemsByKindAndName<ModificationMuzzle>(name);
                    return Ok(muzzles);

                case KindOfItem.ModificationPistolgrip:
                    var pistolgrips = _itemRepository.GetItemsByKindAndName<ModificationPistolgrip>(name);
                    return Ok(pistolgrips);

                case KindOfItem.ModificationReceiver:
                    var receivers = _itemRepository.GetItemsByKindAndName<ModificationReceiver>(name);
                    return Ok(receivers);

                case KindOfItem.ModificationSight:
                    var sights = _itemRepository.GetItemsByKindAndName<ModificationSight>(name);
                    return Ok(sights);

                case KindOfItem.ModificationSightSpecial:
                    var specials = _itemRepository.GetItemsByKindAndName<ModificationSightSpecial>(name);
                    return Ok(specials);

                case KindOfItem.ModificationStock:
                    var stocks = _itemRepository.GetItemsByKindAndName<ModificationStock>(name);
                    return Ok(stocks);

                case KindOfItem.Money:
                    var moneys = _itemRepository.GetItemsByKindAndName<Money>(name);
                    return Ok(moneys);

                case KindOfItem.Tacticalrig:
                    var tacticalrigs = _itemRepository.GetItemsByKindAndName<Tacticalrig>(name);
                    return Ok(tacticalrigs);

                default:
                    return NoContent();

                    #endregion
            }
        }
    }
}

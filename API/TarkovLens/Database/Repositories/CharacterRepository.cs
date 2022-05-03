using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Database.Repositories;
using TarkovLens.Documents.Characters;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Indexes;
using TarkovLens.Interfaces;
using TarkovLens.Models.Items;

namespace TarkovLens.Services.Item
{
    public interface ICharacterRepository : IRepository
    {
        public ICharacter GetCharacterById(string id);
        public IEnumerable<ICharacter> GetAll();
        public IEnumerable<ICharacter> GetCharactersByType(CharacterType type);
        public IEnumerable<Combatant> GetCombatants();
    }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDocumentSession session;

        public CharacterRepository(IDocumentSession documentSession)
        {
            session = documentSession;
        }

        public ICharacter GetCharacterById(string id)
        {
            var character = session.Load<ICharacter>(id);
            if (character.IsNotNull())
            {
                session.Advanced.IgnoreChangesFor(character);
            }
            return character;
        }

        public IEnumerable<ICharacter> GetAll()
        {
            var characters = session
                .Query<ICharacter, Characters_ByType>()
                .ToList();
            return characters;
        }

        public IEnumerable<ICharacter> GetCharactersByType(CharacterType type)
        {
            List<ICharacter> characters = session
                .Query<ICharacter, Characters_ByType>()
                .Where(x => x.Type == type)
                .ToList();
            return characters;
        }

        public IEnumerable<Combatant> GetCombatants()
        {
            var combatants = session.Query<Combatant>().ToList();
            return combatants;
        }

        public void IncreaseMaxNumberOfRequestsPerSession(int increase) =>
            session.Advanced.MaxNumberOfRequestsPerSession += increase;

        public void SaveChanges() => session.SaveChanges();
    }
}

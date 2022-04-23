using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TarkovLens.Contracts;
using TarkovLens.Documents.Characters;
using TarkovLens.Enums;
using TarkovLens.Interfaces;
using Xunit;

namespace TarkovLens.IntegrationTests
{
    public class CharacterControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturnCharacters()
        {
            // Arrange
            var route = ApiRoutes.Characters.Get();

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var characters = Deserialize<IEnumerable<Combatant>>(json);
            characters.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task GetById_ShouldReturnCharacter()
        {
            // Arrange
            var route = ApiRoutes.Characters.Get(id: "combatants-1-A");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var character = Deserialize<Combatant>(json);
            character.Should().NotBeNull();
            character.Id.ToLowerInvariant().Should().Equals("combatants-1-A");
        }

        [Fact]
        public async Task GetByType_ShouldReturnCharactersWithSpecifiedType()
        {
            // Arrange
            var route = ApiRoutes.Characters.Type(type: CharacterType.Boss);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var bosses = Deserialize<IEnumerable<Combatant>>(json);
            bosses.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task GetCombatants_ShouldReturnCombatants()
        {
            // Arrange
            var route = ApiRoutes.Characters.Combatants();

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var combatants = Deserialize<IEnumerable<Combatant>>(json);
            combatants.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}

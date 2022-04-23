using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Contracts;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Models.Items;
using Xunit;

namespace TarkovLens.IntegrationTests
{
    public class ItemControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturnItems()
        {
            // Arrange
            var route = ApiRoutes.Items.Get();

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var items = Deserialize<IEnumerable<BaseItem>>(json);
            items.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task GetById_ShouldReturnItem()
        {
            // Arrange
            var route = ApiRoutes.Items.Get(id: "firearms-1-A");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var item = Deserialize<BaseItem>(json);
            item.Should().NotBeNull();
            item.Id.ToLowerInvariant().Should().Equals("firearms/1-A");
        }

        [Fact]
        public async Task GetByBsgId_ShouldReturnItem()
        {
            // Arrange
            var route = ApiRoutes.Items.BsgId(bsgId: "5ea05cf85ad9772e6624305d");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var item = Deserialize<BaseItem>(json);
            item.Should().NotBeNull();
            item.BsgId.ToLowerInvariant().Should().Be("5ea05cf85ad9772e6624305d");
        }

        [Fact]
        public async Task SearchingByName_ShouldReturnItems()
        {
            // Arrange
            var route = ApiRoutes.Items.Search(name: "bitcoin");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var items = Deserialize<IEnumerable<BaseItem>>(json);
            items.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task SearchingByEmptyName_ShouldBeBadRequest()
        {
            // Arrange
            var route = ApiRoutes.Items.Search(name: "");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetKinds_ShouldReturnAllItemKinds()
        {
            // Arrange
            var route = ApiRoutes.Items.Kind();

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var kinds = Deserialize<IEnumerable<string>>(json);
            kinds.Should().NotBeNull().And.NotBeEmpty().And.Equal(Enum.GetNames(typeof(KindOfItem)));
        }

        [Fact]
        public async Task GetByKind_ShouldReturnItems()
        {
            // Arrange
            var route = ApiRoutes.Items.Kind(kind: KindOfItem.Ammunition);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var ammunitions = Deserialize<IEnumerable<Ammunition>>(json);
            ammunitions.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task GetByKindAmmunitionWithCaliberFilter_ShouldReturnAmmunitionWithSpecifiedCaliber()
        {
            // Arrange
            string caliber = "7.62";
            var route = ApiRoutes.Items.Kind(kind: KindOfItem.Ammunition, caliber: caliber);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var ammunitions = Deserialize<IEnumerable<Ammunition>>(json);
            ammunitions.Should().NotBeNull().And.NotBeEmpty();

            foreach (var ammunition in ammunitions)
            {
                ammunition.Caliber.ToLower().Should().Contain(caliber.ToLower());
            }
        }

        [Fact]
        public async Task GetByKindAmmunitionWithCaliberAndNameFilters_ShouldReturnAmmunitionWithSpecifiedCaliberAndNameShouldMatch()
        {
            // Arrange
            string caliber = "5.56";
            string name = "m995";
            var route = ApiRoutes.Items.Kind(kind: KindOfItem.Ammunition, caliber: caliber, name: name);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var ammunitions = Deserialize<IEnumerable<Ammunition>>(json);
            ammunitions.Should().NotBeNull().And.NotBeEmpty();

            foreach (var ammunition in ammunitions)
            {
                ammunition.Caliber.ToLower().Should().Contain(caliber.ToLower());
                ammunition.Name.ToLower().Should().Contain(name.ToLower());
            }
        }
    }
}

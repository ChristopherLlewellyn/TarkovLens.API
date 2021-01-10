using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TarkovLens.Contracts;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Interfaces;
using TarkovLens.Models.Items;
using Xunit;

namespace TarkovLens.IntegrationTests
{
    public class ItemControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturnItem()
        {
            // Arrange
            var route = ApiRoutes.Items.Get(id: "firearms-1-A");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<BaseItem>()).Should().NotBeNull();
        }

        [Fact]
        public async Task Search_ShouldReturnItems()
        {
            // Arrange
            var route = ApiRoutes.Items.Search(name: "bitcoin");

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<BaseItem>>()).Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task Kind_ShouldReturnItems()
        {
            // Arrange
            var route = ApiRoutes.Items.Kind(kind: KindOfItem.Ammunition);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Ammunition>>()).Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task Kind_AmmunitionWithCaliberFilter_ReturnsAmmunitionSatisfyingFilters()
        {
            // Arrange
            string caliber = "7.62";
            var route = ApiRoutes.Items.Kind(kind: KindOfItem.Ammunition, caliber: caliber);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ammunitions = await response.Content.ReadAsAsync<List<Ammunition>>();
            ammunitions.Should().NotBeNull().And.NotBeEmpty();

            foreach (var ammunition in ammunitions)
            {
                ammunition.Caliber.ToLower().Should().Contain(caliber.ToLower());
            }
        }

        [Fact]
        public async Task Kind_AmmunitionWithCaliberAndNameFilters_ReturnsAmmunitionSatisfyingFilters()
        {
            // Arrange
            string caliber = "5.56";
            string name = "m995";
            var route = ApiRoutes.Items.Kind(kind: KindOfItem.Ammunition, caliber: caliber, name: name);

            // Act
            var response = await TestClient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ammunitions = await response.Content.ReadAsAsync<List<Ammunition>>();
            ammunitions.Should().NotBeNull().And.NotBeEmpty();

            foreach (var ammunition in ammunitions)
            {
                ammunition.Caliber.ToLower().Should().Contain(caliber.ToLower());
                ammunition.Name.ToLower().Should().Contain(name.ToLower());
            }
        }
    }
}

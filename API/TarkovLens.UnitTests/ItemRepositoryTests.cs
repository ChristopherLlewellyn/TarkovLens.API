using FluentAssertions;
using NSubstitute;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using TarkovLens.Indexes;
using TarkovLens.Interfaces;
using TarkovLens.Models.Items;
using TarkovLens.Services.Item;
using Xunit;

namespace TarkovLens.UnitTests
{
    public class ItemRepositoryTests
    {
        private readonly IItemRepository _sut;
        private readonly IDocumentSession _session = Substitute.For<IDocumentSession>();

        public ItemRepositoryTests()
        {
            _sut = new ItemRepository(_session);
        }

        [Fact]
        public void GetAllItems_ShouldReturnItems()
        {
            // Arrange
            IEnumerable<IItem> itemsMock = new List<BaseItem> {
                new BaseItem() { Id = "Firearms/1-A" },
                new BaseItem() { Id = "Ammunitions/1-A" }
            };

            _session.Query<IItem, Items_ByBsgId>().Returns(itemsMock);

            // Act
            var items = _sut.GetAllItems();

            // Assert
            items.Should().BeEquivalentTo(itemsMock);
        }

        [Fact]
        public void GetItemById_ShouldReturnItemWithSpecifiedId()
        {
            // Arrange
            var id = "Firearms/1-A";
            var name = "M4A1";

            IItem itemMock = new BaseItem
            {
                Id = id,
                Name = name
            };

            _session.Load<IItem>(id).Returns(itemMock);

            // Act
            var item = _sut.GetItemById(id);

            // Assert
            item.Id.Should().Be(itemMock.Id);
            item.Name.Should().Be(itemMock.Name);
        }
    }
}

using System;
using System.Collections.Generic;
using CatalogAPI.Entities;

namespace CatalogAPI.Repositories
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}
using System;
using System.Collections.Generic;
using CatalogAPI.Entities;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController: ControllerBase
    {
        private readonly InMenItemsRepository repository;

        public ItemsController()
        {
            repository = new InMenItemsRepository();
        }

        // GET /Items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
           var items = repository.GetItems();
           return items;
        }

        // GET /Items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return  NotFound();
            }
            return item;
        }
    }
}
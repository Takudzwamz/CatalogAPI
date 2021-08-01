using System.Linq;
using System;
using System.Collections.Generic;
using CatalogAPI.Entities;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Dtos;
using System.Threading.Tasks;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
           this.repository = repository;
        }

        // GET /Items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
           var items = (await repository.GetItemsAsync()).Select( item => item.AsDto());
           return items;
        }

        // GET /Items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return  NotFound();
            }
            return item.AsDto();
        }

        // POST /Items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
           Item item = new()
           {
               Id = Guid.NewGuid(), 
               Name = itemDto.Name,
               Price = itemDto.Price,
               CreatedDate = DateTimeOffset.UtcNow
           };

           await repository.CreateItemAsync(item);

           return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.AsDto());

        }

        // PUT /Items
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /Items
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
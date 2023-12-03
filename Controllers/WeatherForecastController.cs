using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static List<Item> _items = new List<Item>
    {
        new Item { Id = 1, Name = "Item 1" },
        new Item { Id = 2, Name = "Item 2" },
        new Item { Id = 3, Name = "Item 3" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Item>> Get()
    {
        return Ok(_items);
    }

    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Item> Create(Item item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public ActionResult<Item> Update(int id, Item updatedItem)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == id);
        if (existingItem == null)
            return NotFound();

        existingItem.Name = updatedItem.Name;

        return Ok(existingItem);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var itemToRemove = _items.FirstOrDefault(i => i.Id == id);
        if (itemToRemove == null)
            return NotFound();

        _items.Remove(itemToRemove);
        return NoContent();
    }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}

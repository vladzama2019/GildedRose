using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GildedRose.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        // initialize the collection of items
        List<Item> items = new List<Item>
        {
            new Item { Name= "C Programming Language", Description= "The authors present the complete guide to ANSI standard C language programming",  Price = 16 },
            new Item { Name = "Cracking the Coding Interview: 189 Programming Questions and Solutions", Description = "Cracking the Coding Interview, 6th Edition is here to teach you what you need to know and enabling you to perform at your very best.", Price = 32 },
            new Item { Name = "Microsoft Visual C# Step by Step (9th Edition) (Developer Reference)", Description = "Expand your expertise--and teach yourself the fundamentals of programming with the latest version of Visual C# with Visual Studio 2017.", Price = 34 }
        };

        /// <summary>
        /// Gets all inventory items
        /// Note: GET: api/Inventory
        /// </summary>
        /// <returns>All inventory items</returns>
        [HttpGet]
        public IActionResult GetAllItems()
        {
            if(items != null && items.Count() >0)
                return Ok(items);
            else
                return NotFound();
        }

        /// <summary>
        /// Gets the specific item from the inventory collection, based on the item name
        /// Note: api/Inventory/{item name}
        /// </summary>
        /// <param name="name">Item name</param>
        /// <returns>Inventory item</returns>
        [HttpGet("{name}")]
        [Authorize]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BuyItem(string name)
        {
            Item item = null;
            string errorMessage = string.Empty;

            try
            {
                // check if Gilded Rose has some inventory
                if (items == null || items.Count == 0)
                {
                    errorMessage = "Gilded Rose is out of inventory";
                }
                else
                {
                    // select the item by name
                    item = items.FirstOrDefault((inv) => inv.Name == name);
                    if (item == null)
                    {
                        errorMessage = $"Item {name} was not found";
                    }
                    else
                    {
                        // delete item from the collection
                        items.Remove(item);
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            // check if any error happened
            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok(item);
            }
            else
            {
                return NotFound(errorMessage);
            }
        }

        // POST: api/Inventory
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

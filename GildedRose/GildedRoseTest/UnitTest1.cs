using Microsoft.VisualStudio.TestTools.UnitTesting;
using GildedRose.Controllers;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GildedRose.Models;

namespace GildedRoseTest
{
    [TestClass]
    public class GildedRoseUnitTest
    {
        /// <summary>
        /// Testing GetAllItems method.
        /// Verifying if the result is not null, has status code =200 (success),
        /// and the selected item name, description, and price
        /// </summary>
        [TestMethod]
        public void GetTokenIsNotNull()
        {
            var tokenController = new TokenController();
            var result = tokenController.Get("vlad") as IActionResult;
            // assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result, string.Empty);
        }

        /// <summary>
        /// Testing GetAllItems method.
        /// Verifying if the result is not null, has status code = 200 (success),
        /// and the selected item name, description, and price
        /// </summary>
        [TestMethod]
        public void TestGetAllItems()
        {
            // create InventoryController instance
            var inventoryController = new InventoryController();
            
            // get all items from collection
            var result = inventoryController.GetAllItems();
            OkObjectResult okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(result);
            
            // get collection of items from the result
            List<Item> items = okResult.Value as List<Item>;

            // verify the status code
            Assert.AreEqual(200, okResult.StatusCode);
            // verifying the number of items
            Assert.AreEqual(3, items.Count);
            // verifying item names
            Assert.AreEqual("C Programming Language", items[0].Name);
            Assert.AreEqual("Cracking the Coding Interview: 189 Programming Questions and Solutions", items[1].Name);
            Assert.AreEqual("Microsoft Visual C# Step by Step (9th Edition) (Developer Reference)", items[2].Name);
        }

        /// <summary>
        /// Testing GetInventoryByName method by passing an item name.
        /// Verifying if the result is not null, has status code =200 (success),
        /// and the selected item name, description, and price
        /// </summary>
        [TestMethod]
        public void TestGetInventoryByName()
        {
            // create InventoryController instance
            var inventoryController = new InventoryController();

            // get the specific item
            var result = inventoryController.BuyItem("C Programming Language") as IActionResult;
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(result);

            // verify the status code
            Assert.AreEqual(200, okResult.StatusCode);

            // get the selected item from the collection and verify all properties
            Item item = (Item)okResult.Value;
            Assert.AreEqual(item.Name, "C Programming Language");
            Assert.AreEqual(item.Description, "The authors present the complete guide to ANSI standard C language programming");
            Assert.AreEqual(item.Price, 16);
        }
    }
}

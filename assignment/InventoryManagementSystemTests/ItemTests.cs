using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Tests
{
    [TestClass()]
    public class ItemTests
    {
        string btemName = "TestItem";
        int ItemNO = 534;
        Item testItem = new Item("TestItem", 534);

        [TestMethod()]
        public void ItemTest()
        {
            Assert.AreEqual(btemName, testItem.getName());
            Assert.AreEqual(ItemNO, testItem.getItemCode());
        }

        [TestMethod()]
        public void updateItemNameTest()
        {
            string newName = "WonkyProduct";
            testItem.updateItemName(newName);
            Assert.AreEqual(newName, testItem.getName());

        }

        [TestMethod()]
        public void changeQtyTest()
        {
            Assert.AreEqual(0, testItem.getQty());
            int newqty = 45;
            testItem.changeQty(newqty, true);
            Assert.AreEqual(newqty, testItem.getQty());
            testItem.changeQty(newqty, false);
            Assert.AreEqual(0, testItem.getQty());
            testItem.changeQty(newqty, false);
            Assert.AreEqual(-newqty, testItem.getQty());
        }
    }
}
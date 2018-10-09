using System;
using System.Collections;
using System.Collections.Generic;
namespace InventoryManagementSystem

{

    /// <summary>
    /// Inventory is a collection of items that any worker or warehouse location might have
    /// that are used as stock or components in production.
    /// </summary>
    public class Inventory
    {
        public string myName;
        private IDictionary<int, Item> myItems;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myName"></param>
        public Inventory(string myName)
        {
            this.myName = myName; 
            myItems = new Dictionary<int, Item>();
            Console.WriteLine(myName + " created.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        public void addItem(Item newItem)
        {
            this.myItems[newItem.getItemCode()] = newItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNo"></param>
        public void removeItem(int itemNo)
        {
            this.myItems.Remove(itemNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNo"></param>
        /// <returns></returns>
        public Item GetItem(int itemNo)
        {
            return this.myItems[itemNo];
        }






    }
}
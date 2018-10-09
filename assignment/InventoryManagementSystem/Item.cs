using System;
namespace InventoryManagementSystem
    
{
    ///<summary>
    /// The Item class is the basic unit in the inventory management system and each 
    /// agent or location will have an inventory of items.
    ///</summary>
    public class Item
    {
        private string itemName;
        private int itemNumber;
        private int qty;

        /// <summary>
        /// Item must be given a name and an id when created, these can later be changed
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemNumber"></param>
        public Item(string itemName, int itemNumber)
        {
            this.itemName = itemName;
            this.itemNumber = itemNumber;
            Console.WriteLine("Item " + this.itemName + " created. Item code: " + this.itemNumber);
        }

        /// <summary>
        /// Just gets the Item name as a string
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return this.itemName;
        }

        /// <summary>
        /// If you want to change the item name, you must provide a new string
        /// </summary>
        /// <param name="newItemName"></param>
        public void updateItemName(string newItemName)
        {
            this.itemName = newItemName;
        }


        /// <summary>
        /// change qty of items, increase must = true for an increase, false for decrease
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="increase"></param>
        public void changeQty(int amount, bool increase)
        {
            

            if (increase == true)
            {
                Console.WriteLine("Qty changed by " + amount);
                this.qty = this.qty + amount;
            }
            else
            {
                this.qty = this.qty - amount;
                Console.WriteLine("Qty change changed by -" + amount);
            }

            }

        /// <summary>
        /// Returns the qty of items in this location as an int
        /// </summary>
        /// <returns></returns>
        public int getQty()
        {
            return this.qty;
        }


        /// <summary>
        /// Returns the product code as an integer
        /// </summary>
        /// <returns></returns>
        public int getItemCode()
        {
            return this.itemNumber;
        }




        }
}
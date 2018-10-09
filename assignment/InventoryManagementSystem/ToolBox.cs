using System;
using System.Collections;
using System.Collections.Generic;

namespace InventoryManagementSystem
    
{
    /// <summary>
    /// Toolbox is used as a collection for storing equipment
    /// for either a location or a staff member
    /// </summary>
    public class ToolBox
    {

        private IDictionary<int, Equipment> myTools;
        public string myName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myName"></param>
        public ToolBox(string myName)
        {
            this.myName = myName;
            myTools = new Dictionary<int, Equipment>();
            Console.WriteLine(myName+" created");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEquipment"></param>
        public void addTools(Equipment newEquipment) {
            myTools.Add(newEquipment.getEquipmentCode(),newEquipment);
            Console.WriteLine(newEquipment.getEquipmentName() +" added to "+ myName + ".");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEquipment"></param>
        public void removeTools(Equipment newEquipment)
        {
            myTools.Remove(newEquipment.getEquipmentCode());
            Console.WriteLine(newEquipment.getEquipmentName()+" removed from myTools");
        }


    }
}
using System;

namespace InventoryManagementSystem
{

    /// <summary>
    /// Equipment class is used to represent any plant or tools
    /// staff need to perform certain roles, generally limited and non-consumable
    /// equipment that has enough value to be worth tracking
    /// </summary>
    public class Equipment
    {
        private string equipmentName;
        private int equipmentNo;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipmentName"></param>
        /// <param name="equipmentNo"></param>
        public Equipment(string equipmentName, int equipmentNo)
        {
            this.equipmentName = equipmentName;
            this.equipmentNo = equipmentNo;
            Console.WriteLine(equipmentName+" created.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getEquipmentCode()
        {
            return this.equipmentNo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getEquipmentName()
        {
            return this.equipmentName;
        }

        /// <summary>
        /// internal sytstem comand to disable a faulty equipment
        /// </summary>
        public void tagEquipmentFaulty()
        {
             // to be done
        }








    }
}
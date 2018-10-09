using System.Collections;
using System;


namespace InventoryManagementSystem
{
    /// <summary>
    /// Staff is the basic unit of worker, it houses all the methods needed to be carried out 
    /// by a worker in the system.
    /// </summary>
    public class Staff
    {

        private string staffName;
        private int staffId;
        public Inventory myInventory;
        public ToolBox myToolBox;
        public Roster myRoster;
        public Workload myWorkload;

 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffName"></param>
        /// <param name="staffId"></param>
        public Staff(string staffName, int staffId)
        {
            this.staffName = staffName;
            this.staffId = staffId;
            myInventory = new Inventory(staffName+"'s inventory");
            myToolBox = new ToolBox(staffName+"'s toolBox");
            myRoster = new Roster(staffName + "'s roster");
            myWorkload = new Workload(staffName + "'s workload");

            Console.WriteLine("Staff member "+staffName+ " created.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return this.staffName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getId()
        {
            return this.staffId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEquipment"></param>
        public void addEquipment(Equipment newEquipment)
        {

            Console.WriteLine(newEquipment.getEquipmentName() + " added to equipment for Staff member" + staffName);
        }

        

    }
}
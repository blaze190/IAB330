using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// This just simulates a very simplified range of behaviours the system will have to handle
    /// This will get replaced by unit testing
    /// </summary>
    class MainProgram
    {
        public static void Main(string[] args)
        {
            // Create a warehouse
            Location wareHouse = new Location("Warehouse", 1);

            // Create an item
            Item testItem = new Item("TestItem", 0001);

            // Put it in a warehouse location
            wareHouse.myInventory.addItem(testItem);

            // Set the new warehouse qty to be 100
            wareHouse.myInventory.GetItem(0001).changeQty(100, true);

            // Create an equipment
            Equipment forkLift = new Equipment("Toyota 2Tonne Ride-On", 001);

            // Put the equipment in a warehouse location
            wareHouse.myToolBox.addTools(forkLift);

            // Create a staff member
            Staff testStaff = new Staff("Sally Patterson", 0045);

            // Add staff member to warehouse
            wareHouse.myRoster.addStaffMember(testStaff);

            // Allocate the staff an equipment
            testStaff.addEquipment(forkLift);

            // Create a new staff member
            Staff testStaff2 = new Staff("Bob Dole", 0046);

            // Add staff to location
            wareHouse.myRoster.addStaffMember(testStaff2);

            // Add staff as a worker for sally
            wareHouse.myRoster.GetStaff(0045).myRoster.addStaffMember(testStaff2);

            // Create a Job
            Job testJob = new Job("JobScript");

            // Create a Workload
            Workload testWorkload = new Workload("testWorkload");
            testWorkload.AddJob(testJob);

            // Assign a workload to a staff member
            wareHouse.myRoster.GetStaff(0045).myWorkload.AddJob(testJob);

            Console.ReadKey();

        }
    }
}

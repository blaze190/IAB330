using System.Collections.Generic;
using System;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Roster is a collection of staff and can be held by a location or a
    /// staff member.
    /// </summary>
    public class Roster
    {
        private string myName;
        private IDictionary<int, Staff> myStaff;


        /// <summary>
        /// Roster must have a name as a string to be made
        /// </summary>
        /// <param name="myName"></param>
        public Roster(string myName)
        {
            this.myName = myName;
            myStaff = new Dictionary<int, Staff>();
            Console.WriteLine(myName+" created");
        }

        /// <summary>
        /// Add a staff member to the roster
        /// </summary>
        /// <param name="newStaff"></param>
        public void addStaffMember(Staff newStaff)
        {
            this.myStaff[newStaff.getId()] = newStaff;
            Console.WriteLine("Staff Member: "+ newStaff.getName()+ "added to "+ myName);

        }

        /// <summary>
        /// take a staff member from the roster
        /// </summary>
        /// <param name="staffNo"></param>
        public void removeItem(int staffNo)
        {
            this.myStaff.Remove(staffNo);
            Console.WriteLine("Staff Member: "+ staffNo + "removed." );
        }

         /// <summary>
         /// If you need to call the staff member's functions, call through here.
         /// </summary>
         /// <param name="staffNo"></param>
         /// <returns></returns>
        public Staff GetStaff(int staffNo)
        {
            return this.myStaff[staffNo];
        }
    }
}
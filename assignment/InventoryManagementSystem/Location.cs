namespace InventoryManagementSystem
{
    /// <summary>
    /// Location is used to simulate a site or section of a warehouse/factory etc
    /// a site will have locations and in those locations are either items, jobs or equipment.
    /// </summary>
    internal class Location
    {

        private string locationName;
        private int locationNumber;
        public Inventory myInventory;
        public Roster myRoster;
        public ToolBox myToolBox;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="locationNumber"></param>
        public Location(string locationName, int locationNumber)
        {
            this.locationName = locationName;
            this.locationNumber = locationNumber;
            myInventory = new Inventory(locationName + "'s inventory");
            myRoster = new Roster(locationName + "'s roster");
            myToolBox = new ToolBox(locationName+"'s toolBox");
            System.Console.WriteLine("location: "+locationName+" created");
        }
    }
}
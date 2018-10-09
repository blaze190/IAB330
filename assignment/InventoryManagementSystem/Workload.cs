using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Workload is a collection of jobs and can be had by a location of by
    /// a staff member
    /// </summary>
    public class Workload
    {
        private IDictionary<int, Job> myWorkload;
        private int myJobCount = 1; // Innital setup value
        private string myName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myName"></param>
        public Workload(string myName)
        {
            this.myName = myName;
            myWorkload = new Dictionary<int, Job>();
            Console.WriteLine("new Workload created.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newJob"></param>
        public void AddJob(Job newJob)
        {
            this.myWorkload.Add(myJobCount, newJob);
            myJobCount = myJobCount + 1;
            Console.WriteLine("job added");
        }
    }
}

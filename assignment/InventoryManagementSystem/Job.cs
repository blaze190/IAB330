using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Job
    {
        private string[] jobTasks;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobScript"></param>
        public Job(string jobScript)
        {
            string[] jobTasks = System.IO.File.ReadAllLines(@"C:\Users\dprob\source\repos\InventoryManagementSystem\InventoryManagementSystem\JobScript.txt");

            foreach (string task in jobTasks)
            {
                Console.WriteLine("Task: " + task);
            }
            

            Console.WriteLine("New Job created");
        }


    }
}

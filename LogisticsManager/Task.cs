using System;
using System.Collections.Generic;
using System.Text;

namespace LogisticsManager
{
    class Task
    {

        private string name;
        private List<string> script;

        public Task(string name, List<string> script) {
            this.name = name;
            this.script = script;

            Console.WriteLine("New Task Created: " + name);

        }

    }
}

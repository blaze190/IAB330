using System;
using System.Collections.Generic;
using System.Text;

namespace LogisticsManager
{
    public class Entry
    {
        private string icon;
        private string desc;
        private DateTime dateTime;
        private string user;

        public Entry(string icon, string desc, DateTime dateTime, string user) {
            this.icon = icon;
            this.desc = desc;
            this.dateTime = dateTime;
            this.user = user;
        }

        public string GetIcon() {
            return icon;
        }

        public string GetDesc()
        {
            return desc;
        }

        public DateTime GetDateTime() {
            return dateTime;
        }

        public string GetUser()
        {
            return user;
        }
    }
}

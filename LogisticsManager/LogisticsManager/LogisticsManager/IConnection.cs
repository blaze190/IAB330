using System;
using System.Collections.Generic;
using System.Text;

namespace LogisticsManager
{
    public interface IConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}

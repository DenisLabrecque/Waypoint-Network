using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
{
   class Racer
   {
      string _name;
      Dictionary<Waypoint, List<DateTime>> _visited;

      public Racer(string name)
      {
         if (string.IsNullOrWhiteSpace(name)) {
            throw new Exception("A racer name cannot be empty or blank.");
         }

         _name = name;
         _visited = new Dictionary<Waypoint, List<DateTime>>();
      }

      public void Visit(Waypoint waypoint, DateTime at)
      {
         if(_visited.ContainsKey(waypoint))
         {
            _visited[waypoint].Add(DateTime.Now);
         }
         else
         {
            _visited.Add(waypoint, new List<DateTime>() { DateTime.Now });
         }
      }

      public int TimesVisited(Waypoint waypoint)
      {
         if(_visited.ContainsKey(waypoint))
         {
            return _visited[waypoint].Count;
         }
         else
         {
            return 0;
         }
      }
   }
}

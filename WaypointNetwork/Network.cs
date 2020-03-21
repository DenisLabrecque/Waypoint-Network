using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
{
   class Network
   {
      HashSet<Waypoint> _waypoints = new HashSet<Waypoint>();

      public Network(List<(Waypoint, Waypoint, float)> connections)
      {
         foreach(var tuple in connections)
         {
            Connect(tuple.Item1, tuple.Item2, tuple.Item3);
         }
      }

      public List<Waypoint> ShortestPath(Waypoint from, Waypoint to) {
         Dictionary<double, List<Waypoint>> visited = new Dictionary<double, List<Waypoint>>();
         double cost = 0;
         Waypoint waypoint = from;
         do
         {

         }
      }

      public void Connect(Waypoint waypoint1, Waypoint waypoint2, float distance)
      {
         waypoint1.TwoWayConnect(waypoint2, distance);
         _waypoints.Add(waypoint1);
         _waypoints.Add(waypoint2);
      }

      public override string ToString()
      {
         StringBuilder builder = new StringBuilder();
         foreach(Waypoint waypoint in _waypoints)
         {
            builder.Append(waypoint.Callsign + ":\n");
            foreach(Connection connection in waypoint.Connections)
            {
               builder.Append("  " + connection.Waypoint2.Callsign + " " + connection.Distance + "\n");
            }
         }
         return builder.ToString();
      }
   }
}

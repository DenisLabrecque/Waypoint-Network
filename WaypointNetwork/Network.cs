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
         Queue<Waypoint> frontier = new Queue<Waypoint>();
         frontier.Enqueue(from);
         Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
         cameFrom.Add(from, null);
         Waypoint current;

         while(frontier.Count != 0)
         {
            current = frontier.Dequeue();
            foreach(Connection next in current.Neighbours)
            {
               if(cameFrom.ContainsKey(next.Next) == false)
               {
                  frontier.Enqueue(next.Next);
                  Console.WriteLine("Enqueued " + next.Next);
                  cameFrom[next.Next] = current;
               }
            }
         }

         current = to;
         List<Waypoint> path = new List<Waypoint>();
         while(current != from)
         {
            path.Add(current);
            current = cameFrom[current];
         }
         path.Add(from);

         foreach(Waypoint waypoint in path)
         {
            Console.WriteLine("Path: " + waypoint);
         }
         return path;
      }

      public void Connect(Waypoint waypoint1, Waypoint waypoint2, float distance)
      {
         waypoint1.TwoWayConnect(waypoint2, distance);
         _waypoints.Add(waypoint1);
         _waypoints.Add(waypoint2);
      }

      /// <summary>
      /// Prints all the waypoints in the network and their connections.
      /// </summary>
      /// <returns>A string that lists all the network nodes.</returns>
      public override string ToString()
      {
         StringBuilder builder = new StringBuilder();
         foreach(Waypoint waypoint in _waypoints)
         {
            builder.Append(waypoint.Callsign + ":\n");
            foreach(Connection connection in waypoint.Neighbours)
            {
               builder.Append("\n  (" + connection.Distance + ") " + connection.Next.Callsign);
            }
         }
         return builder.ToString();
      }
   }
}

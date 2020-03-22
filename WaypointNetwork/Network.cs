using System;
using System.Collections.Generic;
using System.Text;
using PriorityQueue;

namespace WaypointNetwork
{
   /// <summary>
   /// An all-knowing network of waypoints.
   /// </summary>
   class Network
   {
      HashSet<Waypoint> _waypoints = new HashSet<Waypoint>();

      /// <summary>
      /// Create a new network by adding connections between waypoints.
      /// </summary>
      /// <param name="connections">The list of connections to add as tuples.</param>
      public Network(List<(Waypoint, Waypoint, float)> connections)
      {
         foreach(var tuple in connections)
         {
            Connect(tuple.Item1, tuple.Item2, tuple.Item3);
         }
      }

      /// <summary>
      /// Find the shortest distance in waypoints from one waypoint to the next.
      /// </summary>
      /// <param name="to">Waypoint to start at.</param>
      /// <param name="from">Waypoint reach.</param>
      /// <returns>A list of waypoints that form the shortest path.</returns>
      public List<Waypoint> ShortestWaypoints(Waypoint from, Waypoint to) {
         return ListWaypoints(from, to, CrawlNetwork(from, to));
      }

      /// <summary>
      /// Find the shortest distance in connections from one waypoint to another.
      /// </summary>
      /// <param name="from">Waypoint to start at.</param>
      /// <param name="to">Waypoint to reach.</param>
      /// <returns>A list of connections that form the shortest path.</returns>
      public List<Connection> ShortestConnections(Waypoint from, Waypoint to)
      {
         return ListConnections(from, to, CrawlNetwork(from, to));
      }

      /// <summary>
      /// At each network node, find the direction one should come from that would point
      /// towards the objective.
      /// </summary>
      /// <param name="end">Waypoint to start at.</param>
      /// <param name="start">Waypoint to reach.</param>
      /// <returns>A dictionary of waypoints that indicates a direction for each waypoint.</returns>
      private Dictionary<Waypoint, Waypoint> CrawlNetwork(Waypoint end, Waypoint start)
      {
         SimplePriorityQueue<Waypoint> frontier = new SimplePriorityQueue<Waypoint>();
         frontier.Enqueue(start, 0);
         Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
         cameFrom.Add(start, null);
         Waypoint current;
         Dictionary<Waypoint, float> costSoFar = new Dictionary<Waypoint, float>();
         costSoFar.Add(start, 0);

         // Go through the nodes
         while (frontier.Count != 0)
         {
            current = frontier.Dequeue();

            foreach (KeyValuePair<Waypoint, float> next in current.Neighbours)
            {
               float newCost = costSoFar[current] + next.Value;
               if(costSoFar.ContainsKey(next.Key) == false || newCost < costSoFar[next.Key])
               {
                  costSoFar[next.Key] = newCost;
                  float priority = newCost;
                  frontier.Enqueue(next.Key, priority);
                  cameFrom[next.Key] = current;
               }
            }
         }
         return cameFrom;
      }

      private List<Waypoint> ListWaypoints(Waypoint from, Waypoint to, Dictionary<Waypoint, Waypoint> directions)
      {
         Waypoint current = from;
         List<Waypoint> path = new List<Waypoint>();
         while(current != to)
         {
            path.Add(current);
            current = directions[current];
         }
         path.Add(to);

         return path;
      }

      private List<Connection> ListConnections(Waypoint from, Waypoint to, Dictionary<Waypoint, Waypoint> directions)
      {
         Waypoint current = from;
         Waypoint end;
         List<Connection> path = new List<Connection>();
         while (current != to)
         {
            end = directions[current];
            Connection connection = new Connection(current, end, end.Neighbours[current]);
            path.Add(connection);
            current = end;
         }

         return path;
      }

      /// <summary>
      /// Create a mutual connection between waypoints.
      /// </summary>
      /// <param name="from">Waypoint to connect from.</param>
      /// <param name="to">Waypoint to connect to.</param>
      /// <param name="distance">Distance between waypoints.</param>
      public void Connect(Waypoint from, Waypoint to, float distance)
      {
         from.TwoWayConnect(to, distance);
         _waypoints.Add(from);
         _waypoints.Add(to);
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
            builder.Append("\n" + waypoint.Callsign + ":");
            foreach(KeyValuePair<Waypoint, float> neighbour in waypoint.Neighbours)
            {
               builder.Append("\n  (" + neighbour.Value + ") " + neighbour.Key);
            }
         }
         return builder.ToString();
      }
   }
}
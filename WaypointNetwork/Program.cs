using System;
using System.Collections.Generic;

namespace WaypointNetwork
{
   class Program
   {
      static void Main(string[] args)
      {
         Waypoint a = new Waypoint('A');
         Waypoint b = new Waypoint('B');
         Waypoint c = new Waypoint('C');
         Waypoint d = new Waypoint('D');
         Waypoint e = new Waypoint('E');
         Waypoint f = new Waypoint('F');
         Waypoint g = new Waypoint('G');


         List<(Waypoint, Waypoint, float)> list = new List<(Waypoint, Waypoint, float)>()
         {
            (a, b, 30.0f),
            (b, c, 20f),
            (a, c, 25.5f)
         };
         Network network = new Network(list);
         Console.WriteLine(network.ToString());

         Console.WriteLine();
         Console.WriteLine("Find the shortest connections from " + a + " to " + c + ":");
         List<Connection> connections = network.ShortestConnections(a, c);
         foreach (Connection connection in connections)
         {
            Console.WriteLine("  Connection: " + connection.ToString());
         }
      }
   }
}

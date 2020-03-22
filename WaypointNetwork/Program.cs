using System;
using System.Collections.Generic;

namespace WaypointNetwork
{
   class Program
   {
      static void Main(string[] args)
      {
         CallsignGenerator generator = new CallsignGenerator();
         List<string> names = generator.UniqueCallsigns(100);
         names.Sort();
         foreach (string name in names)
         {
            Console.WriteLine(name);
         }

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
            (a, c, 25.5f),
            (b, c, 3.0f),
            (c, d, 3.5f),
            (d, e, 1.0f),
            (d, f, 4.5f),
            (e, f, 1.0f),
            (f, g, 2f),
            (g, a, 30f)
         };
         Network network = new Network(list);
         Console.WriteLine(network.ToString());

         Console.WriteLine();
         Console.WriteLine("Find the shortest connections from " + a + " to " + f + ":");
         List<Connection> connections = network.ShortestConnections(a, f);
         foreach (Connection connection in connections)
         {
            Console.WriteLine("  Connection: " + connection.ToString());
         }
      }
   }
}

using System;
using System.Collections.Generic;
using System.Text;
using DGL.Utility;

namespace WaypointNetwork
{
   class Program
   {
      static void Main(string[] args)
      {
         CallsignGenerator generator = new CallsignGenerator(1, 1);
         generator.AddRandom(10);
         generator.AddUnique(10);
         Console.WriteLine("Random: " + generator);
         Console.WriteLine("Unique: " + generator);
         StringBuilder builder = new StringBuilder();
         //builder.AppendJoin(", ", generator.NamesSorted);
         //Console.WriteLine("Alphabeticise: " + builder);

         StringBuilder builder2 = new StringBuilder();
         builder2.AppendJoin(", ", generator);
         Console.WriteLine("What happened?: " + builder2);

         //Waypoint a = new Waypoint('A');
         //Waypoint b = new Waypoint('B');
         //Waypoint c = new Waypoint('C');
         //Waypoint d = new Waypoint('D');
         //Waypoint e = new Waypoint('E');
         //Waypoint f = new Waypoint('F');
         //Waypoint g = new Waypoint('G');


         //List<(Waypoint, Waypoint, float)> list = new List<(Waypoint, Waypoint, float)>()
         //{
         //   (a, b, 30.0f),
         //   (b, c, 20f),
         //   (a, c, 25.5f)
         //};
         //Network network = new Network(list);
         //Console.WriteLine(network.ToString());

         //Console.WriteLine();
         //Console.WriteLine("Find the shortest connections from " + a + " to " + c + ":");
         //List<Connection> connections = network.ShortestConnections(a, c);
         //foreach (Connection connection in connections)
         //{
         //   Console.WriteLine("  Connection: " + connection.ToString());
         //}

         //List<Waypoint> names = new List<Waypoint>();
         //for (int i = 0; i < names.Count; i++)
         //   names[i] = new Waypoint();
         //names.Sort();
         //foreach (var name in names)
         //   Console.WriteLine(name);
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaypointNetwork
{
   public class Waypoint
   {
      static CallsignGenerator _generator = null;

      string _callsign;
      List<Connection> _connections;

      public string Callsign {
         get {
            return _callsign;
         }
      }

      public List<Connection> Connections {
         get {
            return _connections;
         }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="generator">The generator used to create unique callsigns.</param>
      public Waypoint()
      {
         _callsign = _generator.Unique;
         ConstructorHelper();
      }

      public Waypoint(string callsign)
      {
         _callsign = callsign;
         ConstructorHelper();
      }

      private void ConstructorHelper()
      {
         if (_generator == null)
         {
            _generator = new CallsignGenerator();
         }
         _connections = new List<Connection>();
      }

      public Connection TwoWayConnect(Waypoint waypoint, float distance)
      {
         Connection connection = new Connection(this, waypoint, distance);
         _connections.Add(connection);
         waypoint.OneWayConnect(this, distance);
         return connection;
      }

      internal Connection OneWayConnect(Waypoint waypoint, float distance)
      {
         Connection connection = new Connection(this, waypoint, distance);
         _connections.Add(connection);
         return connection;
      }

      /// <summary>
      /// List the name of this waypoint node and the nodes it connects to.
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         string output = Callsign + " connects to:\n";
         foreach(Connection connection in _connections)
         {
            output += "  " + connection.Previous.Callsign + " - " + connection.Distance + " - " + connection.Next.Callsign + "\n";
         }
         return output;
      }
   }
}

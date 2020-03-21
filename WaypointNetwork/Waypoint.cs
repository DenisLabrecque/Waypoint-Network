using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaypointNetwork
{
   /// <summary>
   /// Represents a node on a network with a set of connections and distances to other waypoints.
   /// </summary>
   public class Waypoint
   {
      static CallsignGenerator _generator = null;

      string _callsign;
      List<Connection> _neighbours;

      /// <summary>
      /// This waypoint's name.
      /// </summary>
      public string Callsign {
         get {
            return _callsign;
         }
      }

      /// <summary>
      /// All the waypoints connected to this one, with their distances away.
      /// </summary>
      public List<Connection> Neighbours {
         get {
            return _neighbours;
         }
      }

      /// <summary>
      /// Returns a list of all the waypoints connected to this waypoint.
      /// </summary>
      public string ConnectionsDebug {
         get {
            StringBuilder builder = new StringBuilder();
            foreach (Connection connection in _neighbours)
            {
               builder.Append("\n  " + connection.Previous.Callsign + " - " + connection.Distance + " - " + connection.Next.Callsign);
            }
            return builder.ToString();
         }
      }

      /// <summary>
      /// Construct a waypoint with a random callsign.
      /// </summary>
      public Waypoint()
      {
         _callsign = _generator.Unique;
         ConstructorHelper();
      }

      /// <summary>
      /// Constructor a waypoint with a specific callsign.
      /// </summary>
      /// <param name="callsign"></param>
      public Waypoint(string callsign)
      {
         _callsign = callsign;
         ConstructorHelper();
      }

      /// <summary>
      /// Initialize a new callsign generator and the list of neighbours.
      /// </summary>
      private void ConstructorHelper()
      {
         if (_generator == null)
         {
            _generator = new CallsignGenerator();
         }
         _neighbours = new List<Connection>();
      }

      /// <summary>
      /// Connect this waypoint to another waypoint and connect the other waypoint
      /// back to this waypoint.
      /// </summary>
      /// <param name="waypoint">The waypoint to connect to.</param>
      /// <param name="distance">Distance between both waypoints.</param>
      /// <returns>The resulting connection.</returns>
      public Connection TwoWayConnect(Waypoint waypoint, float distance)
      {
         Connection connection = new Connection(this, waypoint, distance);
         _neighbours.Add(connection);
         waypoint.OneWayConnect(this, distance);
         return connection;
      }

      /// <summary>
      /// Connect this waypoint to another waypoint without connecting the other waypoint
      /// back to this one.
      /// </summary>
      /// <param name="waypoint">To waypoint to connect to.</param>
      /// <param name="distance">Distance between both waypoints.</param>
      /// <returns>The resulting connection.</returns>
      internal Connection OneWayConnect(Waypoint waypoint, float distance)
      {
         Connection connection = new Connection(this, waypoint, distance);
         _neighbours.Add(connection);
         return connection;
      }

      /// <summary>
      /// Give this waypoint's callsign.
      /// </summary>
      public override string ToString()
      {
         return Callsign;
      }
   }
}

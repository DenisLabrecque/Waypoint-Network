using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
{
   /// <summary>
   /// Represents two waypoints and the distance between them.
   /// </summary>
   public class Connection
   {
      private Waypoint _previous;
      private Waypoint _next;
      private float _distance;

      /// <summary>
      /// The waypoint going from.
      /// </summary>
      public Waypoint Previous {
         get {
            return _previous;
         }
      }

      /// <summary>
      /// The waypoint going to.
      /// </summary>
      public Waypoint Next {
         get {
            return _next;
         }
      }

      /// <summary>
      /// A non-valued length between waypoints. Cannot be negative.
      /// </summary>
      public float Distance {
         get {
            return _distance;
         }
      }

      /// <summary>
      /// Construct a connection between two waypoints, with a positive distance between them.
      /// </summary>
      /// <param name="previous">The waypoint to connect from.</param>
      /// <param name="next">The waypoint to connect to.</param>
      /// <param name="distance">The distance between waypoints. Must be greater than zero.</param>
      public Connection(Waypoint previous, Waypoint next, float distance = 1.0f)
      {
         if(previous == null || next == null)
         {
            throw new Exception("Waypoints cannot be connected if they are null.");
         }
         else if(distance <= 0)
         {
            throw new Exception("Waypoints must be distant from each other.");
         }

         _previous = previous;
         _next = next;
         _distance = distance;
      }

      /// <summary>
      /// Prints both connected waypoints and the distance between them.
      /// </summary>
      /// <returns>A one-line string.</returns>
      public override string ToString()
      {
         return "\n" + _previous.Callsign + " (" + _distance + ") " + _next.Callsign;
      }
   }
}

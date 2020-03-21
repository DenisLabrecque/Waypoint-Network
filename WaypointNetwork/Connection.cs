using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
{
   public class Connection
   {
      private Waypoint _previous;
      private Waypoint _next;
      private float _distance;

      public Waypoint Previous {
         get {
            return _previous;
         }
      }

      public Waypoint Next {
         get {
            return _next;
         }
      }

      public float Distance {
         get {
            return _distance;
         }
      }

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

      public override string ToString()
      {
         return _previous.Callsign + " -> " + _next.Callsign;
      }
   }
}

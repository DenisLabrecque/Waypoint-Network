using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
{
   public class Connection
   {
      private Waypoint _waypoint1;
      private Waypoint _waypoint2;
      private float _distance;

      public Waypoint Waypoint1 {
         get {
            return _waypoint1;
         }
      }

      public Waypoint Waypoint2 {
         get {
            return _waypoint2;
         }
      }

      public float Distance {
         get {
            return _distance;
         }
      }

      public Connection(Waypoint waypoint1, Waypoint waypoint2, float distance = 1.0f)
      {
         if(waypoint1 == null || waypoint2 == null)
         {
            throw new Exception("Waypoints cannot be connected if they are null.");
         }
         else if(distance <= 0)
         {
            throw new Exception("Waypoints must be distant from each other.");
         }

         _waypoint1 = waypoint1;
         _waypoint2 = waypoint2;
         _distance = distance;
      }

      public override string ToString()
      {
         return _waypoint1.Callsign + " -> " + _waypoint2.Callsign;
      }
   }
}

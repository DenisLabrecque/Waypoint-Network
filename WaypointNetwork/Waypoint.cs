﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DGL.Utility;

namespace WaypointNetwork
{
   /// <summary>
   /// Represents a node on a network with a set of connections and distances to other waypoints.
   /// </summary>
   public class Waypoint
   {
      static CallsignGenerator _generator = null;

      string _callsign;
      Dictionary<Waypoint, float> _neighbours;

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
      public Dictionary<Waypoint, float> Neighbours {
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
            foreach (var neighbour in _neighbours)
            {
               builder.Append("\n  " + _callsign + " - " + neighbour.Value + " - " + neighbour.Key);
            }
            return builder.ToString();
         }
      }

      /// <summary>
      /// Construct a waypoint with a random callsign.
      /// </summary>
      public Waypoint()
      {
         ConstructorHelper();
         _callsign = _generator.NextUnique();
      }

      /// <summary>
      /// Constructor a waypoint with a specific callsign.
      /// </summary>
      /// <param name="callsign"></param>
      public Waypoint(string callsign)
      {
         ConstructorHelper();
         _callsign = callsign;
      }

      public Waypoint(char callsignFirstLetter)
      {
         ConstructorHelper();
         _callsign = _generator.NextRandom(callsignFirstLetter);
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
         _neighbours = new Dictionary<Waypoint, float>();
      }

      /// <summary>
      /// Connect this waypoint to another waypoint and connect the other waypoint
      /// back to this waypoint.
      /// </summary>
      /// <param name="waypoint">The waypoint to connect to.</param>
      /// <param name="distance">Distance between both waypoints.</param>
      public void TwoWayConnect(Waypoint waypoint, float distance)
      {
         _neighbours.Add(waypoint, distance);
         waypoint.OneWayConnect(this, distance);
      }

      /// <summary>
      /// Connect this waypoint to another waypoint without connecting the other waypoint
      /// back to this one.
      /// </summary>
      /// <param name="waypoint">To waypoint to connect to.</param>
      /// <param name="distance">Distance between both waypoints.</param>
      /// <returns>The resulting connection.</returns>
      internal void OneWayConnect(Waypoint waypoint, float distance)
      {
         _neighbours.Add(waypoint, distance);
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

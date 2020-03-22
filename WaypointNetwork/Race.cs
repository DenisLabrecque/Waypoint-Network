using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
{
   class Race
   {
      Network _network;
      Waypoint _startLocation;
      HashSet<Racer> _racers;
      DateTime _startTime;
      uint _laps;

      public HashSet<Racer> Racers {
         get {
            return _racers;
         }
      }

      public Race(List<(Waypoint, Waypoint, float)> connections)
      {
         _network = new Network(connections);
      }

      public bool AddRacer(Racer racer)
      {
         if(_racers.Contains(racer))
         {
            return false;
         }
         else
         {
            _racers.Add(racer);
            return true;
         }
      }

      public void AddRacers(HashSet<Racer> racers)
      {
         foreach(Racer racer in racers)
         {
            _racers.Add(racer);
         }
      }

      public void RemoveRacer(Racer racer)
      {
         _racers.Remove(racer);
      }

      public void RemoveAllRacers()
      {
         _racers.Clear();
      }

      public void Start(Waypoint startLocation, uint laps = 1)
      {
         _startLocation = startLocation; // TODO: check that the track contains the start waypoint
         _startTime = DateTime.Now;
         _laps = laps;
      }

      
   }
}

using GoogleMaps.LocationServices;
using System;

namespace AspImp.Extensions
{
  public static class MapPointExtensions
  {
    public static double GetDistanceTo(this MapPoint fromMapPoint, MapPoint toMapPoint, char unit = 'K')
    {
      double rlat1 = Math.PI * fromMapPoint.Latitude / 180;
      double rlat2 = Math.PI * toMapPoint.Latitude / 180;
      double theta = fromMapPoint.Longitude - toMapPoint.Longitude;

      double rtheta = Math.PI * theta / 180;

      double dist =
          Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
          Math.Cos(rlat2) * Math.Cos(rtheta);
      dist = Math.Acos(dist);
      dist = dist * 180 / Math.PI;
      dist = dist * 60 * 1.1515;

      switch (unit)
      {
        case 'K': //Kilometers -> default
          return dist * 1.609344;
        case 'N': //Nautical Miles 
          return dist * 0.8684;
        case 'M': //Miles
          return dist;
      }

      return dist;
    }
  }
}

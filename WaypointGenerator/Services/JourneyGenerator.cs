using SNC;
using WaypointGenerator.Models;

namespace WaypointGenerator.Services;

public class JourneyGenerator
{
    private readonly Random _rand = new(DateTime.UtcNow.Millisecond);

    public Journey GenerateRandomJourney(
        Vehicle vehicle,
        int waypointCount)
    {
        var waypoints = new List<Waypoint>();

        (double lat, double lon) = GetRandomStart(vehicle);
        var bearing = _rand.NextDouble() * 360;
        var speed = vehicle.GetRandomSpeed();
        //random seconds on bearing up to 1 hour
        var timeOnBearing = GetRandomTimeInSeconds();

        for (int i = 0; i < waypointCount; i++)
        {
            var waypoint = new Waypoint
            {
                Latitude = lat,
                Longitude = lon,
                SpeedMph = speed,
                TimeInSeconds = timeOnBearing
            };

            var distanceInFeet = speed / 3600.0 * 5280 * timeOnBearing;

            GeoCalc.GetEndingCoordinates(lat, lon, bearing, distanceInFeet, out var endLat, out var endLong, out var endBearing);

            if (ValidateLocationInOperatingZone(vehicle, lat, lon))
            {
                waypoints.Add(waypoint);
                lat = endLat;
                lon = endLong;
            }
            else
            {
                //end location not within the operating zone, retry
                i--;
            }

            speed = vehicle.GetRandomSpeed();
            timeOnBearing = GetRandomTimeInSeconds();

            bearing += vehicle.GetTurnDegrees();
            bearing = (bearing + 360) % 360;
        }

        return new Journey { Vehicle = vehicle, Waypoints = waypoints };
    }

    private double GetRandomTimeInSeconds()
    {
        return _rand.NextDouble() * 3600;
    }

    private bool ValidateLocationInOperatingZone(
        Vehicle vehicle,
        double lat,
        double longitude)
        {
        return vehicle.OperatingZone.MinLat <= lat && vehicle.OperatingZone.MaxLat >= lat
            && vehicle.OperatingZone.MinLong <= longitude && vehicle.OperatingZone.MaxLong >= longitude;
        }

    private (double lat, double lon) GetRandomStart(Vehicle vehicle)
    {
        return (
            (_rand.NextDouble() * (vehicle.OperatingZone.MaxLat - vehicle.OperatingZone.MinLat)) + vehicle.OperatingZone.MinLat,
            (_rand.NextDouble() * (vehicle.OperatingZone.MaxLong - vehicle.OperatingZone.MinLong)) + vehicle.OperatingZone.MinLong
        );
    }
}

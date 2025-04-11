using System.Text;

namespace WaypointGenerator.Models;

public class Journey
{
    public Vehicle Vehicle { get; set; } = default!;
    public List<Waypoint> Waypoints { get; set; } = [];

    public string ToJnyFormat()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Vehicle.ToString());
        double accumulatedTime = 0;
        for (var i = 0; i < Waypoints.Count; i++)
        {
            if (i > 0)
            {
                accumulatedTime += Waypoints[i].TimeInSeconds;
            }
            sb.AppendLine($"{Waypoints[i].Latitude:F6},{Waypoints[i].Longitude:F6},{accumulatedTime:F2}");
        }

        return sb.ToString();
    }
}

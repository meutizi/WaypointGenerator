namespace WaypointGenerator.Models;

public class Boat : Vehicle
{
    private static readonly Random _rand = new(DateTime.UtcNow.Millisecond);
    private static readonly List<OperatingZone> operatingZones =
    [
            new (15.6, 56.2, -49.8, -23.1),
            new (-48.8, -6.9, -28.6, 8.2),
            new (-43.4, 8.1, -161.4, -98.4),
            new (-41.1, -1.4, 62.2, 94.5)
    ];
    public override OperatingZone OperatingZone
    {
        get
        {
            _operatingZone ??= operatingZones[_rand.Next(operatingZones.Count)];
            return _operatingZone;
        }
    }
    private OperatingZone? _operatingZone;
    public override string Identifier => "BOAT";

    public BoatPower Power { get; set; }
    public double Draft { get; set; }
    public string Manufacturer { get; set; } = default!;
    public static Vehicle GenerateRandomVehicle()
    {
        return new Boat
        {
            Description = $"Boat {_rand.Next(1000)}",
            Weight = _rand.Next(5000, 50000),
            Width = (_rand.NextDouble() * 20) + 5,
            Height = (_rand.NextDouble() * 30) + 10,
            Length = (_rand.NextDouble() * 50) + 20,
            Power = (BoatPower)_rand.Next(3),
            Draft = (_rand.NextDouble() * 10) + 2,
            Manufacturer = "BoatMakers Inc."
        };
    }

    public override int GetRandomSpeed()
    {
        return Power switch
        {
            BoatPower.Motor => _rand.Next(25, 61),
            BoatPower.Sail => _rand.Next(15, 31),
            _ => _rand.Next(1, 11)
        };
    }

    public override int GetTurnDegrees()
    {
        return _rand.Next(-30, 31);
    }

    public override string ToString()
    {
        return $"{base.ToString()},{Power.ToString().ToUpperInvariant()},{Draft:F2},{Manufacturer}";
    }
}

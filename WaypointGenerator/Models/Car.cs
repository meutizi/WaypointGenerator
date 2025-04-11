namespace WaypointGenerator.Models;

public class Car : Vehicle
{
    private static readonly Random _rand = new(DateTime.UtcNow.Millisecond);
    public override OperatingZone OperatingZone => new(-90.0, 90.0, -180.0, 180.0);

    public override string Identifier => "CAR";

    public string Manufacturer { get; set; } = default!;
    public uint ModelYear { get; set; }
    public BodyStyle BodyStyle { get; set; }
    public FuelType FuelType { get; set; }

    public static Vehicle GenerateRandomVehicle()
    {
        return new Car
        {
            Description = $"Car {_rand.Next(1000)}",
            Weight = _rand.Next(2000, 6000),
            Width = (_rand.NextDouble() * 4) + 4,
            Height = (_rand.NextDouble() * 3) + 4,
            Length = (_rand.NextDouble() * 8) + 10,
            Manufacturer = "Auto Co",
            ModelYear = (uint)_rand.Next(2000, 2026),
            BodyStyle = (BodyStyle)_rand.Next(11),
            FuelType = (FuelType)_rand.Next(4)
        };
    }

    public override int GetRandomSpeed()
    {
        return _rand.Next(25, 61);
    }

    public override int GetTurnDegrees()
    {
        return _rand.Next(-90, 91);
    }

    public override string ToString()
    {
        return $"{base.ToString()},{Manufacturer},{ModelYear},{BodyStyle.ToString().ToUpperInvariant()},{FuelType.ToString().ToUpperInvariant()}";
    }
}

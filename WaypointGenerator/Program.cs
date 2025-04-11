using CommandLine;
using WaypointGenerator.Models;
using WaypointGenerator.Services;

Random _rand = new(DateTime.UtcNow.Millisecond);
int count = _rand.Next(10, 31);
Vehicle vehicle = _rand.Next(2) == 0 ? Boat.GenerateRandomVehicle() : Car.GenerateRandomVehicle();

Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => {
    if (o.Waypoints.HasValue)
    {
        if (o.Waypoints >= 10 && o.Waypoints <= 30)
        {
            count = o.Waypoints.Value;
        }
        else
        {
            Console.WriteLine($"Invalid number of waypoints specified, using {count}");
        }
    }
    if (o.IsBoat)
    {
        vehicle = Boat.GenerateRandomVehicle();
        if (o.BoatPower.HasValue)
        {
            ((Boat)vehicle).Power = o.BoatPower.Value;
        }
    }
    else if (o.IsCar)
    {
        vehicle = Car.GenerateRandomVehicle();
        if (o.BodyStyle.HasValue)
        {
            ((Car)vehicle).BodyStyle = o.BodyStyle.Value;
        }
        if (o.FuelType.HasValue)
        {
            ((Car)vehicle).FuelType = o.FuelType.Value;
        }
    }
    else
    {
        vehicle = _rand.Next(2) == 0 ? Boat.GenerateRandomVehicle() : Car.GenerateRandomVehicle();
    }
});

var generator = new JourneyGenerator();
var journey = generator.GenerateRandomJourney(vehicle, count);

string outputPath = $"journey_{DateTime.UtcNow:yyyyMMdd_HHmmss}.jny";
File.WriteAllText(outputPath, journey.ToJnyFormat());
Console.WriteLine($"Journey File written to {outputPath}");

public class Options
{
    [Option('w', "waypointCount", Required=false, HelpText ="Sets the number of waypoints, between 10 and 30.")]
    public int? Waypoints { get; set; }

    [Option('b', "boat", Required = false, HelpText = "Sets the type of to boat", SetName = "Boat" )]
    public bool IsBoat { get; set; }

    [Option('p', "boatPower", Required = false, HelpText = "Sets the type of to boat power. Valid options are: Unpowered, Sail, Motor", SetName = "Boat")]
    public BoatPower? BoatPower { get; set; }

    [Option('c', "car", Required = false, HelpText = "Sets the type of to car", SetName = "Car")]
    public bool IsCar { get; set; }

    [Option('s', "bodyStyle", Required = false, HelpText = "Sets the type of to car body style. Valid options are: Bus, Compact, Coupe, Crossover, Minivan, Sedan, Semi, Sports, Suv, Truck, Van", SetName = "Car")]
    public BodyStyle? BodyStyle { get; set; }

    [Option('f', "fuelType", Required = false, HelpText = "Sets the type of to car fuel type. Valid options are: Regular, Diesel, Hybrid, Electric", SetName = "Car")]
    public FuelType? FuelType { get; set; }
}

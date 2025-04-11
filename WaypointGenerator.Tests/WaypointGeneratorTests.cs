using WaypointGenerator.Models;

namespace WaypointGenerator.Tests;

public class WaypointGeneratorTests
{
    [Fact]
    public void Car_ToString_HasCorrectFormat()
    {
        var car = new Car
        {
            Description = "TestCar",
            Weight = 3000,
            Width = 6.0,
            Height = 5.0,
            Length = 15.0,
            Manufacturer = "TestCo",
            ModelYear = 2022,
            BodyStyle = BodyStyle.Sedan,
            FuelType = FuelType.Regular
        };

        var line = car.ToString();
        Assert.Equal("CAR,TestCar,3000.00,6.00,5.00,15.00,TestCo,2022,SEDAN,REGULAR", line, ignoreCase: true);
    }

    [Fact]
    public void Boat_ToString_HasCorrectFormat()
    {
        var boat = new Boat
        {
            Description = "SeaTest",
            Weight = 10000,
            Width = 10,
            Height = 12,
            Length = 30,
            Power = BoatPower.Sail,
            Draft = 7.5,
            Manufacturer = "BoatMakers"
        };

        var line = boat.ToString();
        Assert.Equal("BOAT,SeaTest,10000.00,10.00,12.00,30.00,SAIL,7.50,BoatMakers", line, ignoreCase: true);
    }

    [Fact]
    public void JourneyFile_HasCorrectFormat()
    {
        var boat = new Boat
        {
            Description = "SeaTest",
            Weight = 10000,
            Width = 10,
            Height = 12,
            Length = 30,
            Power = BoatPower.Sail,
            Draft = 7.5,
            Manufacturer = "BoatMakers"
        };

        var journey = new Journey
        {
            Vehicle = boat
        };
        journey.Waypoints.AddRange(
        [
            new Waypoint { Latitude = 37.0, Longitude = -122.0, TimeInSeconds=0 },
            new Waypoint { Latitude = 37.01, Longitude = -122.01, TimeInSeconds=1145 }
        ]);

        var output = journey.ToJnyFormat();
        var expectedOutput = "BOAT,SeaTest,10000.00,10.00,12.00,30.00,SAIL,7.50,BoatMakers\r\n37.000000,-122.000000,0.00\r\n37.010000,-122.010000,1145.00\r\n";
        Assert.Equal(expectedOutput, output, ignoreCase: true);


    }
}

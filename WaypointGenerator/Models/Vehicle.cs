namespace WaypointGenerator.Models;

public abstract class Vehicle
{
    public abstract OperatingZone OperatingZone { get; }
    public abstract string Identifier { get; }
    public string Description { get; set; } = default!;
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public abstract int GetRandomSpeed();
    public abstract int GetTurnDegrees();
    public override string ToString()
    {
        return $"{Identifier},{Description},{Weight:F2},{Width:F2},{Height:F2},{Length:F2}";
    }
}
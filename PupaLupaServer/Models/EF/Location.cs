namespace PupaLupaServer.Models.EF;

public partial class Location
{
    public Guid UserId { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }
}

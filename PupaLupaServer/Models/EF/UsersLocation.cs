using System;
using System.Collections.Generic;

namespace PupaLupaServer.Models.EF;

public partial class UsersLocation
{
    public Guid UserId { get; set; }

    /// <summary>
    /// Ширина
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    public decimal? Longitude { get; set; }
}

using System;
using System.Collections.Generic;

namespace PupaLupaServer.Models.EF;

public partial class Statistic
{
    public Guid Id { get; set; }

    public short? RelationType { get; set; }

    /// <summary>
    /// hoursCount
    /// </summary>
    public int? TimeTogether { get; set; }

    public DateTime? FirstMetDate { get; set; }

    public long? MessagesCount { get; set; }

    public int? MeetingsCount { get; set; }
}

using System;
using System.Collections.Generic;

namespace PupaLupaServer.Models.EF;

public partial class Relationship
{
    public Guid UserId { get; set; }

    public Guid FriendId { get; set; }

    public Guid? StatisticsId { get; set; }
}

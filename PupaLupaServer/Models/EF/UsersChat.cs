using System;
using System.Collections.Generic;

namespace PupaLupaServer.Models.EF;

public partial class UsersChat
{
    public Guid UserId { get; set; }

    public Guid ChatId { get; set; }
}

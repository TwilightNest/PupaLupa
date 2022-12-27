using System;
using System.Collections.Generic;

namespace PupaLupaServer.Models.EF;

public partial class Chat
{
    public Guid Id { get; set; }

    public string? LastMessage { get; set; }

    public Guid? LastSenderUserId { get; set; }
}

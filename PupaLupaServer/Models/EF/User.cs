using System;
using System.Collections.Generic;

namespace PupaLupaServer.Models.EF;

public partial class User
{
    public Guid Id { get; set; }

    /// <summary>
    /// nickname
    /// </summary>
    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;
}

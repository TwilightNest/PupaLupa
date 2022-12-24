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

    public virtual ICollection<InboxParticipant> InboxParticipants { get; } = new List<InboxParticipant>();

    public virtual ICollection<Message> Messages { get; } = new List<Message>();
}

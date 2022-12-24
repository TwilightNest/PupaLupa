namespace PupaLupaServer.Models.EF;

public partial class Inbox
{
    public Guid Id { get; set; }

    public string? LastMessage { get; set; }

    public Guid? LastSentUserId { get; set; }

    public virtual ICollection<InboxParticipant> InboxParticipants { get; } = new List<InboxParticipant>();

    public virtual ICollection<Message> Messages { get; } = new List<Message>();
}
